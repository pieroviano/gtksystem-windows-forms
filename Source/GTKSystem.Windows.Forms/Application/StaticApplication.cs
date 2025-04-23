using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

public static class Application
{
    public static string CommonAppDataPath
    {
        get => NonStaticApplication.Instance.CommonAppDataPath;
    }

    public static string UserAppDataPath
    {
        get => NonStaticApplication.Instance.UserAppDataPath;
    }

    public static string LocalUserAppDataPath
    {
        get => NonStaticApplication.Instance.LocalUserAppDataPath;
    }

    public static string ExecutablePath
    {
        get => NonStaticApplication.Instance.ExecutablePath;
    }

    public static string StartupPath
    {
        get => NonStaticApplication.Instance.StartupPath;
    }

    public static CultureInfo CurrentCulture
    {
        get => NonStaticApplication.Instance.CurrentCulture;
        set => NonStaticApplication.Instance.CurrentCulture = value;
    }

    public static InputLanguage CurrentInputLanguage
    {
        get => NonStaticApplication.Instance.CurrentInputLanguage;
        set => NonStaticApplication.Instance.CurrentInputLanguage = value;
    }

    public static FormCollection OpenForms
    {
        get => NonStaticApplication.Instance.OpenForms;
    }

    public static Gtk.Application? App
    {
        get => NonStaticApplication.Instance.App;
    }

    public static bool UseAsyncLoad
    {
        get => NonStaticApplication.Instance.UseAsyncLoad;
        set => NonStaticApplication.Instance.UseAsyncLoad = value;
    }

    public static void DoEvents()
    {
        NonStaticApplication.Instance.DoEvents();
    }

    public static Gtk.Application Init()
    {
        return NonStaticApplication.Instance.Init();
    }

    public static bool SetHighDpiMode(HighDpiMode highDpiMode)
    {
        return NonStaticApplication.Instance.SetHighDpiMode(highDpiMode);
    }

    public static void EnableVisualStyles()
    {
        NonStaticApplication.Instance.EnableVisualStyles();
    }

    public static void SetCompatibleTextRenderingDefault(bool defaultValue)
    {
        NonStaticApplication.Instance.SetCompatibleTextRenderingDefault(defaultValue);
    }

    public static void Run(Form mainForm)
    {
        NonStaticApplication.Instance.Run(mainForm);
    }

    public static void Exit()
    {
        NonStaticApplication.Instance.Exit();
    }

    public static void Exit(CancelEventArgs? e)
    {
        NonStaticApplication.Instance.Exit(e);
    }

    public static void ExitThread()
    {
        NonStaticApplication.Instance.ExitThread();
    }

    public static void EventInvoke(Action eventToInvoke, bool useAsyncLoad = false)
    {
        NonStaticApplication.Instance.EventInvoke(eventToInvoke, useAsyncLoad);
    }
}