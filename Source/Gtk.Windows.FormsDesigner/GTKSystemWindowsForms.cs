using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Gtk.Windows.FormsDesigner
{
    public class GTKSystemWindowsForms : Task
    {
        [Required]
        public string FileName
        {
            get;
            set;
        }

        [Required]
        public string ObjPath
        {
            get;
            set;
        }

        [Required]
        public string TargetFramework
        {
            get;
            set;
        }

        [Required]
        public string TargetFrameworkVersion
        {
            get;
            set;
        }

        public GTKSystemWindowsForms()
        {
        }

        public override bool Execute()
        {
            if (Directory.Exists(this.ObjPath))
            {
                string runtimeconfig = String.Concat(this.ObjPath, "\\", this.FileName, ".runtimeconfig.json");
                if (!File.Exists(runtimeconfig))
                {
                    string runtimeOptions = @"{
  ""runtimeOptions"": {
    ""tfm"": ""[0]"",
    ""framework"": {
      ""name"": ""Microsoft.WindowsDesktop.App"",
      ""version"": ""[1]""
    },
    ""configProperties"": {
      ""System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization"": false
    }
  }
}";
                    File.WriteAllText(runtimeconfig, runtimeOptions.Replace("[0]", this.TargetFramework).Replace("[1]", String.Concat(this.TargetFrameworkVersion.Substring(1), ".0")));
                }
                else
                {
                    string runtimeConfigContent = File.ReadAllText(runtimeconfig);
                    string netCoreApp = @"""name""\:[\s]*""Microsoft\.NETCore\.App""";
                    if (Regex.IsMatch(runtimeConfigContent, netCoreApp, RegexOptions.IgnoreCase))
                    {
                        string str4 = Regex.Replace(runtimeConfigContent, netCoreApp, @"""name"":""Microsoft.WindowsDesktop.App""", RegexOptions.IgnoreCase);
                        File.WriteAllText(runtimeconfig, str4);
                    }
                }
                string designerJsonFile = String.Concat(this.ObjPath, "\\", this.FileName, ".designer.runtimeconfig.json");
                if (!File.Exists(designerJsonFile))
                {
                    string designerJsonFileContent = @"{
  ""runtimeOptions"": {
    ""tfm"": ""[0]"",
    ""framework"": {
      ""name"":""Microsoft.WindowsDesktop.App"",
      ""version"": ""[1]""
    },
    ""additionalProbingPaths"": [
      ""C:\\Users\\[2]\\.dotnet\\store\\|arch|\\|tfm|"",
      ""C:\\Users\\[2]\\.nuget\\packages"",
      ""C:\\Program Files (x86)\\Microsoft Visual Studio\\Shared\\NuGetPackages"",
      ""C:\\Program Files\\dotnet\\sdk\\NuGetFallbackFolder""
    ],
    ""configProperties"": {
      ""System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization"": true,
      ""Microsoft.NETCore.DotNetHostPolicy.SetAppPaths"": true
    }
  }
}";
                    File.WriteAllText(designerJsonFile, designerJsonFileContent.Replace("[0]", this.TargetFramework).Replace("[1]", String.Concat(this.TargetFrameworkVersion.Substring(1), ".0")).Replace("[2]", Environment.UserName));
                }
                else
                {
                    string designerJsonFileContent = File.ReadAllText(designerJsonFile);
                    string netCoreApp = @"""name""\:[\s]*""Microsoft\.NETCore\.App""";
                    if (Regex.IsMatch(designerJsonFileContent, netCoreApp, RegexOptions.IgnoreCase))
                    {
                        string newContent = Regex.Replace(designerJsonFileContent, netCoreApp, @"""name"":""Microsoft.WindowsDesktop.App""", RegexOptions.IgnoreCase);
                        File.WriteAllText(designerJsonFile, newContent);
                    }
                }
            }
            return true;
        }
    }
}