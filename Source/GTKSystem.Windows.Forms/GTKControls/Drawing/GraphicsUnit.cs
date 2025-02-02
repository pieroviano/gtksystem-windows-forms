﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.Drawing
{
    /// <summary>
    ///     Specifies the unit of measure for the given data.
    /// </summary>
    public enum GraphicsUnit
    {
        /// <summary>
        ///     Specifies the world coordinate system unit as the unit of measure.
        /// </summary>
        World = 0,

        /// <summary>
        ///     Specifies the unit of measure of the display device. Typically pixels for video
        ///     displays, and 1/100 inch for printers.
        /// </summary>
        Display = 1,

        /// <summary>
        ///     Specifies a device pixel as the unit of measure.
        /// </summary>
        Pixel = 2,

        /// <summary>
        ///     Specifies a printer's point (1/72 inch) as the unit of measure.
        /// </summary>
        Point = 3,

        /// <summary>
        ///     Specifies the inch as the unit of measure.
        /// </summary>
        Inch = 4,

        /// <summary>
        ///     Specifies the document unit (1/300 inch) as the unit of measure.
        /// </summary>
        Document = 5,

        /// <summary>
        ///     Specifies the millimeter as the unit of measure.
        /// </summary>
        Millimeter = 6
    }
}