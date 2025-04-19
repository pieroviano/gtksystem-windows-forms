// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace System.Windows.Forms;

using GtkColor = System.Drawing.Color;
using GtkSize = System.Drawing.Size;
using GtkPoint = System.Drawing.Point;

public partial class ImageList
{
    private EventHandler? _recreateHandler;
    private EventHandler? _changeHandler;

    public event EventHandler? RecreateHandle
    {
        add => _recreateHandler += value;
        remove => _recreateHandler -= value;
    }

    internal event EventHandler? ChangeHandle
    {
        add => _changeHandler += value;
        remove => _changeHandler -= value;
    }
}