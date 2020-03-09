﻿using ServerGame.Interface.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGame.Interface.Permissions
{
   public interface IPermissionsZone : IPermissions
    {
     
        bool CanAddNewZoom { get; }

    }
}
