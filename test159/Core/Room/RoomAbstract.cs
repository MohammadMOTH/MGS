using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.Room;
using ServerGame;
using ServerGame.Interface.Event;

namespace ServerGame.Core.Room
{
    public abstract class RoomAbstract : IRoom
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int MaxPlayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IPermissionsUser PermissionsUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IUser> UserInRoom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateStart { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DataEnd { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<Ievnet<object>> _MyEvnets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IZone IZone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool AddEventListener(Ievnet<object> evnet)
        {
            throw new NotImplementedException();
        }

        public bool JoinUserToRoom(IUser User)
        {
            throw new NotImplementedException();
        }

        public bool Test()
        {
            throw new NotImplementedException();
        }

        public bool _AddNewUser(IUser User)
        {
            throw new NotImplementedException();
        }

        public bool _CheckBlackList(IUser InputUserToSendData)
        {
            throw new NotImplementedException();
        }

        public bool _CheckPermissionsUser(IUser InputUserToSendData)
        {
            throw new NotImplementedException();
        }
    }
}
