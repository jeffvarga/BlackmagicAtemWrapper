using System;
using System.Runtime.InteropServices;

namespace BlackmagicAtemWrapper.utility
{
    public class FailedException : Exception
    {
        public FailedException(Exception e) : base("E_FAIL", e) { }

        public static bool IsFailedException(int hr)
        {
            return (uint)hr == 0x80004005;
        }
    }

    /// <summary>
    /// Encapsulates a Y'CbCr color
    /// </summary>
    public class YCbCrColor
    {
        /// <summary>
        /// Initializes an instance of the <see cref="YCbCrColor"/> class.
        /// </summary>
        public YCbCrColor() : this(0.0, 0.0, 0.0) { }

        /// <summary>
        /// Initializes an instance of the <see cref="YCbCrColor"/> class.
        /// </summary>
        /// <param name="y">The Luma component.</param>
        /// <param name="cb">The blue chroma component.</param>
        /// <param name="cr">The red chroma component.</param>
        public YCbCrColor(double y, double cb, double cr)
        {
            this.Y = y;
            this.Cb = cb;
            this.Cr = cr;
        }

        /// <summary>
        /// Initializes an instance of the <see cref="YCbCrColor"/> class by copying it from <paramref name="color"/>.
        /// </summary>
        /// <param name="color">The <see cref="YCbCrColor"/> to copy.</param>
        public YCbCrColor(YCbCrColor color)
        {
            this.Y = color.Y;
            this.Cb = color.Cb;
            this.Cr = color.Cr;
        }

        /// <summary>
        /// Luma component.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Blue chroma component.
        /// </summary>
        public double Cb { get; set; }

        /// <summary>
        /// Red chroma component.
        /// </summary>
        public double Cr { get; set; }
    }
}
