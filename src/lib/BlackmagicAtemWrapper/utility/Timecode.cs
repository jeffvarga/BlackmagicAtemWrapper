//-----------------------------------------------------------------------------
// <copyright file="Timecode.cs">
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

namespace BlackmagicAtemWrapper
{
    /// <summary>
    /// Encapsulates a Timecode.
    /// </summary>
    public class Timecode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Timecode"/> class.
        /// </summary>
        /// <param name="hours">The hours component of the timecode.</param>
        /// <param name="minutes">The minutes component of the timecode.</param>
        /// <param name="seconds">The seconds component of the timecode.</param>
        /// <param name="frames">The frames component of the timecode.</param>
        /// <param name="dropFrame">The dropframe flag of the timecode.</param>
        public Timecode(byte hours, byte minutes, byte seconds, byte frames, bool dropFrame = false)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
            this.Frames = frames;
            this.DropFrame = dropFrame;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timecode"/> class;
        /// </summary>
        public Timecode() : this(0, 0, 0, 0)
        {
            return;
        }

        /// <summary>
        /// Gets or sets the hours component of the timecode.
        /// </summary>
        public byte Hours { get; set; }

        /// <summary>
        /// Gets or sets the minutes component of the timecode.
        /// </summary>
        public byte Minutes { get; set; }

        /// <summary>
        /// Gets or sets the seconds component of the timecode.
        /// </summary>
        public byte Seconds { get; set; }

        /// <summary>
        /// Gets or sets the frames component of the timecode.
        /// </summary>
        public byte Frames { get; set; }

        /// <summary>
        /// Gets a value indicating whether the dropframe flag of the timecode is set.
        /// </summary>
        public bool DropFrame { get; }

        /// <summary>
        /// Returns the timecode object as a string of the form HH:MM:SS:FF, where the : is replaced by ; if the <see cref="DropFrame"/> flag is set.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return string.Format("{1:00}{0}{2:00}{0}{3:00}{0}{4:00}", this.DropFrame ? ";" : ":", this.Hours, this.Minutes, this.Seconds, this.Frames);
        }
    }
}
