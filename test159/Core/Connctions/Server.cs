using ServerGame.Interface.Connctions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServerGame.Core.Connctions
{
  public  class Server : AbstractServer
    {

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

            #if DEBUG
            Console.WriteLine("init server .....");
            #endif

        }

        /// <summary>
        /// it start stating server listening 
        /// </summary>
        /// <returns></returns>
        public override async Task Start()
        {
           await base.Start();


            #if DEBUG
            Console.WriteLine("Start Server .....");

            #endif

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
