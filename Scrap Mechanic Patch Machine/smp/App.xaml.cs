﻿using System.Windows;
using System.Windows.Threading;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace smp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		public static App? GetApp { get; private set; }

		private void Initialize(object sender, StartupEventArgs args)
		{
			AppCenter.Start("521e1419-cc08-4115-b795-feab38170224", typeof(Analytics), typeof(Crashes));
			GetApp = this;
			Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
		}

		private void AppExit(object sender, ExitEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void HandleException(object sender, DispatcherUnhandledExceptionEventArgs args)
		{
			args.Handled = true;
			Crashes.TrackError(args.Exception, null);
			new WnException(args.Exception).ShowDialog();
		}
	}
}
