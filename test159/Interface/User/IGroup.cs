using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.User
{
  public interface IGroup
    {

         List<IUser> GetListUser(List<IUser> AllUser);

    }
}
