using ServerGame.Core.Connctions;
using ServerGame.Core.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{

    // State object for receiving data from remote device.  
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 256; 
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousClient
    {
        private static int port = 9999;
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.  
        private static String response = String.Empty;

        private static void StartClient()
        {
            Console.WriteLine("Please Enter IP(if not is same machen name) : ");
            var ip = Console.ReadLine();
            if (ip == "")
                ip = Dns.GetHostName();
            Console.WriteLine("Please Enter port(if not,is 9999) : ");
            var _port = Console.ReadLine();
            if (_port != "")
                port =  Convert.ToInt32(_port);
         


            try
            {
                IPHostEntry ipHostInfo =Dns.GetHostEntry(ip);//Dns.GetHostEntry("net.super-bots.com");
                IPAddress ipAddress = ipHostInfo.AddressList[ipHostInfo.AddressList.Length-1];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

               
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
                if (client.Connected == false)
                    return;

                Task.Run(() => {
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

                    if (readline.Split(' ')[0].Trim() == "loop")
                    {
                        var manytime = Convert.ToInt32( readline.Split(' ')[1]);
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
                            datalist.Add(new Data(world, "string"));

                             data = new PackSendData("PostionTeset", datalist);
                            Send(client, Serialize(data));
                            sendDone.WaitOne();

                            manytime--;
                            Thread.Sleep(sleeptimeMilSec);

                        }



                        
                    }
                     datalist = new List<Data>();
                    datalist.Add(new Data(readline, "string"));
                    data = new PackSendData("Chat", datalist);

                    Send(client, Serialize(data));
                    sendDone.WaitOne();


                }
                // Release the socket.  
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
        public static byte[] Serialize(object anySerializableObject)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                (new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return memoryStream.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            using (var memoryStream = new System.IO.MemoryStream(bytes))
                return (T)(new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()).Deserialize(memoryStream);
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


                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

         
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
              
                      state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    response = state.sb.ToString();
                  

                }
              receiveDone.Set();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                receiveDone.Set();
            }
        }

        private static void Send(Socket client, byte[] byteData )
        {
            // Convert the string data to byte data using ASCII encoding.  
         
            byte[] sizeOfByteData = BitConverter.GetBytes(byteData.Length);
            byte[] SendingData = new byte[byteData.Length + sizeOfByteData.Length];

            Array.Copy(byteData, 0, SendingData, sizeOfByteData.Length, byteData.Length);
            Array.Copy(sizeOfByteData, 0, SendingData, 0, sizeOfByteData.Length);
            ///TODO :Remove all byte arrays
            // Begin sending the data to the remote device.  
            client.BeginSend(SendingData, 0, SendingData.Length, 0,
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
