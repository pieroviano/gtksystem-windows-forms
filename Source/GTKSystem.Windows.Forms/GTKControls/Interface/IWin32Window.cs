﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    [Guid("458AB8A2-A1EA-4d7b-8EBE-DEE5D3D9442C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface IWin32Window
    {
        /// <summary>
        ///  Gets the handle to the window represented by the implementor.
        /// </summary>
        IntPtr Handle { get; }
    }
}