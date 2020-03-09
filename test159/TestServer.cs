using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;
using ServerGame.Core.Connctions;
using ServerGame.Core.Data;
using ServerGame.Core.Event;
using ServerGame.Core.EventStore;
using ServerGame.Core.GException;
using ServerGame.Interface.User;
using ServerGame.Interface.Zone;
using ServerGame.Program.Room;
using ServerGame.Program.Zone;
using static ServerGame.Core.Connctions.AbstractServer;

namespace ServerGame
{

    public class TestServer
    {


        static  int port_tcp = 9999;
        static  int port_udp  = 9998;
        static ServerGame.Program.Connctions.Server server;
        private static void OnLoopSedingUDP(object arg1, ResEventArgs arg2)
        {
            if (arg2.ConnctionType == Interface.Connctions.ConnctionType.UDP)
            {
                foreach (var data in arg2.PackSendData.AllData)
                {
                    Console.WriteLine(data.DataS.ToString());
                    Core.Connctions.Server.ServerOject.Sendudp(arg2.UserSenderAllInfo.workSocket, data.DataS.ToString(), arg2.UserSenderAllInfo.IPEndPointUDP);
                }
            }
            else
            {

                foreach (var data in arg2.PackSendData.AllData)
                {
                    Console.WriteLine(data.DataS.ToString());
                    Core.Connctions.Server.ServerOject.Send(arg2.UserSenderAllInfo.workSocket, data.DataS.ToString());
                }

            }
        }

        private static void onAtho(object arg1, ResEventArgs arg2)
        {
            try
            {
                
              if (arg2.PackSendData.AllData.Count<4 )
                {

                    throw new Exception("Atho Function Not Count is " + arg2.PackSendData.AllData.Count.ToString() + " Must be or more 4 ");
                }
               
                var username =  arg2.PackSendData.AllData.Find(x=>x.PramterName == 0).DataS.ToString();
                var password =  arg2.PackSendData.AllData.Find(x => x.PramterName == 1).DataS.ToString();
                var id =Convert.ToInt32( arg2.PackSendData.AllData.Find(x => x.PramterName == 2).DataS);
                var Room = Convert.ToInt32(arg2.PackSendData.AllData.Find(x => x.PramterName == 3).DataS);
                var zone = ZoneMaster.GetZoneBy(id);
                if (arg2.ConnctionType == Interface.Connctions.ConnctionType.UDP)
                {
                    if (zone != null)
                        if (ZoneMaster.CanLoginToZoneBy(username, password, zone))
                        {
                            IUser user;
                            ZoneMaster.AddUserToRoomLubbyBy(username, password, Room, ref zone, arg2.UserSenderAllInfo, out user);
                            if (user.ConnctionUDP == null)
                            {

                                user.ConnctionUDP = new StateObjectupb();
                                user.ConnctionUDP.user = user;
                                var portout = 0;
                                var v = user.ConnctionUDP;

                                if (server.WatchUdpByPort(ref v, out portout))
                                {
                                    var newdatapack = new List<Core.Data.Data>();
                                    newdatapack.Add(new Core.Data.Data(user.IdIntTempUDP, 0));
                                    newdatapack.Add(new Core.Data.Data(portout, 1));

                                    var newpacket = new PackSendData(5, newdatapack);

                                    server.Sendudp(arg2.UserSenderAllInfo.workSocket, newpacket, arg2.UserSenderAllInfo.IPEndPointUDP);
                                }
                                else
                                    throw new CanNotWatchUdpPort();
                            }
                            else
                            {
                                var newdatapack = new List<Core.Data.Data>();
                                newdatapack.Add(new Core.Data.Data(user.IdIntTempUDP, 0));
                                newdatapack.Add(new Core.Data.Data((user.ConnctionUDP.workSocket.LocalEndPoint as IPEndPoint).Port, 1));

                                var newpacket = new PackSendData(5, newdatapack);

                                server.Sendudp(arg2.UserSenderAllInfo.workSocket, newpacket, arg2.UserSenderAllInfo.IPEndPointUDP);

                            }

                        }
                }else
                {
                    if (zone != null)
                        if (ZoneMaster.CanLoginToZoneBy(username, password, zone))
                        {
                            IUser user;
                            ZoneMaster.AddUserToRoomLubbyBy(username, password, Room, ref zone, arg2.UserSenderAllInfo, out user);
                            if (user.ConnctionTCP == null)
                            {

                                user.ConnctionTCP = arg2.UserSenderAllInfo ;
                                user.ConnctionTCP.user = user;
                             
                                arg2.UserSenderAllInfo.user = user;


                            }
                           

                        }

                }

            }
            catch (UserNameOrPasswordError e)
            {
                #if DEBUG
                Console.WriteLine(e.ToString());
            #endif
            }

            catch (Exception e )
            {
                #if DEBUG
                Console.WriteLine(e.ToString());
                 #endif
            }
            finally
            {


            }
        }

      
        private static void Brodcast(object arg1, ResEventArgs arg2)
        {


            try
            {
               
                RoomMaster.BradCastToAllOfData(arg2.UserSenderAllInfo.user.Room, arg2.PackSendData, true , arg2.UserSenderAllInfo.user , arg2.ConnctionType);
                

            }
            catch (UserNameOrPasswordError e)
            {
                #if DEBUG
                Console.WriteLine(e.ToString());
                #endif
            }

            catch (Exception e)
            {
            #if DEBUG
                Console.WriteLine(e.ToString());
            #endif
            }
            finally
            {


            }
        }

