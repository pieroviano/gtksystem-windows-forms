// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;

namespace System.Windows.Forms;

public partial class ApplicationContext
{
    /// <summary>
    ///  Is raised when the thread's message loop should be terminated.
    ///  This is raised by calling ExitThread.
    /// </summary>
    public event EventHandler? ThreadExit;

}