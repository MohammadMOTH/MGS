using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Permissions;
using ServerGame.Program.Event;
using ServerGame.Interface.Event;

namespace ServerGame.Program.Room
{
   public class Room : Core.Room.RoomAbstract
    {
        public override string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override int MaxPlayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IPermissionsUser PermissionsUser { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override List<IUser> UserInRoom { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override DateTime DateStart { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override DateTime DataEnd { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override List<Ievnet> _MyEvnets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
        public Room ()
        {
        


        }
        public override bool AddEventListener(Ievnet evnet)
        {
            throw new NotImplementedException();
        }

        public override bool JoinUserToRoom(IUser User)
        {
            throw new NotImplementedException();
        }

        public override bool Test()
        {
            throw new NotImplementedException();
        }

        public override bool _AddNewUser(IUser User)
        {
            throw new NotImplementedException();
        }

        public override bool _CheckBlackList(IUser InputUserToSendData)
        {
            throw new NotImplementedException();
        }

        public override bool _CheckPermissionsUser(IUser InputUserToSendData)
        {
            throw new NotImplementedException();
        }

        public void OnMessageSent(object source, MessageSenderEventArgs args)
        {
            Console.WriteLine("Message Service: Sending An Email...." + args.Message.message);
        }
    }
}
