// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Windows.Forms;

using Color = System.Drawing.Color;

internal partial class PropertyStore
{
    private sealed class ColorWrapper
    {
        public Color Color;

        public ColorWrapper(Color color)
        {
            Color = color;
        }
    }
}