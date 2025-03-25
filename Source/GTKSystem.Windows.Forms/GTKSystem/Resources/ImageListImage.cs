// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using SdImage = System.Drawing.Image;

namespace System.Resources;

using SdSize = System.Drawing.Size;
using SdSizeF = System.Drawing.SizeF;

public class ImageListImage
{
    public ImageListImage(SdImage? image)
    {
        Image = image;
    }

    public ImageListImage(SdImage? image, string name)
    {
        Image = image;
        Name = name;
    }

    public string Name { get; set; } = string.Empty;

    [Browsable(false)]
    public SdImage? Image { get; set; }

    // Add properties to make this object "look" like Image in the Collection editor
    public float HorizontalResolution => Image?.HorizontalResolution ?? 0;

    public float VerticalResolution => Image?.VerticalResolution ?? 0;

    public PixelFormat PixelFormat => Image?.PixelFormat ?? 0;

    public ImageFormat? RawFormat => Image?.RawFormat;

    public SdSize Size => Image?.Size ?? default;

    public SdSizeF PhysicalDimension => Image?.Size ?? default(SdSizeF);

    public static ImageListImage? ImageListImageFromStream(Stream stream, bool imageIsIcon)
    {
        if (imageIsIcon)
        {
            return new ImageListImage(new Icon(stream).ToBitmap());
        }

        var fromStream = (Bitmap?)SdImage.FromStream(stream);
        if (fromStream != null)
        {
            return new ImageListImage(fromStream);
        }

        return null;
    }
}