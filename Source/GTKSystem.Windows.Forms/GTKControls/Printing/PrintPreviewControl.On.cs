// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Gtk;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;

namespace System.Windows.Forms;

public partial class PrintPreviewControl : Control
{
    protected virtual void OnStartPageChanged(EventArgs e)
    {
        ((EventHandler?)Events[s_startPageChangedEvent])?.Invoke(this, e);
    }

}