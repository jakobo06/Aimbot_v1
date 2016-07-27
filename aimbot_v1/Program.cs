using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace aimbot_v1
{
    public class Program
    {
        public VAMemory vam = new VAMemory("csgo");
        public static int baseClient, baseEngine;


        static void Main(string[] args)
        {
            VAMemory vam = new VAMemory("csgo");

        }

        static bool GetModuleAddy()
        {
            try
            {
                Process[] p = Process.GetProcessesByName("csgo");

                if (p.Length > 0)
                {
                    foreach (ProcessModule m in p[0].Modules) // kikker igennem alle filer under csgo (process)
                    {
                        if (m.ModuleName == "client.dll")
                        {
                            baseClient = (int)m.BaseAddress; // vi får addressen af client.dll, som er vores base address.
                            return true;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    static Player GetMyPlayer(Player Player)
    {
        int address = baseClient + Offset.oLocalPlayer;
        int LocalPLayer = vam.ReadInt32((IntPtr)address);

        address = LocalPLayer + Offset.xpos;
        Player.X = vam.ReadFloat((IntPtr)address);

        address = LocalPLayer + Offset.ypos;
        Player.Y = vam.ReadFloat((IntPtr)address);

        address = LocalPLayer + Offset.zpos;
        Player.Z = vam.ReadFloat((IntPtr)address);
    }

    static angle GetMyAngle()
    {

    }

    static FloatVector GetMyposition()
    {

    }

    static Player GetEnemeyPlayer()
    {

    }

    struct Player
    {
        public float X, Y, Z;
    }

    struct angle
    {
        public float X, y;
    }

    struct Vector
    {
        public int X, Y, Z;
    }

    struct FloatVector
    {
        public float X, Y, Z;
    }

    public class Offset
    {
        public static int oLocalPlayer = 0x00A31504;
        public static int oEntityList = 0x04A4CCC4;
        public static int oCrosshair = 0x0000AA44;
        public static int oTeam = 0x0F0;
        public static int oHealth = 0x000000FC;
        public static int oAttack = 0x02E8CCD0;
        public static int oEntityListLoopDis = 0x10;
        public static int xpos = 0x0134;
        public static int ypos = 0x0138;
        public static int zpos = 0x013c;
    }
}