using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Program.Connctions
{
   public class Server : Core.Connctions.Server
    {


        public Server(int UDPPort, int TCPPort , bool loopback ,bool Gzip)
          : base(UDPPort, TCPPort , loopback , Gzip)
        {

        }



    }
}
