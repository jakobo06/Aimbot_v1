using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace aimbot_v1
{
    class AngleParser
    {

        public AngleParser()
        {

        }

        // ops. hvad max angelen er. da 180 -> -180 = 361
        public float CalcuateAnglePlus(float first, float last)
        { // -180 -> 180

            float value = first + last;

            if (value >= 180)
            {
                return (value % 180) - 180;
            }
            else if (value <= -180)
            {
                return 180 - (value % 180);
            }
            else {
                return value;
            }
        }

        public float CalcuateAngleMinus(float first, float last)
        { // -180 -> 180

            float value = first - last;

            if (value >= 180)
            {
                return (value % 180) - 180;
            }
            else if (value <= -180)
            {
                return 180 - (value % 180);
            }
            else {
                return value;
            }

        }

        /*
        public void MoveCrossair(float andglex, float angley, float distx, float disty) {

            int length;
            int time;

            

            for (int i = 0; i < length; i++) {

            }


        }
        */
    }
}
