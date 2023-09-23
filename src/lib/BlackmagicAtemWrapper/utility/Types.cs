//-----------------------------------------------------------------------------
// <copyright file="Types.cs">
//   Copyright (c) 2023 Jeff Varga
//
//   Permission is hereby granted, free of charge, to any person obtaining a copy
//   of this software and associated documentation files (the "Software"), to deal
//   in the Software without restriction, including without limitation the rights
//   to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is
//   furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in all
//   copies or substantial portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------------

namespace BlackmagicAtemWrapper.utility
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents an E_FAIL COM exception.
    /// </summary>
    public class FailedException : Exception
    {
        private static readonly uint E_FAIL = 0x80004005;

        /// <summary>
        /// Initializes a new instance of the <see cref="FailedException"/> class.
        /// </summary>
        /// <param name="e">The exception to wrap.</param>
        public FailedException(Exception e) : base("E_FAIL", e) { }

        /// <summary>
        /// Checks to see if the given exception represents E_FAIL.
        /// </summary>
        /// <param name="hr">The HR to check.</param>
        /// <returns>A value indicating whether the given HR is E_FAIL.</returns>
        public static bool IsFailedException(int hr)
        {
            return (uint)hr == FailedException.E_FAIL;
        }

        /// <summary>
        /// Checks to see if the given exception represents E_FAIL
        /// </summary>
        /// <param name="e">A <see cref="COMException"/> object to check.</param>
        /// <returns>A value indicating whether the given exception is E_FAIL.</returns>
        public static bool IsFailedException(COMException e)
        {
            return FailedException.IsFailedException(e.ErrorCode);
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
