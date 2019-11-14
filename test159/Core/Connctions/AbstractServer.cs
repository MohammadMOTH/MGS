using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerGame.Interface.Connctions;
using System.IO.Compression;
using System.IO;
using ServerGame.Core.Data;

namespace ServerGame.Core.Connctions
{
    public abstract class AbstractServer : IServer
    {

        public class StateObject
        {

            public Socket workSocket ;

            public const int BufferSize = 1024;
 
            public byte[] buffer = new byte[BufferSize];
            //    public StringBuilder sb = new StringBuilder();
            public List<byte> ListOfByteReciver = new List<byte>();

            public int alldatasqmant = 0;
            public int Nowdatasqmant = 0;

            public ManualResetEvent StopAndLoopReading = new ManualResetEvent(false);


        }
        /// <summary>
        /// listeners pointer looping
        /// </summary>
        public ManualResetEvent StopAndLoop = new ManualResetEvent(false);
       // public ManualResetEvent StopAndLoopudb = new ManualResetEvent(false);
        
        public bool Enblie_Gzip { get; protected set; }
        /// <summary>
        /// udp port
        /// </summary>
        public int UDPPort {  get; protected set; }
        /// <summary>
        /// save here all connctions tpc
        /// </summary>
        public List<Socket> listeners { get; protected set; }
        /// <summary>
        /// tcp prot
        /// </summary>
        public int TCPPort { get; protected set; }

        /// <summary>
        /// replay form sender to looping<
        /// </summary>
        public bool LoopBack { get; protected set; }


        /// <summary>
        /// constructor init for class  
        /// </summary>
        /// <param name="UDPPort"> udp port </param>
        /// <param name="TCPPort">tcp port</param>
        /// <param name="LoopBack"> replay form sender to looping</param>
        /// <param name="Enblie_Gzip">commpress data and decompress mode </param>
        public AbstractServer(int UDPPort, int TCPPort ,bool LoopBack = false , bool Enblie_Gzip = false )
        {
            listeners = new List<Socket>();
            this.UDPPort = UDPPort;
            this.TCPPort = TCPPort;
            this.LoopBack = LoopBack;
            this.Enblie_Gzip = Enblie_Gzip;
        }

        public abstract void TcpSendData(IData Data, IUser User);
        public abstract void UdpSendData(IData Data, IUser User);

        /// <summary>
        /// Start Listeing 
        /// </summary>
        /// <returns></returns>
        public virtual async Task Start()
        {

            try
            {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); 
            IPAddress ipAddress = ipHostInfo.AddressList[1]; 
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, TCPPort);
        
             var listener = new Socket
                    (ipAddress.AddressFamily,
                    SocketType.Stream, 
                    ProtocolType.Tcp);
            
                listener.Bind(localEndPoint);
               listener.Listen(5000);
                await Task.Run(() =>
                {
                    while (true)
                    { 
                        Console.WriteLine("Waiting ..." );
                         StopAndLoop.Reset(); 
                          listener.BeginAccept(
                              new AsyncCallback(AcceptCallback),
                              listener);
                              StopAndLoop.WaitOne();

                        /*
                      StopAndLoopudb.Reset();
                      var state = new StateObject();

                      listener.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                      StopAndLoopudb.WaitOne();
                       */


                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }
        #region internal_Method


        protected virtual void AcceptNewConnction(ref Socket handler ,ref object[] objects)
        {



        }

        public static byte[] Serialize(object anySerializableObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                (new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()).Serialize(memoryStream, anySerializableObject);
                return memoryStream.ToArray();
            }
        }

        public static B Deserialize<B>( byte[] bytes )
        {
            using (var memoryStream = new MemoryStream(bytes))
                return (B) (new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()).Deserialize(memoryStream);
        }


        protected bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);

