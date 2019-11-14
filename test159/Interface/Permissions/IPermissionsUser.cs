using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Permissions
{
  public  interface IPermissionsUser : IPermissions
    {


        
        bool CheckUser(string UserName, string Password);

    


        bool AddNewUser(IUser User, string UserName, string Password);

    }
}
