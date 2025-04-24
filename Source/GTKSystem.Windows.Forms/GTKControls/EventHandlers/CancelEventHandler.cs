using System.ComponentModel;

namespace System.Windows.Forms;

public delegate void CancelEventHandler<in T>(object sender, T eventArgs) where T: CancelEventArgs;