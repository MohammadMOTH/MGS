using ServerGame.Interface.Permissions;
using ServerGame.Interface.Room;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Core.Zone
{
    public class Lobby  : ZoneAbstract
    {
  
        public Lobby() :base (
        "Lobby",
        -1 , 15 , true , 
        false , false , 
        false, true , 5000 ,
        1000 , true 
        )
        {
        
        }


    }
}
