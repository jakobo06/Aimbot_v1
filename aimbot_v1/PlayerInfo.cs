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
        AngleParser parser = new AngleParser();

        public struct Angle
        {
            public double F, S;
        }

        public struct Player
        {
            public double X, Y, Z;
            public int Enemey_Team, MyTeam;
        }


        public PlayerInfo(VAMemory Vam)
        {
            GetModuleAddy();
            vam = Vam;
        }

        public Player GetMyPlayer()
        {
            Player _temp = new Player();

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

        public void PrintDistBetween2Players(int i)
        {
            Player _MyPLayer = GetMyPlayer();
            Player _Enemey = GetEnemeyPlayer(i);
            Console.WriteLine(DistBetween2Players(_MyPLayer, _Enemey)); 
        }


        public void testangel()
        {
            GetEnemeyPlayer(2);
            GetMyPlayer();

            Angle test = calangle(GetMyPlayer(), GetEnemeyPlayer(1));
            Console.WriteLine(test.F);
            Console.WriteLine(test.S);
        }

        public Angle calangle(Player Myplayer, Player Enemy)
        {
            Angle _temp;
            double[] dist = { (Myplayer.X - Enemy.X), (Myplayer.Y - Enemy.Y),(Myplayer.Z - Enemy.Z) };
            double hyp = Math.Sqrt(dist[0] * dist[0] + dist[1] * dist[1]);
            _temp.F = (double)(Math.Asin(dist[2] / hyp) * 57.295779513082f);
            _temp.S = (double)(Math.Atan(dist[1] / dist[0]) * 57.295779513082f);
            if (dist[0] >= 0.0)
            {
                _temp.S += 180f;
            }
            


            //parser.CalcuateAnglePlus(_temp.F, _temp.S);
            //parser.CalcuateAngleMinus(_temp.F, _temp.S);



            return _temp;
        }

        public static double DistBetween2Players(Player player1, Player player2)
        { // player 2 længest væk

            return Math.Sqrt(Math.Pow(player2.X - player1.X, 2) + Math.Pow(player2.Y - player1.Y, 2) + Math.Pow(player2.Z - player1.Z, 2));
        }

        public static double DistBetween2Players(double p1x, double p1y, double p1z, double p2x, double p2y, double p2z)
        {

            return Math.Sqrt(Math.Pow(p2x - p1x, 2) + Math.Pow(p2y - p1y, 2) + Math.Pow(p2z - p1z, 2));
        }

        public static Angle GetMyAngle()
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

        public void PrintEnemey(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                Player _enemey = GetEnemeyPlayer(i);

                Console.WriteLine("x:" + _enemey.X);
                Console.WriteLine("y: "+ _enemey.Y);
                Console.WriteLine("z: "+ _enemey.Z);
            }
        }

        public static Player GetEnemeyPlayer(int i) 
        {
            Player _Enemey;
            _Enemey.X = 0;
            _Enemey.Y = 0;
            _Enemey.Z = 0;

            int address = baseClient + Offsets.oLocalPlayer;
            int LocalPLayer = vam.ReadInt32((IntPtr)address);

            address = LocalPLayer + Offsets.oTeam;
            _Enemey.MyTeam = vam.ReadInt32((IntPtr)address);

            address = baseClient + Offsets.oEntityList + (i * Offsets.oEntityListLoopDis);
            int PictoPic = vam.ReadInt32((IntPtr)address);

            address = PictoPic + Offsets.oTeam;
            _Enemey.Enemey_Team = vam.ReadInt32((IntPtr)address);

            address = PictoPic + Offsets.oDormant;

            if (_Enemey.Enemey_Team !=_Enemey.MyTeam & !vam.ReadBoolean((IntPtr)address))
            {
                address = PictoPic + Offsets.xpos;
                _Enemey.X = vam.ReadFloat((IntPtr)address);

                address = PictoPic + Offsets.ypos;
                _Enemey.Y = vam.ReadFloat((IntPtr)address);

                address = PictoPic + Offsets.zpos;
                _Enemey.Z = vam.ReadFloat((IntPtr)address) + 64.063312f;
                return _Enemey;
            }
            return _Enemey;
        }

        public static bool GetModuleAddy()
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
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
