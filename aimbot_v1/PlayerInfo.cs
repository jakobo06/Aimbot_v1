using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace aimbot_v1
{
    public class PlayerInfo
    {
        static VAMemory vam;
        public float X, Y, Z;
        public static int baseClient;
        public struct Angle
        {
            public float F, S;
        }

        public struct Player
        {
            public float X, Y, Z;
        }


        public PlayerInfo(VAMemory Vam)
        {
            GetModuleAddy();
            vam = Vam;
        }
        public Player GetMyPlayer() 
        {
            Player _temp;

            int address = baseClient + Offsets.oLocalPlayer;
            int LocalPLayer = vam.ReadInt32((IntPtr)address);

            address = LocalPLayer + Offsets.xpos;
            _temp.X = vam.ReadFloat((IntPtr)address);

            address = LocalPLayer + Offsets.ypos;
            _temp.Y = vam.ReadFloat((IntPtr)address);

            address = LocalPLayer + Offsets.zpos;
            _temp.Z = vam.ReadFloat((IntPtr)address) + 64.063312f;
            return _temp;
        }
        public void PrintMyPlayer()
        {
            Player _temp = GetMyPlayer();

            Console.WriteLine(_temp.X);
            Console.WriteLine(_temp.Y);
            Console.WriteLine(_temp.Z);

            Angle _Angle = GetMyAngle();
            Console.WriteLine(_Angle.F);
            Console.WriteLine(_Angle.S);

        }

        public void PrintEnemey(int count)
        {

            for (int i = 0; i < count; i++)
            {
                Player _enemey = GetEnemeyPlayer(i);

                Console.WriteLine(_enemey.X);
                Console.WriteLine(_enemey.Y);
                Console.WriteLine(_enemey.Z);
            }
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

        static Angle GetMyAngle()
        {
            Angle _temp;
            int address = baseClient + Offsets.oLocalPlayer;
            int LocalPLayer = vam.ReadInt32((IntPtr)address);

            address = LocalPLayer + Offsets.F_angle;
            _temp.F = vam.ReadFloat((IntPtr)address);

            address = LocalPLayer + Offsets.S_angle;
            _temp.S = vam.ReadFloat((IntPtr)address);

            return _temp;

        }

        //static FloatVector GetMyposition()
        //{

        //}

        static Player GetEnemeyPlayer(int i)
        {
            Player _Enemey;
            int address = baseClient + Offsets.oLocalPlayer;

             address = baseClient + Offsets.oEntityList + (i * Offsets.oEntityListLoopDis);
             int PictoPic = vam.ReadInt32((IntPtr)address);

             address = PictoPic + Offsets.xpos;
             _Enemey.X = vam.ReadFloat((IntPtr)address);

             address = PictoPic + Offsets.ypos;
             _Enemey.Y = vam.ReadFloat((IntPtr)address);

             address = PictoPic + Offsets.zpos;
             _Enemey.Z = vam.ReadFloat((IntPtr)address)+ 64.063312f;
            return _Enemey;
        }
    }
}
