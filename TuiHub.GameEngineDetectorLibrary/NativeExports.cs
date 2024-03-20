using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TuiHub.GameEngineDetectorLibrary
{
    public static class NativeExports
    {
        [UnmanagedCallersOnly(EntryPoint = "detect")]
        public static IntPtr Detect(IntPtr exePathPtr, IntPtr baseDirPtr)
        {
            try
            {
                var exePath = Marshal.PtrToStringAnsi(exePathPtr);
                if (exePath == null) { throw new ArgumentNullException(exePath); }
                var baseDir = baseDirPtr == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(baseDirPtr);

                string result = Detector.Detect(exePath, baseDir);

                return Marshal.StringToHGlobalAnsi(result);
            }
            catch (Exception ex)
            {
                return Marshal.StringToHGlobalAnsi($"Error: {ex}");
            }
        }
    }
}
