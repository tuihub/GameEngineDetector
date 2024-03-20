using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuiHub.GameEngineDetectorLibrary
{
    public static class Detector
    {
        public static string Detect(string exePath, string? baseDirPath = null)
        {
            string result;
            if (Detectors.Detector.IsBgi(exePath, baseDirPath))
            {
                result = "BGI";
            }
            else
            {
                result = "Unknown";
            }
            return result;
        }
    }
}
