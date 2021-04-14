using System.Runtime.InteropServices;

namespace DLL_Regression { 
    public class DLL_Regression {
        [DllImport("Regression_DLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void regression(string CSVreg, string CSVanomaly, string result);
    }
}
