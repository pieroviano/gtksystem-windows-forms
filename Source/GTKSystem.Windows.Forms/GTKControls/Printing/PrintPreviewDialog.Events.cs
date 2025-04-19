// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Gtk;
using System.ComponentModel;
using System.Drawing.Printing;

namespace System.Windows.Forms;

public partial class PrintPreviewDialog : ScrollableControl
{
    public event EventHandler? MaximumSizeChanged;

    public event EventHandler? MinimumSizeChanged;
}