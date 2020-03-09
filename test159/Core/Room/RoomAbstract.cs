using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Interface.Room;
using ServerGame;
using ServerGame.Interface.Event;
using ServerGame.Interface.User;
using ServerGame.Interface.Zone;
using ServerGame.Core.Connctions;
using ServerGame.Interface.Connctions;

namespace ServerGame.Core.Room
{
    public abstract class RoomAbstract : IRoom
    {

        
        public string Name { get; protected set; }
        public int Id { get; protected set; }
        public int MaxPlayers { get; protected set; }
        protected IPermissionsRoom _IPermissionsRoom;
        public ref IPermissionsRoom IPermissionsRoom { get { return ref _IPermissionsRoom; } }
        public List<IUser> UserInRoom { get; protected set; }
        public DateTime DateStart { get; protected set; }
        public DateTime DataEnd { get; protected set; }

        protected IZone _Zone;
        public ref IZone Zone
        {
            get
            {
                return ref _Zone;
            }

        }
        public RoomAbstract(string Name, int Id, int MaxPlayers, DateTime DateStart, DateTime DataEnd, ref IZone IZone, ref IPermissionsRoom IPermissionsRoom)
        {

            this.Name = Name;
            this.Id = Id;
            this.MaxPlayers = MaxPlayers;
            this._IPermissionsRoom = IPermissionsRoom;
            this.UserInRoom = UserInRoom;
            this.DateStart = DateStart;
            this.DataEnd = DataEnd;
            this._Zone = IZone;

        }


        public bool JoinUserToRoom( IUser User)
        {

            UserInRoom.Add(User);

            return true;
        }

        public void ChangeName(string ChangeName)
        {
            Name = ChangeName;

        }
        public bool AddNewUser( IUser User)
        {
            JoinUserToRoom( User);
            return true;
        }

        public bool CheckBlackList(IUser InputUserToSendData)
        {
            throw new NotImplementedException();
        }

        public bool CheckPermissionsUser(IUser InputUserToSendData)
        {
            throw new NotImplementedException();
        }
        public bool SendToAllUsers(ServerGame.Core.Connctions.PackSendData PackSendData , Interface.Connctions.ConnctionType ConnctionType  , bool sendtoself , IUser userSender)
        {
            try
            {
                byte pramterhi = 0;
                foreach (var data in PackSendData.AllData)
                {
                    if (pramterhi < data.PramterName)
                        pramterhi = data.PramterName;
                }
                PackSendData.AllData.Add(new Data.Data(userSender.Name, ++pramterhi));

                
                    if (ServerGame.Interface.Connctions.ConnctionType.UDP == ConnctionType )
                    {
                        foreach (var user in this.UserInRoom)
                        {
                            if (  user.ConnctionUDP.IPEndPointUDP != null)
                                if (userSender != user || sendtoself)
                                {
                                    //  PackSendData.AllData.Find(x => x.PramterName == pramterhi).DataSChange(userSender.Name);
                                    Connctions.Server.ServerOject.Sendudp(user.ConnctionUDP.workSocket, PackSendData, user.ConnctionUDP.IPEndPointUDP);
                                }
                        }
                    }
                   else if (ServerGame.Interface.Connctions.ConnctionType.TCP == ConnctionType)
                   {
                        foreach (var user in this.UserInRoom)
                        {
                            if (!user.ConnctionTCP.workSocket.Poll(1000, System.Net.Sockets.SelectMode.SelectRead) || ! (user.ConnctionTCP.workSocket.Available == 0 ) )
                            if ( userSender != user || sendtoself  )
                            {
                                //  PackSendData.AllData.Find(x => x.PramterName == pramterhi).DataSChange(userSender.Name);
                            
                                Connctions.Server.ServerOject.Send(user.ConnctionTCP.workSocket, PackSendData);
                            }
                        }

                   }
                return true;
            }
            catch (Exception e )
            {

                Console.WriteLine(e.ToString());
                return false;
            }
        }
        public bool SendToUser(ServerGame.Core.Connctions.PackSendData PackSendData, IUser User)
        {
            throw new NotImplementedException();

        }
        public bool SendToUsers(ServerGame.Core.Connctions.PackSendData PackSendData, List<IUser> ListUsers)
        {
            throw new NotImplementedException();
        }
        public bool SendToAllZone(ServerGame.Core.Connctions.PackSendData PackSendData)
        {
            throw new NotImplementedException();
        }

        public bool SendToUsers(PackSendData PackSendData, List<IUser> ListUsers, ConnctionType ConnctionType)
        {
            throw new NotImplementedException();
        }

        public bool SendToUser(PackSendData PackSendData, IUser User, ConnctionType ConnctionType)
        {
            throw new NotImplementedException();
        }

        public bool SendToAllZone(PackSendData PackSendData, ConnctionType ConnctionType)
        {
            throw new NotImplementedException();
        }

        public bool SendToGroupUser(PackSendData PackSendData, IGroup group, ConnctionType ConnctionType)
        {
            throw new NotImplementedException();
        }
    }
}
