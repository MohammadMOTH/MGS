using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.User
{
  public  class GroupFreinds : IGroup
    {
        public List<IUser> GetListUser(List<IUser> AllUser)
        {
            List<IUser> llistgroup = new List<IUser>();
            for (int i = 0; i < AllUser.Count; i++)
            {
                
                for (int j = 0; j < AllUser[i].Friends.Count; j++)
                {
                    llistgroup.Add(AllUser[i].Friends[j]);
                }
            }

            return llistgroup; 
        }
    }
}
