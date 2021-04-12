using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model {
    
    public class DLL {
        // regression algorithm
        [DllImport("Regression_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void regression(string CSVreg, string CSVanomaly, string result);

        // circle algorithm
        [DllImport("circle_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void circle(string CSVreg, string CSVanomaly, string result);
    }
}
