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
        struct Vector
        {
            public int X, Y, Z;
        }

        struct FloatVector
        {
            public float X, Y, Z;
        }

        public static string process = "csgo";
        static VAMemory vam = new VAMemory(process);
        public static int baseClient, baseEngine;

        static void Main(string[] args)
        {
            PlayerInfo Info = new PlayerInfo(vam);

            if (GetModuleAddy())
            {
                while (true)
                {
                    Info.testangel();
                    Thread.Sleep(1000);
                }
            }
        }

        static bool GetModuleAddy()
        {
            try
            {
                Process[] p = Process.GetProcessesByName(process);

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
}