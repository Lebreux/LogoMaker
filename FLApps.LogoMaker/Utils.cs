using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace FLApps.LogoMaker
{
    public static class Utils
    {
        public static Vector2 Normalized(this Vector2 vec)
        {
            return Vector2.Normalize(vec);
        }

        public static Vector2 MultipleScalar(this Vector2 vec, float val)
        {
            return Vector2.Multiply(val, vec);
        }

        public static Vector2 Add(this Vector2 vec, Vector2 add)
        {
            return Vector2.Add(vec, add);
        }
    }
}
