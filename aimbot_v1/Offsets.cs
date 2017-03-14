using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aimbot_v1
{
    static class Offsets
    {
        public static int oLocalPlayer = 0xAA66D4;
        public static int oEntityList = 0x4AC91B4;
        public static int oCrosshair = 0xAA70;
        public static int oDormant = 0x000000E9;
        public static int oTeam = 0xF0;
        public static int oHealth = 0x000000FC;
        public static int oAttack = 0x02E8CCD0;
        public static int oEntityListLoopDis = 0x10;
        public static int xpos = 0x0134;
        public static int ypos = 0x0138;
        public static int zpos = 0x013c;
        public static int F_angle = 0x0128;
        public static int S_angle = 0x012C;
    }
}
