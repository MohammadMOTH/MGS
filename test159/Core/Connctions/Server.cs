using ServerGame.Interface.Connctions;
using ServerGame.Interface.Data;
using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerGame.Core.Connctions
{
  public  class Server : AbstractServer
    {

        public static Server ServerOject;

        public static List<ServerGame.Interface.Zone.IZone> ZoneWorking = new List<Interface.Zone.IZone>();
        public static List<ServerGame.Interface.Room.IRoom> RoomWorking = new List<Interface.Room.IRoom>();

        public bool lastupdate = false;
        /// <summary>
        /// constructor init for class  
        /// </summary>
        /// <param name="UDPPort"> udp port </param>
        /// <param name="TCPPort">tcp port</param>
        /// <param name="LoopBack"> replay form sender to looping</param>
        /// <param name="Enblie_Gzip">commpress data and decompress mode </param>
        public Server(int UDPPort, int TCPPort , bool LoopBack = false , bool Enblie_Gzip = false)
          : base(UDPPort,TCPPort , LoopBack ,  Enblie_Gzip )
        {

            ServerOject = this;

            #if DEBUG
            Console.WriteLine("init server .....");
            #endif
            lastupdate = true;
        }



        /// <summary>
        /// it start stating server listening 
        /// </summary>
        /// <returns></returns>
        public override async Task Start()
        {
           base.Start();
             this.StartUdp();

            #if DEBUG
            Console.WriteLine("Start Server .....");

            #endif

        }
        

        public async Task StartUdp()
        {
            try
            {
                #if DEBUG
                    Console.WriteLine("Start Server .....");

                #endif
             //   IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
              IPAddress ipAddress = IPAddress.Any;
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, UDPPort);

                var listener = new Socket
                       (
                       ipAddress.AddressFamily
                       ,
                       SocketType.Dgram
                       ,
                       ProtocolType.Udp
                       );

                listener.Bind(localEndPoint);

              
                Task.Run(() =>
                {
                agen:
                    StateObjectupb state = new StateObjectupb();
                    state.workSocket = listener;
                    state.StopAndLoopReading.Reset();
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint tempRemoteEP = (EndPoint)sender;
                    if (SocketConnected(state.workSocket))
                        listener.BeginReceiveFrom(state.buffer, 0, state.BufferSize, 0, ref tempRemoteEP, new AsyncCallback(ReadCallbackudp) ,state );

                         state.StopAndLoopReading.WaitOne();

                    goto agen;
                });


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }



        }

        /// <summary>
        /// send data tcp to user 
        /// </summary>
        /// <param name="Data">data class for send data</param>
        /// <param name="User">user class for get id tcp </param>
        public override void TcpSendData(IData Data, IUser User)
        {
           
           
        }

        /// <summary>
        /// send data udp to user 
        /// </summary>
        /// <param name="Data">data class for send data</param>
        /// <param name="User">user class for get id tcp </param>
        public override void UdpSendData(IData Data, IUser User)
        {

        
            throw new NotImplementedException();
        }


    }
}
