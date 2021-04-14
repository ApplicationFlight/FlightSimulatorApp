using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Circle
{
    public class DLL_Circle
    {
        [DllImport("Circle_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void circle(string CSVreg, string CSVanomaly, string result);
    }
}
