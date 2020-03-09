using ServerGame.Core.Connctions;
using ServerGame.Interface.Data;
using ServerGame.Interface.Room;
using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerGame.Core.User
{
   public class User : IUser
    {
        
        public string Name { get; set; }
        public decimal IdDataBase { get; set; }
        public  AbstractServer.StateObject Connction { get; protected set; }

        public IRoom Room { get; protected set; }
        public AbstractServer.StateObjectupb ConnctionUDP { get; set; }

        public AbstractServer.StateObject ConnctionTCP { get; set; }

        public int IdIntTempUDP { get; protected set; }

        public List<IUser> Friends { get; protected set; }

        public float x => throw new NotImplementedException();

        public float y => throw new NotImplementedException();

        public float z => throw new NotImplementedException();

        public int anmationWork => throw new NotImplementedException();

        public float anmationWorkTime => throw new NotImplementedException();

        public DateTime LastTimeReciver => throw new NotImplementedException();

        public User (string Name , decimal IdDataBase  , AbstractServer.StateObject Connction , ref IRoom Room , int IdIntTempUDP)
        {
            this.IdIntTempUDP = IdIntTempUDP;
            this.Name = Name;
            this.IdDataBase = IdDataBase;
            this.Connction = Connction;
            this.Room = Room;
            this.Friends = new List<IUser>();
        }

        public bool AddFriend(IUser user)
        {
            Friends.Add(user);
            return true;
        }

        public bool RemoveFriend(IUser user)
        {
            throw new NotImplementedException();
        }

        public bool AddFriends(List<IUser> users)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFriends(List<IUser> users)
        {
            throw new NotImplementedException();
        }
    }
}
