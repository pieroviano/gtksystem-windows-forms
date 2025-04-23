using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

public interface IApplication
{
    string CommonAppDataPath { get; }
    string UserAppDataPath { get; }
    string LocalUserAppDataPath { get; }
    string ExecutablePath { get; }
    string StartupPath { get; }
    CultureInfo CurrentCulture { get; set; }
    InputLanguage CurrentInputLanguage { get; set; }
    FormCollection OpenForms { get; }
    Gtk.Application? App { get; }
    bool UseAsyncInvoke { get; set; }
    void DoEvents();
    Gtk.Application Init();
    bool SetHighDpiMode(HighDpiMode highDpiMode);
    void EnableVisualStyles();
    void SetCompatibleTextRenderingDefault(bool defaultValue);
    void Run(Form mainForm);
    void Exit();
    void Exit(CancelEventArgs? e);
    void ExitThread();
    void EventInvoke(Action eventToInvoke, bool useAsyncInvoke);
}