using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommandLine;

namespace ServerGamex
{
 
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

                [Option('t', "tcpport", Default =9999, HelpText = "Tcp prot for start server")]
                public int tcpport { get; set; }

                [Option('l', "loopback", Default = false, HelpText = "Return SendBack From/to Server to same client has been sent")]
                public bool loopback { get; set; }


                [Option( 'z' , "gzip", Default = false,  HelpText = "Compress and decompress data mode for more speed")]
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
            [Option('c', "Id list", Default = false, HelpText = "Id list")]
            public int idlist { get; set; }

            [Option('d', "Data", Default = false, HelpText = "Data string")]
            public string data { get; set; }
        }






    class Test
    {


        static  int port_tcp = 9999;
        static  int port_udp  = 9998;
        static ServerGame.Program.Connctions.Server server;

        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            
            while (true)
            {
       
                Console.Write("Server#: ");
                var consoleRead = Console.ReadLine();
                if (consoleRead.Trim().TrimStart().TrimEnd().Length != 0)
                {
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
        }

        private static  object RunExitReturnExitCode(Exit opts)
        {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Stopping Server...");
               if (server !=null)
                if (server.listeners != null) { 
                    foreach (var listener in server.listeners)
                    {
                        Console.WriteLine($"Connction is :{listener.Connected.ToString()} ,  Ip : {((IPEndPoint)listener.RemoteEndPoint).Address.ToString()} , port : {((IPEndPoint)listener.RemoteEndPoint).Port.ToString() } , SendTimeout : {listener.SendTimeout.ToString()}, ReceiveTimeout  : {listener.ReceiveTimeout.ToString()} :: closing listener ..");
                            listener.Close();

                        Console.WriteLine("Closed listener");

                        listener.Dispose();
                        Console.WriteLine("Dispose listener");
                    }

                        server.listeners.Clear();
                    }

                
         
            System.Environment.Exit(0);
            return null;
          
        }
        private static object RunSendReturnExitCode(sendstring opts)
        {
           
            if (server != null)
                if (server.listeners != null)
                {
                    server.Send(server.listeners[opts.idlist], opts.data);
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

            

                 foreach (var listener in server.listeners)
                 { 
                     Console.WriteLine($"Connction is :{listener.Connected.ToString()} , Ip : {((IPEndPoint)listener.LocalEndPoint).Port.ToString()} , Ip : {((IPEndPoint)listener.RemoteEndPoint).Address.ToString()} , port : {((IPEndPoint)listener.RemoteEndPoint).Port.ToString() } , SendTimeout : {listener.SendTimeout.ToString()}, ReceiveTimeout  : {listener.ReceiveTimeout.ToString()} ");
                if (opts.StopAll) {
                    listener.Shutdown(SocketShutdown.Both);
                     listener.Close(); }

                 }

                 server.listeners.RemoveAll(matching_listener_is_close);
       
        

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


  

}
