using ServerGame.Core.Connctions;
using ServerGame.Interface.Room;
using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.User
{
    public class UserMaster
    {


        public static List<IUser> users = new List<IUser>();

        public static  IUser ReturnUserBy( string username )
        {

          /*  var    User=  users.Find(x => x.IdIntTempUDP == IdIntTempUDP);

            if (User == null)*/
           var  User = users.Find(x => x.Name == username);


            if (User == null)
              throw  new Core.GException.UserNotFind();

            return  User;
        

        }

        public static IUser AddNewUser(string Name, decimal IdDataBase, AbstractServer.StateObject Connction, ref IRoom Room, int IdIntTempUDP)
        {

            var user=  new Core.User.User(Name, IdDataBase, Connction, ref Room, IdIntTempUDP);
            users.Add(user);
            return user;

        }


        public static void ConfingAtho ()
        {



        }


    }
}
