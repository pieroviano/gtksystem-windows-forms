﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Windows.Forms
{
    /// <summary>
    ///  Specifies how the control will behave when its AutoSize property is enabled
    /// </summary>
    public enum AutoSizeMode
    {
        /// <summary>
        ///  The same behavior as you get for controls with AutoSize and no AutoSizeMode
        ///  property. The control will grow or shrink to encompass the contents (e.g.
        ///  text for a Button, child controls for a container). The MinimumSize and
        ///  MaximumSize are followed, but the current value of the Size property is
        ///  ignored.
        /// </summary>
        GrowAndShrink,

        /// <summary>
        ///  The control will grow as much as it needs to encompass its contents (e.g.
        ///  text for a button, child controls for a container), but will not shrink
        ///  smaller than its Size, whichever is larger.
        /// </summary>
        GrowOnly
    }
}