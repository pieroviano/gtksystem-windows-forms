// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2002-2006 Novell, Inc.
//
// Authors:
//	Jackson Harper (jackson@ximian.com)
//	Gonzalo Paniagua Javier (gonzalo@ximian.com)
//
// Based on work done by:
//   Dennis Hayes (dennish@Raytek.com)
//   Aleksey Ryabchuk (ryabchuk@yahoo.com)

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization;
using System.Resources;
using Image = System.Drawing.Image;

namespace System.Windows.Forms;

using GtkColor = System.Drawing.Color;
using GtkSize = System.Drawing.Size;
using GtkRectangle = System.Drawing.Rectangle;

[Serializable]
public sealed class ImageListStreamer : ISerializable
{
    private ImageList.ImageCollection? imageCollection;
    private Image[]? images;
    private GtkSize image_size;
    private GtkColor back_color;

    public static implicit operator ImageListStreamer(Image source)
    {
        var imageListStreamer = new ImageListStreamer
        {
            images = [source]
        };
        return imageListStreamer;
    }

    private ImageListStreamer()
    {

    }

    internal ImageListStreamer(ImageList.ImageCollection? imageCollection)
    {
        this.imageCollection = imageCollection;
    }

    internal ImageListStreamer(SerializationInfo info, StreamingContext context):this((byte[]?)info.GetValue("Data", typeof(byte[])))
    {
    }

