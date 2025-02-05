using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTKWinformVSIXProject
{
	internal sealed class GTKCommand
	{
		public const int CommandId1 = 768;

		public const int CommandId2 = 816;

		public const int CommandId9 = 912;

		public const int CommandId10 = 913;

		public readonly static Guid CommandSet;

		private readonly AsyncPackage package;

		public static GTKCommand Instance
		{
			get;
			private set;
		}

		private IAsyncServiceProvider ServiceProvider
		{
			get
			{
				return this.package;
			}
		}

		static GTKCommand()
		{
			GTKCommand.CommandSet = new Guid("33934a89-b8cb-4fcb-a75c-64661180b671");
		}

		private GTKCommand(AsyncPackage package, OleMenuCommandService commandService)
		{
			AsyncPackage asyncPackage = package;
			if (asyncPackage == null)
			{
				throw new ArgumentNullException("package");
			}
			this.package = asyncPackage;
			OleMenuCommandService oleMenuCommandService = commandService;
			if (oleMenuCommandService == null)
			{
				throw new ArgumentNullException("commandService");
			}
			commandService = oleMenuCommandService;
			CommandID menuCommandID = new CommandID(GTKCommand.CommandSet, 768);
			MenuCommand menuItem = new MenuCommand(new EventHandler(this.OpenToCreateForm), menuCommandID);
			commandService.AddCommand(menuItem);
			CommandID menuCommandID2 = new CommandID(GTKCommand.CommandSet, 816);
			MenuCommand menuItem2 = new MenuCommand(new EventHandler(this.FixFormDesignerConfig), menuCommandID2);
			commandService.AddCommand(menuItem2);
			CommandID menuCommandID9 = new CommandID(GTKCommand.CommandSet, 912);
			MenuCommand menuItem9 = new MenuCommand(new EventHandler(this.LinkToGitee), menuCommandID9);
			commandService.AddCommand(menuItem9);
			CommandID menuCommandID10 = new CommandID(GTKCommand.CommandSet, 913);
			MenuCommand menuItem10 = new MenuCommand(new EventHandler(this.LinkToGTKAPP), menuCommandID10);
			commandService.AddCommand(menuItem10);
		}

		public bool Execute()
		{
			StringBuilder message = new StringBuilder();
			try
			{
				DTE2 dte2 = Package.GetGlobalService(typeof(DTE)) as DTE2;
				UIHierarchyItem[] projects = dte2.ToolWindows.SolutionExplorer.SelectedItems as UIHierarchyItem[];
				Project proj = projects[0].Object as Project;
				string FileName = proj.Name;
				string projectfullname = proj.FullName;
				string debugPath = string.Concat(Path.GetDirectoryName(projectfullname), "\\obj\\Debug");
				if (Directory.Exists(debugPath))
				{
					string[] directories = Directory.GetDirectories(debugPath);
					for (int i = 0; i < (int)directories.Length; i++)
					{
						string ObjPath = directories[i];
						string TargetFramework = Path.GetFileName(ObjPath);
						string TargetFrameworkVersion = string.Concat(Regex.Match(TargetFramework, "[\\d\\.]+", RegexOptions.IgnoreCase).Value, ".0");
						string runtimeconfig = string.Concat(ObjPath, "\\", FileName, ".runtimeconfig.json");
						if (!File.Exists(runtimeconfig))
						{
							string configtext = "{\r\n  \"runtimeOptions\": {\r\n    \"tfm\": \"[0]\",\r\n    \"framework\": {\r\n      \"name\": \"Microsoft.WindowsDesktop.App\",\r\n      \"version\": \"[1]\"\r\n    },\r\n    \"configProperties\": {\r\n      \"System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization\": false\r\n    }\r\n  }\r\n}";
							File.WriteAllText(runtimeconfig, configtext.Replace("[0]", TargetFramework).Replace("[1]", TargetFrameworkVersion));
						}
						else
						{
							string config = File.ReadAllText(runtimeconfig);
							string pattern = "\"name\"\\:[\\s]*\"Microsoft\\.NETCore\\.App\"";
							if (Regex.IsMatch(config, pattern, RegexOptions.IgnoreCase))
							{
								string newstr = Regex.Replace(config, pattern, "\"name\":\"Microsoft.WindowsDesktop.App\"", RegexOptions.IgnoreCase);
								File.WriteAllText(runtimeconfig, newstr);
							}
						}
						string designerruntimeconfig = string.Concat(ObjPath, "\\", FileName, ".designer.runtimeconfig.json");
						if (!File.Exists(designerruntimeconfig))
						{
							string configtext = "{\r\n  \"runtimeOptions\": {\r\n    \"tfm\": \"[0]\",\r\n    \"framework\": {\r\n      \"name\":\"Microsoft.WindowsDesktop.App\",\r\n      \"version\": \"[1]\"\r\n    },\r\n    \"additionalProbingPaths\": [\r\n      \"C:\\\\Users\\\\[2]\\\\.dotnet\\\\store\\\\|arch|\\\\|tfm|\",\r\n      \"C:\\\\Users\\\\[2]\\\\.nuget\\\\packages\",\r\n      \"C:\\\\Program Files (x86)\\\\Microsoft Visual Studio\\\\Shared\\\\NuGetPackages\",\r\n      \"C:\\\\Program Files\\\\dotnet\\\\sdk\\\\NuGetFallbackFolder\"\r\n    ],\r\n    \"configProperties\": {\r\n      \"System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization\": true,\r\n      \"Microsoft.NETCore.DotNetHostPolicy.SetAppPaths\": true\r\n    }\r\n  }\r\n}";
							File.WriteAllText(designerruntimeconfig, configtext.Replace("[0]", TargetFramework).Replace("[1]", TargetFrameworkVersion).Replace("[2]", Environment.UserName));
						}
						else
						{
							string config = File.ReadAllText(designerruntimeconfig);
							string pattern = "\"name\"\\:[\\s]*\"Microsoft\\.NETCore\\.App\"";
							if (Regex.IsMatch(config, pattern, RegexOptions.IgnoreCase))
							{
								string newstr = Regex.Replace(config, pattern, "\"name\":\"Microsoft.WindowsDesktop.App\"", RegexOptions.IgnoreCase);
								File.WriteAllText(designerruntimeconfig, newstr);
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				message.Append(exception.Message);
			}
			if (message.Length == 0)
			{
				message.Append("成功处理完成，建议重启Visual Studio\n");
			}
			VsShellUtilities.ShowMessageBox(this.package, message.ToString(), "正在修复窗体设计器", OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
			return true;
		}

		private void Execute(object sender, EventArgs e)
		{
			ThreadHelper.ThrowIfNotOnUIThread("Execute");
			string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
			string title = "GTKCommand";
			VsShellUtilities.ShowMessageBox(this.package, message, title, OLEMSGICON.OLEMSGICON_INFO, OLEMSGBUTTON.OLEMSGBUTTON_OK, OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
		}

		private void FixFormDesignerConfig(object sender, EventArgs e)
		{
			this.Execute();
		}

		public static async Task InitializeAsync(AsyncPackage package)
		{
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
			object serviceAsync = await package.GetServiceAsync(typeof(IMenuCommandService));
			OleMenuCommandService oleMenuCommandService = serviceAsync as OleMenuCommandService;
			serviceAsync = null;
			GTKCommand.Instance = new GTKCommand(package, oleMenuCommandService);
			oleMenuCommandService = null;
		}

		private void LinkToGitee(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer", "https://gitee.com/easywebfactory");
		}

		private void LinkToGTKAPP(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer", "https://www.gtkapp.com");
		}

		private void OpenToCreateForm(object sender, EventArgs e)
		{
			SendKeys.Send("^+A");
		}
	}
}