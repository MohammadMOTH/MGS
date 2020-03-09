using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServerGame;
using ServerGame.Interface.Data;
using ServerGame.Interface.User;
using static ServerGame.Core.Connctions.AbstractServer;

namespace ServerGame.Interface.Connctions
{

    /// <summary>
    /// Interface of server
    /// </summary>
    /// <remarks>
    /// Interface of server . we mange here all of commnction to Clint .
    /// it've tpc and udp ports . send data asyn tcp and upd .
    /// list of clint udp and tcp . and it ref to user to porwer full manger connctions
    /// </remarks>
  interface IServer
    {

         int UDPPort { get;  }
         int TCPPort { get; }
         bool Enblie_Gzip { get; }
        bool LoopBack { get; }

        List<Socket> ListenersTCP { get; }
        List<Socket> ListenersUDP { get; }


        #region Public_Method

        Task Start();
         void TcpSendData(IData Data, IUser User);
         void UdpSendData(IData Data, IUser User);

        bool WatchUdpByPort(ref StateObjectupb StateObjectupb,out int port, in int portIn);
        #endregion

    }




}
