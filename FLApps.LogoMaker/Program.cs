using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLApps.LogoMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            var logo = new Logo(DateTime.Now.GetHashCode());
            var bitmap = logo.ToBitmap();
        }
    }
}
