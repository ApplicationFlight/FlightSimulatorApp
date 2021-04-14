using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Regression
{
    public class DLL_Regression {
        [DllImport("Regression_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void regression(string CSVreg, string CSVanomaly, string result);
    }
}
