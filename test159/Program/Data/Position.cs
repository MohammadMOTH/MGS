using System;
using System.Collections.Generic;
using System.Text;
using ServerGame.Interface.Data;

namespace ServerGame.Program.Data
{
    public class Position : ICustomeData
    {
        private int _x;
        private int _y;

        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x,int y)
        {
            this._x = x;
            this._y = y;
        }
    }
}
