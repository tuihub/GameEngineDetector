using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TuiHub.GameEngineDetectorLibrary.Detectors
{
    public static partial class Detector
    {
        public static bool IsBgi(string exePath, string? baseDirPath = null)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(exePath);
            bool result = versionInfo.FileDescription == "Ethornell - BURIKO General Interpreter"
                || versionInfo.InternalName == "Ethornell"
                || versionInfo.LegalTrademarks == "BURIKO General Interpreter"
                || versionInfo.OriginalFilename == "BGI.exe";
            if (!result)
            {
                baseDirPath ??= Path.GetDirectoryName(exePath);
                if (baseDirPath == null) { return false; }
                var matches = 0;
                var files = Directory.EnumerateFiles(baseDirPath, "*.*", new EnumerationOptions
                {
                    RecurseSubdirectories = true,
                    MaxRecursionDepth = 1
                });
                var matchFileNames = new string[] { "BGI.exe", "BHVC.exe", "BGI.hvl", "sysgrp.arc", "sysprg.arc", "system.arc" };
                matches += files.Where(f => matchFileNames.Contains(Path.GetFileName(f))).Count();
                if (matches < 3)
                {
                    Regex regex = new(@"^data[0-9]{3,5}\.arc$");
                    matches += files.Where(f => (Path.GetDirectoryName(f) == baseDirPath || Path.GetFileName(baseDirPath) == "Archive")
                        && regex.IsMatch(Path.GetFileName(f))).Count();
                }
                result = matches >= 3;
            }
            return result;
        }
    }
}
