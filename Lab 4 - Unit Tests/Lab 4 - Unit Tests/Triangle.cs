using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_4___Unit_Tests
{
    class Triangle
    {
        public static bool CheckSides(float s1, float s2, float s3)
        {
            var sides = new float[3] { s1, s2, s3 };
            if (sides.Min() <= 0 || float.IsInfinity(s1 + s2 + s3))
                return false;
            return 2 * sides.Max() < s1 + s2 + s3;
        }
    }
}
