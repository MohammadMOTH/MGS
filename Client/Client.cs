using ServerGame.Core.Connctions;
using ServerGame.Core.Data;
using ServerGame.Core.Event;
using ServerGame.Core.EventStore;
using ServerGame.Core.GException;
using ServerGame.Interface.User;
using ServerGame.Program.Zone;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static ServerGame.Core.Connctions.AbstractServer;

namespace Client
{

    // State object for receiving data from remote device.  
  /*  public class StateObject
    {
        public Socket workSocket = null;
        public int BufferSize;
        public byte[] buffer;
        public StringBuilder sb = new StringBuilder();
        public object returnObjectFromEventPresor;
        public StateObject(int buffersize = 1024)
        {
            BufferSize = buffersize;


            buffer = new byte[BufferSize];
        }

    }

    public class StateObjectudp : StateObject
    {
        public StateObjectudp(int buffersize = 65507) : base(buffersize)
        {
            BufferSize = buffersize;


            buffer = new byte[BufferSize];
        }
    }*/

    public class AsynchronousClient
    {
        private static int porttcp = 9999;
        private static int portudp = 9998;

        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.  
        private static String response = String.Empty;
        private static string ip;
        private static int port = 9999;

        public static CancellationTokenSource startRciverUDPStop = new CancellationTokenSource();
      
        public static Socket clientudp;
        public static Socket clientreserudp;

        public static async void startRciverUDP()
        {
            try

            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(ip);//Dns.GetHostEntry("net.super-bots.com");
                IPAddress ipAddress = ipHostInfo.AddressList[ipHostInfo.AddressList.Length - 1];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, portudp);

                clientudp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                clientreserudp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


                List<Data> datalist = null;

                sendDone.Reset();
                datalist = new List<Data>();

                clientudp.Connect(remoteEP);
                clientreserudp.Bind(clientudp.LocalEndPoint);
                CancellationToken ct = startRciverUDPStop.Token;
                await Task.Run(() =>
                {
                    StateObjectupb state = new StateObjectupb();
                    state.workSocket = clientreserudp;
                    ct.ThrowIfCancellationRequested();

                    while (true)
                    {
                        if (ct.IsCancellationRequested)
                        {
                            ct.ThrowIfCancellationRequested();
                        }
                        receiveDone.Reset();
                        Receiveudp(ref state);
                        receiveDone.WaitOne();
                        if (state.returnObjectFromEventPresor != null)
                            Console.WriteLine("Response received udp : {0}", state.returnObjectFromEventPresor.ToString());
                        state.returnObjectFromEventPresor = null;
                    }
                }, startRciverUDPStop.Token);
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            finally
            {
                startRciverUDPStop.Dispose();
                startRciverUDPStop = new CancellationTokenSource();
                startRciverUDP();
                startSendUDP(new PackSendData(0, new List<Data>() { new Data((byte)0, 0) }));

            }
           

        }

        public static async void startSendUDP(PackSendData PackSendData)
        {
            await Task.Run(() =>
            {
                Send(clientudp, ServerGame.Core.Serialize.Serialize.serialize(PackSendData));
                sendDone.WaitOne();
            });
        }

