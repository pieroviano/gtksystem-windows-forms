namespace System.Windows.Forms;

public partial class Timer
{
    protected virtual void OnTick(Timers.ElapsedEventArgs _)
    {
        Gtk.Application.Invoke(Tick);
    }
}

