using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using ServerGame.Core.Connctions;
using ServerGame.Interface.Room;
namespace ServerGame.Interface.User
{
    public interface IUser 
    {
         List<IUser> Friends { get; }
        string Name { get;  }
        IRoom Room { get;  }
        decimal IdDataBase { get;  }

         float x{ get;  }
         float y { get; }
         float z { get; }

         int anmationWork { get; }

         float anmationWorkTime { get; }

         DateTime LastTimeReciver { get; }


         int IdIntTempUDP { get; }


         bool AddFriend(IUser user);
         bool RemoveFriend(IUser user);
         bool AddFriends(List<IUser> users);
         bool RemoveFriends(List<IUser> users);
        AbstractServer.StateObjectupb ConnctionUDP { get; set; }

        AbstractServer.StateObject ConnctionTCP { get; set; }


    }
}