        public static void Main(string[] args)
        {

         
      

            EventStore.AddNewEvent(new Core.Event.LoopSendingUDP(OnLoopSedingUDP));
            EventStore.AddNewEvent(new Core.Event.Authtcation(onAtho));
            EventStore.AddNewEvent(new Core.Event.Broadcast(Brodcast)) ;


            Server.ZoneWorking.Add(new ServerGame.Core.Zone.Lobby());
            ServerGame.Program.Room.RoomMaster.init();

          //  Server.RoomWorking.Add()

            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
       
                Console.Write("Server#: ");
                var consoleRead = Console.ReadLine();
                if (consoleRead.Trim().TrimStart().TrimEnd().Length != 0)
                {
                    var ff = CommandLine.Parser.Default.ParseArguments<
                            startServer,
                            ShowAllConnctions,
                            Exit, sendstring
                            >
                            (consoleRead.Split(' ')

                            );
                        ff.MapResult(
                        (startServer opts) => RunServerAndReturnExitCode(opts),
                        (ShowAllConnctions opts) => RunShowAllConnctionsAndReturnExitCode(opts),
                       (Exit opts) => RunExitReturnExitCode(opts),
                       (sendstring opts) => RunSendReturnExitCode(opts),
                       ((errs) => HandleParseError(errs))
                       );
                        System.GC.Collect();
                }

            }
        }


        private static  object RunExitReturnExitCode(Exit opts)
        {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stopping Server...");
               if (server !=null)
                if (server.ListenersTCP != null) { 
                    foreach (var listener in server.ListenersTCP)
                    {
                        Console.WriteLine($"Connction is :{listener.Connected.ToString()} ,  Ip : {((IPEndPoint)listener.RemoteEndPoint).Address.ToString()} , port : {((IPEndPoint)listener.RemoteEndPoint).Port.ToString() } , SendTimeout : {listener.SendTimeout.ToString()}, ReceiveTimeout  : {listener.ReceiveTimeout.ToString()} :: closing listener ..");
                            listener.Close();

                        Console.WriteLine("Closed listener");

                        listener.Dispose();
                        Console.WriteLine("Dispose listener");
                    }

                        server.ListenersTCP.Clear();
                    }

                
         
            System.Environment.Exit(0);
            return null;
          
        }
        private static object RunSendReturnExitCode(sendstring opts)
        {
           
            if (server != null)
                if (server.ListenersTCP != null)
                {
                    server.Send(server.ListenersTCP[opts.idlist], opts.data);
                    /*
                    foreach (var listener in server.listeners)
                    {
                        Console.WriteLine($"Connction is :{listener.Connected.ToString()} ,  Ip : {((IPEndPoint)listener.RemoteEndPoint).Address.ToString()} , port : {((IPEndPoint)listener.RemoteEndPoint).Port.ToString() } , SendTimeout : {listener.SendTimeout.ToString()}, ReceiveTimeout  : {listener.ReceiveTimeout.ToString()} :: closing listener ..");
                        listener.Close();

                        Console.WriteLine("Closed listener");

                        listener.Dispose();
                        Console.WriteLine("Dispose listener");
                    }

                    server.listeners.Clear();*/
                }



            
            return null;

        }

