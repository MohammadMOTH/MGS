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
        private int zone;
        private int id;
        private int dataend;
        private int datastart;
        private int maxplayers;
        private int name;
        private int permissionsuser;


        public abstract string Name { get; set; }
        public abstract int Id { get; set; }
        public abstract int MaxPlayers { get; set; }
        public abstract IPermissionsUser PermissionsUser { get; set; }
        public abstract List<IUser> UserInRoom { get; set; }
        public abstract DateTime DateStart { get; set; }
        public abstract DateTime DataEnd { get; set; }
        public abstract List<Ievnet> _MyEvnets { get; set; }
        public  IZone IZone { get; set; }

        public abstract bool AddEventListener(Ievnet evnet);
        public abstract bool JoinUserToRoom(IUser User);
        public abstract bool Test();
        public abstract bool _AddNewUser(IUser User);
        public abstract bool _CheckBlackList(IUser InputUserToSendData);
        public abstract bool _CheckPermissionsUser(IUser InputUserToSendData);
    }
}
