namespace System.Windows.Forms;

public static class InitApplication
{
    private static Gtk.Application? _app = Application.Init();
    static InitApplication()
    {
    }
}