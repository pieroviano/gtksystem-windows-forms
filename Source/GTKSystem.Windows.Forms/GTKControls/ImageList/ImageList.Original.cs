// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;

namespace System.Windows.Forms;

using GtkColor = System.Drawing.Color;

public sealed partial class ImageList
{
    /// <summary>
    ///  An image before we add it to the image list, along with a few details about how to add it.
    /// </summary>
    private class Original
    {
        internal readonly object? _image;
        internal readonly OriginalOptions _options;
        internal GtkColor _customTransparentColor = GtkColor.Transparent;

        internal int _nImages = 1;

        internal Original(object? image, OriginalOptions options) : this(image, options, GtkColor.Transparent)
        {
        }

        internal Original(object? image, OriginalOptions options, int nImages) : this(image, options, GtkColor.Transparent)
        {
            _nImages = nImages;
        }

        internal Original(object? image, OriginalOptions options, GtkColor customTransparentColor)
        {
            //if (image is not Icon && image is not Image)
            //{
            //    throw new InvalidOperationException(SR.ImageListEntryType);
            //}

            _image = image;
            _options = options;
            _customTransparentColor = customTransparentColor;
            if ((options & OriginalOptions.CustomTransparentColor) == 0)
            {
                Trace.Assert(customTransparentColor.Equals(GtkColor.Transparent), "Specified a custom transparent color then told us to ignore it");
            }
        }
    }
}