    internal ImageListStreamer(byte[]? data)
    {
        if (data == null || data.Length <= 4)
        { // 4 is the signature
            return;
        }

        // check the signature ( 'MSFt' )
        if (data[0] != 77 || data[1] != 83 || data[2] != 70 || data[3] != 116)
        {
            return;
        }

        var decoded = GetDecodedStream(data, 4, data.Length - 4);
        decoded.Position = 4; // jumps over 'magic' and 'version', which are 16-bits each

        var reader = new BinaryReader(decoded);
        var nimages = reader.ReadUInt16();
        reader.ReadUInt16();    // cMaxImage
        var grow = reader.ReadUInt16(); // cGrow
        var cx = reader.ReadUInt16();
        var cy = reader.ReadUInt16();
        var bkcolor = reader.ReadUInt32();
        back_color = GtkColor.FromArgb((int)bkcolor);
        reader.ReadUInt16();    // flags

        var ovls = new short[4];
        for (var i = 0; i < 4; i++)
        {
            ovls[i] = reader.ReadInt16();
        }

        var decoded_buffer = decoded.GetBuffer();
        var bmp_offset = 28;
        // FileSize field from the bitmap file header
        var filesize = decoded_buffer[bmp_offset + 2] + (decoded_buffer[bmp_offset + 3] << 8) +
                       (decoded_buffer[bmp_offset + 4] << 16) + (decoded_buffer[bmp_offset + 5] << 24);
        // ImageSize field from the info header (can be 0)
        var imagesize = decoded_buffer[bmp_offset + 34] + (decoded_buffer[bmp_offset + 35] << 8) +
                        (decoded_buffer[bmp_offset + 36] << 16) + (decoded_buffer[bmp_offset + 37] << 24);

        var bmp_length = imagesize + filesize;
        var bmpms = new MemoryStream(decoded_buffer, bmp_offset, bmp_length);
        Bitmap? mask = null;
        var bmp = new Bitmap(bmpms);
        var mask_stream = new MemoryStream(decoded_buffer,
            bmp_offset + bmp_length,
            (int)(decoded.Length - bmp_offset - bmp_length));

        if (mask_stream.Length > 0)
            mask = new Bitmap(mask_stream);

        if (bkcolor == 0xFFFFFFFF)
            back_color = bmp.GetPixel(0, 0);

        if (mask != null)
        {
            var width = bmp.Width;
            var height = bmp.Height;
            var newbmp = new Bitmap(bmp);
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    var mcolor = mask.GetPixel(x, y);
                    if (mcolor.B != 0)
                    {
                        newbmp.SetPixel(x, y, GtkColor.Transparent);
                    }
                }
            }
            bmp.Dispose();
            bmp = newbmp;
            mask.Dispose();
        }
        images = new Image[nimages];
        image_size = new GtkSize(cx, cy);
        var dest_rect = new GtkRectangle(0, 0, cx, cy);
        if (grow * bmp.Width > cx) // Some images store a wrong 'grow' factor
            grow = (ushort)(bmp.Width / cx);

        for (var r = 0; r < nimages; r++)
        {
            var col = r % grow;
            var row = r / grow;
            var area = new GtkRectangle(col * cx, row * cy, cx, cy);
            var b = new Bitmap(cx, cy);
            using (var g = Graphics.FromImage(b))
            {
                g.DrawImage(bmp, dest_rect, area, GraphicsUnit.Pixel);
            }

            images[r] = b;
        }
        bmp.Dispose();
    }

    /*
    static void WriteToFile (MemoryStream st)
    {
        st.Position = 0;
        FileStream fs = File.OpenWrite (Path.GetTempFileName ());
        Console.WriteLine ("Writing to {0}", fs.Name);
        st.WriteTo (fs);
        fs.Close ();
    }
    */

    private static byte[] header = [77, 83, 70, 116, 73, 76, 1, 1];
    public void GetObjectData(SerializationInfo si, StreamingContext context)
    {
        var stream = new MemoryStream();
        var writer = new BinaryWriter(stream);
        writer.Write(header);

        Image[]? imagesValue = (imageCollection != null) ? imageCollection.ToArray() : images;
        var cols = 4;
        var rows = imagesValue?.Length??0 / cols;
        if ((imagesValue?.Length??0) % cols > 0)
            ++rows;

        writer.Write((ushort)(imagesValue?.Length??0));
        writer.Write((ushort)(imagesValue?.Length ?? 0));
        writer.Write((ushort)0x4);
        writer.Write((ushort)(imagesValue?[0].Width??0));
        writer.Write((ushort)(imagesValue?[0].Height??0));
        writer.Write(0xFFFFFFFF); //BackColor.ToArgb ()); //FIXME: should set the right one here.
        writer.Write((ushort)0x21);
        for (var i = 0; i < 4; i++)
            writer.Write((short)-1);

        var main = new Bitmap(cols * ImageSize.Width, rows * ImageSize.Height);
        var g = Graphics.FromImage(main);
        if (g != null)
        {
            using (g)
            {
                g.FillRectangle(new SolidBrush(BackColor), 0, 0,
                    main.Width, main.Height);
                for (var i = 0; i < (images?.Length??0); i++)
                {
                    var o = images;
                    if (o != null)
                    {
                        g.DrawImage(o[i], (i % cols) * ImageSize.Width,
                            (i / cols) * ImageSize.Height);
                    }
                }
            }
        }

        var tmp = new MemoryStream();
        main.Save(tmp, ImageFormat.Bmp);
        tmp.WriteTo(stream);

        var mask = Get1bppMask(main);
        main.Dispose();

        tmp = new MemoryStream();
        mask.Save(tmp, ImageFormat.Bmp);
        tmp.WriteTo(stream);
        mask.Dispose();

        stream = GetRLEStream(stream, 4);
        si.AddValue("Data", stream.ToArray(), typeof(byte[]));
    }

    private unsafe Bitmap Get1bppMask(Bitmap main)
    {
        var rect = new GtkRectangle(0, 0, main.Width, main.Height);
        var result = new Bitmap(main.Width, main.Height, PixelFormat.Format1BppIndexed);
        var dresult = result.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format1BppIndexed);

        var w = images?[0].Width??0;
        var h = images?[0].Height??0;
        var scan = (byte*)dresult.Scan0.ToPointer();
        var stride = dresult.Stride;
        Bitmap? current = null;
        for (var idx = 0; idx < (images?.Length??0); idx++)
        {
            if (images != null)
            {
                current = (Bitmap)images[idx];
            }

            // Hack for newly added images.
            // Probably has to be done somewhere else.
            if (current != null)
            {
                var c1 = current.GetPixel(0, 0);
                if (c1.A != 0 && c1 == back_color)
                    current.MakeTransparent(back_color);
            }

            //
        }

        var yidx = 0;
        var imgidx = 0;
        var localy = 0;
        var localx = 0;
        var factor_y = 0;
        var factor_x = 0;
        for (var y = 0; y < main.Height; y++)
        {
            if (localy == h)
            {
                localy = 0;
                factor_y += 4;
            }
            factor_x = 0;
            localx = 0;
            for (var x = 0; x < main.Width; x++)
            {
                if (localx == w)
                {
                    localx = 0;
                    factor_x++;
                }
                imgidx = factor_y + factor_x;
                if (imgidx >= (images?.Length ?? 0))
                    break;
                if (images != null)
                {
                    current = (Bitmap)images[imgidx];
                }

                if (current != null)
                {
                    var color = current.GetPixel(localx, localy);
                    if (color.A == 0)
                    {
                        var ptridx = yidx + (x >> 3);
                        scan[ptridx] |= (byte)(0x80 >> (x & 7));
                    }
                }

                localx++;
            }
            if (imgidx >= (images?.Length ?? 0))
                break;
            yidx += stride;
            localy++;
        }
        result.UnlockBits(dresult);

        return result;
    }

    private static MemoryStream GetDecodedStream(byte[] bytes, int offset, int size)
    {
        var buffer = new byte[512];
        var position = 0;
        int count, data;
        var result = new MemoryStream();
        while (size > 0)
        {
            count = bytes[offset++];
            data = bytes[offset++];
            if ((512 - count) < position)
            {
                result.Write(buffer, 0, position);
                position = 0;
            }

            for (var i = 0; i < count; i++)
                buffer[position++] = (byte)data;
            size -= 2;
        }

        if (position > 0)
            result.Write(buffer, 0, position);

        result.Position = 0;
        return result;
    }

    //TODO: OptimizeMe
    private static MemoryStream GetRLEStream(MemoryStream input, int start)
    {
        var result = new MemoryStream();
        var ibuffer = input.GetBuffer();
        result.Write(ibuffer, 0, start);
        input.Position = start;

        var prev = -1;
        var count = 0;
        int current;
        while ((current = input.ReadByte()) != -1)
        {
            if (prev != current || count == 255)
            {
                if (prev != -1)
                {
                    result.WriteByte((byte)count);
                    result.WriteByte((byte)prev);
                }
                prev = current;
                count = 0;
            }
            count++;
        }

        if (count > 0)
        {
            result.WriteByte((byte)count);
            result.WriteByte((byte)current);
        }

        return result;
    }

    internal Image[]? Images => images;

    internal GtkSize ImageSize => image_size;

    internal ColorDepth ColorDepth => ColorDepth.Depth32Bit;

    internal GtkColor BackColor => back_color;

    public GtkResourceManager.ResourceInfo? ResourceInfo { get; set; }
}