// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;

namespace System.Windows.Forms;

public partial class ImageList : Component //, IHandle<HIMAGELIST>
{
    /// <summary>
    ///  Called when the Handle property changes.
    /// </summary>
    protected virtual void OnRecreateHandle(EventArgs e) => _recreateHandler?.Invoke(this, e);

    protected virtual void OnChangeHandle(EventArgs e) => _changeHandler?.Invoke(this, e);

}
