using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Threading;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace GTKWinformVSIXProject
{
	[Guid("8f967761-406c-4d51-ac24-cb669cfa8387")]
	[PackageRegistration(UseManagedResourcesOnly=true, AllowsBackgroundLoading=true)]
	[ProvideMenuResource("Menus.ctmenu", 1)]
	public sealed class GTKWinformVSIXProjectPackage : AsyncPackage
	{
		public const string PackageGuidString = "8f967761-406c-4d51-ac24-cb669cfa8387";

		public GTKWinformVSIXProjectPackage()
		{
		}

		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await base.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
			await GTKCommand.InitializeAsync(this);
		}
	}
}