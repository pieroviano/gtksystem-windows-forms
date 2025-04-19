// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Windows.Forms;

using Rectangle = Drawing.Rectangle;

internal partial class PropertyStore
{
    private sealed class RectangleWrapper
    {
        public Rectangle Rectangle;

        public RectangleWrapper(Rectangle rectangle)
        {
            Rectangle = rectangle;
        }
    }
}