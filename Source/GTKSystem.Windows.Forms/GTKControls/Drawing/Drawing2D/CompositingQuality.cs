namespace System.Drawing.Drawing2D
{
    /// <summary>Specifies the quality level to use during compositing.</summary>
    public enum CompositingQuality
    {
        /// <summary>Invalid quality.</summary>
        Invalid = -1,

        /// <summary>Default quality.</summary>
        Default,

        /// <summary>High speed, low quality.</summary>
        HighSpeed,

        /// <summary>High quality, low speed compositing.</summary>
        HighQuality,

        /// <summary>Gamma correction is used.</summary>
        GammaCorrected,

        /// <summary>Assume linear values.</summary>
        AssumeLinear
    }
}