        private static object RunServerAndReturnExitCode(startServer opts)
        {
             port_tcp = opts.tcpport;
             port_udp = opts.udpport;
             var loopback = opts.loopback;
             var Gzip = opts.Gzip;
             server = new ServerGame.Program.Connctions.Server(port_udp, port_tcp , loopback , Gzip);
            
             server.Start();

            return null;

        }
        private static async  Task <object> RunShowAllConnctionsAndReturnExitCode(ShowAllConnctions opts)
        {

            

                 foreach (var listener in server.ListenersTCP)
                 { 
                     Console.WriteLine($"Connction is :{listener.Connected.ToString()} , Ip : {((IPEndPoint)listener.LocalEndPoint).Port.ToString()} , Ip : {((IPEndPoint)listener.RemoteEndPoint).Address.ToString()} , port : {((IPEndPoint)listener.RemoteEndPoint).Port.ToString() } , SendTimeout : {listener.SendTimeout.ToString()}, ReceiveTimeout  : {listener.ReceiveTimeout.ToString()} ");
                if (opts.StopAll) {
                    listener.Shutdown(SocketShutdown.Both);
                     listener.Close(); }

                 }

                 server.ListenersTCP.RemoveAll(matching_listener_is_close);
       
        

            GC.Collect();
            
            return null;

        }
        private static bool matching_listener_is_close(Socket Socket)
        {

            return !Socket.Connected;
        }

        private static object HandleParseError(IEnumerable<Error> errs)
        {
            foreach (var err in errs)
            {
                if ( err.Tag == ErrorType.UnknownOptionError)
                Console.WriteLine("Unknown Command!!!");

            }
            return null;

        }


    }


    [Verb("start", HelpText = "Start Server For listern")]
    public class startServer
    {
        /*
            [Option('u', "udpport", Required = false, HelpText = "udp prot for start server")]
            public IEnumerable<int> udpport { get; set; }

            [Option('t', "tcpport", Required = false, HelpText = "Tcp prot for start server")]
            public IEnumerable<int> tcpport { get; set; }

            [Option('l', "loopback", Required = false, HelpText = "Return SendBack From/to Server to same client has been sent")]
            public IEnumerable<bool> loopback { get; set; }


            [Option('z', "Gzip", Required = false, HelpText = "Compress and decompress data mode for more speed")]
            public IEnumerable<bool> Gzip { get; set; }*/



        [Option('u', "udpport", Default = 9998, HelpText = "udp prot for start server")]
        public int udpport { get; set; }

        [Option('t', "tcpport", Default = 9999, HelpText = "Tcp prot for start server")]
        public int tcpport { get; set; }

        [Option('l', "loopback", Default = false, HelpText = "Return SendBack From/to Server to same client has been sent")]
        public bool loopback { get; set; }


        [Option('z', "gzip", Default = false, HelpText = "Compress and decompress data mode for more speed")]
        public bool Gzip { get; set; }

        /*
               [Option(
                 Default = false,
                 HelpText = "Prints all messages to standard output.")]
               public bool Verbose { get; set; }


               [Option("stdin",
                 Default = false,
                 HelpText = "Read from stdin")]


               public bool stdin { get; set; }

               [Value(0, MetaName = "offset", HelpText = "File offset.")]
               public long? Offset { get; set; }*/
        //normal options here
    }

    [Verb("show-all", HelpText = "Show all connction now on server")]
    public class ShowAllConnctions
    {
        [Option(
        'S', "StopAll",
      Default = false,
      HelpText = "Prints all messages to standard output.")]
        public bool StopAll { get; set; }
    }


    [Verb("exit", HelpText = "Starting exit methods")]
    public class Exit
    {/*
            [Option(
          Default = false,
          HelpText = "Prints all messages to standard output.")]
            public bool Verbose { get; set; } */
    }


    [Verb("sendstring", HelpText = "send string method")]
    public class sendstring
    {
        [Option('c', "id-list", Default = false, HelpText = "Id Client From list")]
        public int idlist { get; set; }

        [Option('d', "data", Default = false, HelpText = "Data string")]
        public string data { get; set; }
    }



}