        private static void StartClient()
        {
           

            EventStore.AddNewEvent(new ServerGame.Core.Event.LoopSendingUDP(OnLoopSedingUDP));

            EventStore.AddNewEvent(new ServerGame.Core.Event.Authtcation(OnAuthtcation));
            EventStore.AddNewEvent(new ServerGame.Core.Event.ConvertConnctionToNewServeerPort(OnConvertConnctionToNewServeerPort));
            EventStore.AddNewEvent(new ServerGame.Core.Event.Broadcast(OnBroadcast));

            Console.WriteLine("Please Enter IP(if not is same machen name) : ");
            ip = Console.ReadLine();
            if (ip == "")
                ip = Dns.GetHostName();

            var _tcp = true;
            Console.WriteLine("Please Enter false or 0 or press enter if use udp : ");
            var ss = Console.ReadLine();
            if (ss == "0" || ss == "false" || ss == "")
                _tcp = false;
            if (_tcp)
            {
                Console.WriteLine("enter port number deflut is : " + porttcp.ToString());
                var _port = Console.ReadLine();
                if (_port != "")
                    port = Convert.ToInt32(_port);
                else
                    port = porttcp;
            }
            else
            {
                Console.WriteLine("Please Enter port(if not,is 9998) : ");
                var _port = Console.ReadLine();
                if (_port != "")
                    port = Convert.ToInt32(_port);
                else
                    port = portudp;
            }
            try
            {
                if (_tcp)
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(ip);//Dns.GetHostEntry("net.super-bots.com");
                    IPAddress ipAddress = ipHostInfo.AddressList[ipHostInfo.AddressList.Length - 1];
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                    // Create a TCP/IP socket.  
                    Socket client = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);


                    client.BeginConnect(remoteEP,
                        new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();
                    if (client.Connected == false)
                        return;

                    Task.Run(() =>
                    {
                        while (true)
                        {
                            receiveDone.Reset();
                            Receive(client);
                            receiveDone.WaitOne();

                            Console.WriteLine("Response received : {0}", response);
                        }
                    });

                    PackSendData data = null;
                    List<Data> datalist = null;
                    while (true)
                    {
                        var readline = Console.ReadLine();

                        if (readline == "close")
                            break;

                        if (readline.Split(' ')[0].Trim() == "loop") // loop 500 20 mohmmadmaskdmqkmfwemgflwjnemgljnwejgjwhnglkwe
                        {
                            var manytime = Convert.ToInt32(readline.Split(' ')[1]);
                            var sleeptimeMilSec = Convert.ToInt32(readline.Split(' ')[2]);
                            var world = "";

                            for (int i = 3; i < readline.Split(' ').Length; i++)
                            {
                                world += " " + readline.Split(' ')[i];
                            }

                            while (manytime > 0)
                            {
                                sendDone.Reset();
                                datalist = new List<Data>();
                                datalist.Add(new Data(manytime.ToString() + world, (byte)DataType.String));

                                data = new PackSendData(1, datalist);
                                Send(client, ServerGame.Core.Serialize.Serialize.serialize(data));
                                sendDone.WaitOne();

                                manytime--;
                                Thread.Sleep(sleeptimeMilSec);

                            }




                        }else if (readline.Split(' ')[0].Trim() == "event") // loop 500 20 mohmmadmaskdmqkmfwemgflwjnemgljnwejgjwhnglkwe
                        {
                            var datasnding = readline.Split(' ');
                            var numberevnet = Convert.ToInt32(datasnding[1]);
                            var world = "";

                            for (int i = 2; i < readline.Split(' ').Length; i++)
                            {
                                world += " " + readline.Split(' ')[i];
                            }
                            switch (numberevnet)
                            {
                                case 1:
                                    datalist = new List<Data>();
                                    datalist.Add(new Data(datasnding[2], 0));
                                    datalist.Add(new Data(datasnding[3], 1));
                                    datalist.Add(new Data(datasnding[4], 2));
                                    datalist.Add(new Data(datasnding[5], 3));
                                    data = new PackSendData(1, datalist);
                                    Send(client, ServerGame.Core.Serialize.Serialize.serialize(data));
                                    break;

                                case 2:
                                    datalist = new List<Data>();
                                    datalist.Add(new Data(world, 0));
                                    data = new PackSendData(2, datalist);
                                    Send(client, ServerGame.Core.Serialize.Serialize.serialize(data));
                                    break;

                                default:
                                    break;
                            }




                        }
                        datalist = new List<Data>();
                        datalist.Add(new Data(readline, (byte)DataType.String));
                        data = new PackSendData(0, datalist);

                        Send(client, ServerGame.Core.Serialize.Serialize.serialize(data));
                        sendDone.WaitOne();


                    }
                    // Release the socket.  
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                else
                {
                  startRciverUDP();

                    while (true)
                    {

                    
                    var readline = Console.ReadLine();

                        if (readline == "stop")
                            break;
                    List<Data> datalist = null;
                    PackSendData data = null;

                    if (readline.Split(' ')[0].Trim() == "event")
                    {
                        var datasnding = readline.Split(' ');
                        var numberevnet = Convert.ToInt32(datasnding[1]);
                        var world = "";

                        for (int i = 2; i < readline.Split(' ').Length; i++)
                        {
                            world += " " + readline.Split(' ')[i];
                        }
                        switch (numberevnet)
                        {
                            case 1:
                                datalist = new List<Data>();
                                datalist.Add(new Data(datasnding[2], 0));
                                datalist.Add(new Data(datasnding[3], 1));
                                datalist.Add(new Data(datasnding[4], 2));
                                datalist.Add(new Data(datasnding[5], 3));
                                data = new PackSendData(1, datalist);
                                startSendUDP(data);
                                break;

                            case 2:
                                datalist = new List<Data>();
                                datalist.Add(new Data(world, 0));
                                data = new PackSendData(2, datalist);
                                startSendUDP(data);
                                break;

                            default:
                                break;
                        }


                    }
                    else if (readline.Split(' ')[0].Trim() == "loop")
                    {
                        var manytime = Convert.ToInt32(readline.Split(' ')[1]);
                        var sleeptimeMilSec = Convert.ToInt32(readline.Split(' ')[2]);
                        var world = "";

                        for (int i = 3; i < readline.Split(' ').Length; i++)
                        {
                            world += " " + readline.Split(' ')[i];
                        }



                        while (manytime > 0)
                        {
                            sendDone.Reset();
                            datalist = new List<Data>();
                            datalist.Add(new Data(manytime.ToString() + world, (byte)DataType.String));

                            data = new PackSendData(0, datalist);
                            startSendUDP( data);
                            sendDone.WaitOne();

                            manytime--;
                            Thread.Sleep(sleeptimeMilSec);

                        }

                    }
                    else
                    {
                        datalist = new List<Data>();
                        datalist.Add(new Data(readline,0));

                        data = new PackSendData(0, datalist);

                        startSendUDP(data);
                    }

                    }

                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private static void OnAuthtcation(object arg1, ResEventArgs arg2)
        {
            throw new NotImplementedException();
        }

        private static void OnConvertConnctionToNewServeerPort(object arg1, ResEventArgs arg2)
        {
            try
            {

                if (arg2.PackSendData.AllData.Count < 2)
                {

                    throw new Exception("Atho Function Not Count is " + arg2.PackSendData.AllData.Count.ToString() + " Must be or more 2 ");
                }

                var IdIntTempUDP = Convert.ToInt32(arg2.PackSendData.AllData.Find(x => x.PramterName == 0).DataS);
                var Port = Convert.ToInt32(arg2.PackSendData.AllData.Find(x => x.PramterName == 1).DataS);

                portudp = Port;
              
                clientreserudp.Dispose();

             
                clientudp.Dispose();

                startRciverUDPStop.Cancel();
                
                receiveDone.Set();

                



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


        private static void OnBroadcast(object arg1, ResEventArgs arg2)
        {
            try
            {

                if (arg2.PackSendData.AllData.Count < 2)
                {

                    throw new Exception("Atho Function Not Count is " + arg2.PackSendData.AllData.Count.ToString() + " Must be or more 2 ");
                }
                for (int i = 0; i < arg2.PackSendData.AllData.Count; i++)
                {
                    Console.WriteLine(arg2.PackSendData.AllData[i].DataS.ToString());
                }
             
              


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

        private static void OnLoopSedingUDP(object arg1, ResEventArgs arg2)
        {
            try
            {
                Console.WriteLine(arg2.PackSendData.AllData[0].DataS.ToString());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
         
        }

        private static void ConnectCallback(IAsyncResult ar)
        {

            try
            {

                Socket client = (Socket)ar.AsyncState;


                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());


                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                connectDone.Set();
            }
        }

        private static void Receive(Socket client)
        {
            try
            {

                StateObject state = new StateObject();
                state.workSocket = client;


                client.BeginReceive(state.buffer, 0, state.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
        }
        private static void Receiveudp(ref ServerGame.Core.Connctions.AbstractServer.StateObjectupb state)
        {
            try
            {
                state.workSocket.BeginReceive(state.buffer, 0, state.BufferSize, 0,
                    new AsyncCallback(ReceiveCallbackudp), state);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }


        }

        private static void ReceiveCallbackudp(IAsyncResult ar)
        {
            try
            {

                var state = (ServerGame.Core.Connctions.AbstractServer.StateObject)ar.AsyncState;
                Socket client = state.workSocket;


                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    PackSendData PackSendData = ServerGame.Core.Serialize.DeSerialize.Deserialize(state.buffer);
                    ServerGame.Core.EventStore.EventStore.Parser(PackSendData, ref state, ServerGame.Interface.Connctions.ConnctionType.UDP);



                }




            }
            catch (Exception e)
            {


            }
            receiveDone.Set();

        }
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (ServerGame.Core.Connctions.AbstractServer.StateObject)ar.AsyncState;
                Socket client = state.workSocket;


                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    PackSendData PackSendData = ServerGame.Core.Serialize.DeSerialize.Deserialize(state.buffer);
                    ServerGame.Core.EventStore.EventStore.Parser(PackSendData, ref state, ServerGame.Interface.Connctions.ConnctionType.TCP);




                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            receiveDone.Set();

        }


        private static void Send(Socket client, byte[] byteData)
        {
            // Convert the string data to byte data using ASCII encoding.  

            byte[] sizeOfByteData = BitConverter.GetBytes(byteData.Length);
            byte[] SendingData = new byte[byteData.Length + sizeOfByteData.Length];

            Array.Copy(byteData, 0, SendingData, sizeOfByteData.Length, byteData.Length);
            Array.Copy(sizeOfByteData, 0, SendingData, 0, sizeOfByteData.Length);
            ///TODO :Remove all byte arrays
            // Begin sending the data to the remote device.  
            //   client.BeginSend(SendingData, 0, SendingData.Length, 0,
            //         new AsyncCallback(SendCallback), client);

            client.BeginSend(SendingData, 0, SendingData.Length, SocketFlags.None,
             new AsyncCallback(SendCallback), client);

        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {

                Socket client = (Socket)ar.AsyncState;


                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);


                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static int Main(String[] args)
        {

            StartClient();
            return 0;
        }
    }
}
