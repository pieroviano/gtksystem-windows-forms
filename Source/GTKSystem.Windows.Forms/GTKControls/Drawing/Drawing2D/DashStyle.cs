namespace System.Drawing.Drawing2D
{
    /// <summary>Specifies the style of dashed lines drawn with a <see cref="T:System.Drawing.Pen" /> object.</summary>
    public enum DashStyle
    {
        /// <summary>Specifies a solid line.</summary>
        Solid,

        /// <summary>Specifies a line consisting of dashes.</summary>
        Dash,

        /// <summary>Specifies a line consisting of dots.</summary>
        Dot,

        /// <summary>Specifies a line consisting of a repeating pattern of dash-dot.</summary>
        DashDot,

        /// <summary>Specifies a line consisting of a repeating pattern of dash-dot-dot.</summary>
        DashDotDot,

        /// <summary>Specifies a user-defined custom dash style.</summary>
        Custom
    }
}