            if (part1 && part2)
                return false;
            else
                return true;

        }
        /// <summary>
        /// Accept Call back Tcp
        /// </summary>
        /// <param name="ar">its for handle async with StateObject class </param>
        protected  void AcceptCallback(IAsyncResult ar)
        {
              
            StopAndLoop.Set();

           
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            listeners.Add(handler);
                
            StateObject state = new StateObject();
            state.workSocket = handler;
          

            Task.Run(() =>
            {
                agen:

                state.StopAndLoopReading.Reset();
              if (   SocketConnected (state.workSocket) )
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                state.StopAndLoopReading.WaitOne();

                goto agen;
            });




        }
        /// <summary>
        /// Waiting byte form client 
        /// </summary>
        /// <param name="ar">its for handle async with StateObject class </param>
        protected  void ReadCallback(IAsyncResult ar)
        {
            //  StopAndLoopudb.Set();
            StateObject state = (StateObject)ar.AsyncState;
            try
            {
                String content = String.Empty;
                
            
            
          /*      if (state.alldatasqmant == 0) { 
                state.ListOfByteReciver.Clear(); // اتوقع هذا افضل من ناحية الذاكرة
               // state.sb = new StringBuilder();
                }*/


                if (!SocketConnected (state.workSocket))
                   return;
                // Read data from the client socket.   
                int bytesRead = state.workSocket.EndReceive(ar);
            
                if (bytesRead > 0)
                {
                    HaveMorData:

                    if (state.alldatasqmant == 0)
                    {
                        if (state.ListOfByteReciver.Count == 0)
                        {
                            state.Nowdatasqmant += bytesRead - 4;
                            byte[] maxbytes = new byte[4];
                           
                            Array.Copy(state.buffer, 0, maxbytes, 0, 4);

                            state.alldatasqmant = BitConverter.ToInt32(maxbytes, 0);

                            
                            for (int i = 4; i < bytesRead; i++)
                            {
                                state.ListOfByteReciver.Add(state.buffer[i]);
                            }
                            Array.Clear(state.buffer, 0, state.buffer.Length);
                            bytesRead -= 4;
                            Array.Clear(maxbytes, 0, maxbytes.Length);
                            maxbytes = null;
                            Array.Clear(state.buffer, 0, state.buffer.Length);

                           

                        }
                        else
                        {
                            state.Nowdatasqmant += bytesRead - 4;
                            byte[] maxbytes = new byte[4];

                            for (int i = 0; i < bytesRead; i++)
                            {
                                state.ListOfByteReciver.Add(state.buffer[i]);
                            }
                            Array.Clear(state.buffer, 0, state.buffer.Length);

                            Array.Copy(state.ListOfByteReciver.GetRange(0,4).ToArray(), 0, maxbytes, 0, 4);
                            state.alldatasqmant = BitConverter.ToInt32(maxbytes, 0);

                            state.ListOfByteReciver.RemoveRange(0, 4);

                            Array.Clear(maxbytes, 0, maxbytes.Length);
                            maxbytes = null;
                            Array.Clear(state.buffer, 0, state.buffer.Length);
                            bytesRead -= 4;


                        }
                    }
                    else
                    {
                        state.Nowdatasqmant += bytesRead;


                        for (int i = 0; i < bytesRead; i++)
                        {
                            state.ListOfByteReciver.Add(state.buffer[i]);
                        }
                        Array.Clear(state.buffer, 0, state.buffer.Length);
                    }

                  
                        if (!this.Enblie_Gzip)
                        {

                          
                           

                        }
                        else
                        {
                        /*
                            byte[] temp = Decompress(state.buffer);
                            state.ListOfByteReciver.Append(Encoding.ASCII.GetString(temp, 0, temp.Length));
                             Array.Clear(temp, 0, temp.Length);*/
                        }

                    if (state.alldatasqmant == state.Nowdatasqmant)
                    {
                      
                           var temp = state.ListOfByteReciver.ToArray();
                        PackSendData PackSendData = Deserialize<PackSendData>(temp);
                        EventStore.EventStore.Parser(PackSendData, ref state);

                        Array.Clear(temp, 0, temp.Length);
                      
                        temp = null;


                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        state.alldatasqmant, content);
                        state.alldatasqmant = 0;
                        state.Nowdatasqmant = 0;
                        state.ListOfByteReciver.Clear();
                    }


                    else if (state.alldatasqmant < state.Nowdatasqmant && state.ListOfByteReciver.Count >= state.alldatasqmant)
                    {
                        
                        var temp = state.ListOfByteReciver.GetRange(0, state.alldatasqmant ).ToArray();

                        PackSendData PackSendData = Deserialize< PackSendData>(temp);
                        EventStore.EventStore.Parser( PackSendData,ref state);

                        Array.Clear(temp, 0, temp.Length);

                        temp = null;


                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        state.alldatasqmant, content);
                        state.ListOfByteReciver.RemoveRange(0, state.alldatasqmant);
                        
                        state.Nowdatasqmant -= state.alldatasqmant;
                        state.alldatasqmant = 0;
                        bytesRead = 0;

                        goto HaveMorData;
                    }

                    if (SocketConnected (state.workSocket)) // TODO :تعديل هنا لتحديد هل هناك اتصال ام لا 
                        if (LoopBack)
                          Send(state.workSocket, content);
                            

                }
                
              

            } catch (Exception e )
            {
                Console.WriteLine(e.ToString());

            }
            state.StopAndLoopReading.Set();
        }

        /// <summary>
        /// Compress Data Gzip
        /// </summary>
        /// <param name="raw"> Byte array for gzip </param>
        /// <returns></returns>
        protected virtual byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory,
                    CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }

        /// <summary>
        /// Decompress data gzip type
        /// </summary>
        /// <param name="gzip"> byte array has been gzip alreader </param>
        /// <returns></returns>
        protected virtual byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
                CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
                
            }
        }
        /// <summary>
        /// send data by Socket handler 
        /// </summary>
        /// <param name="handler">Socket handler  </param>
        /// <param name="data"> String data to send </param>
        public virtual void Send(Socket handler, String data)
        {
            byte[] byteData;
            if (this.Enblie_Gzip)
              byteData = Compress(Serialize(data));
            else
                byteData = Serialize(data);

            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);

        }

        /// <summary>
        /// Send Call back  async 
        /// </summary>
        /// <param name="ar">its for handle async with StateObject class</param>
        protected virtual void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //  handler.Shutdown(SocketShutdown.Both);
                //  handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    
        /// <summary>
        /// send data for user and data class 
        /// </summary>
        /// <param name="Data"> data class for sending </param>
        /// <param name="User">user for sending this data </param>
        /// <param name="ConnctionType">tcp or udp</param>
        protected virtual void _SendData(IData Data, IUser User, ConnctionType ConnctionType)
        {


        }

        #endregion

    }
}
