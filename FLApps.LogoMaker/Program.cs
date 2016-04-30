using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var temp = System.IO.Path.ChangeExtension(System.IO.Path.GetTempFileName(), "bmp");
            bitmap.Save(temp);
            Process.Start(temp);
        }
    }
}
