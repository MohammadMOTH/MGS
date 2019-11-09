using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.Room;
using ServerGame;
namespace ServerGame.Core.Room
{
    public abstract class RoomAbstract : IRoom
    {
        string IRoom.Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IRoom.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IRoom.MaxPlayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IPermissionsUser IRoom.PermissionsUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        List<IUser> IRoom.UserInRoom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime IRoom.DateStart { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime IRoom.DataEnd { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        List<Ievnet> IRoom._MyEvnets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        IZone IRoom.IZone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        bool IRoom.AddEventListener(Ievnet evnet)
        {
            Console.WriteLine("fuxxxck");
            return false;
        }

        bool IRoom.JoinUserToRoom(IUser User)
        {
            throw new NotImplementedException();
        }

        bool IRoom.Test()
        {
            throw new NotImplementedException();
        }

        bool IRoom._AddNewUser(IUser User)
        {
            throw new NotImplementedException();
        }

        bool IRoom._CheckBlackList(IUser InputUserToSendData)
        {
            throw new NotImplementedException();
        }

        bool IRoom._CheckPermissionsUser(IUser InputUserToSendData)
        {
            throw new NotImplementedException();
        }
    }
}
