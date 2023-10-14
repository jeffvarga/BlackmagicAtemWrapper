//-----------------------------------------------------------------------------
// <copyright file="MediaPool.cs">
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

namespace BlackmagicAtemWrapper.Media
{
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The <see cref="MediaPool"/> class provides for the creation of frames and audio and for accessing and modifying stills and clips.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 4.3.14</remarks>
    public class MediaPool : IBMDSwitcherMediaPoolCallback
    {
        /// <summary>
        /// Internal reference to the raw <see cref="IBMDSwitcherMediaPool"/>
        /// </summary>
        private readonly IBMDSwitcherMediaPool InternalMediaPoolReference;

        /// <summary>
        /// Initializes an instance of the <see cref="MediaPlayer"/> class.
        /// </summary>
        /// <param name="mediaPool">The native <see cref="IBMDSwitcherMediaPool"/> from the BMDSwitcherAPI.</param>
        public MediaPool(IBMDSwitcherMediaPool mediaPool)
        {
            this.InternalMediaPoolReference = mediaPool;
            this.InternalMediaPoolReference.AddCallback(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPool"/> class from a <seealso cref="IBMDSwitcher"/>.
        /// </summary>
        /// <param name="switcher">The native <seealso cref="IBMDSwitcher"/> from the BMDSwitcherAPI.</param>
        public MediaPool(IBMDSwitcher switcher)
        {
            if (null == switcher)
            {
                throw new ArgumentNullException(nameof(switcher));
            }

            this.InternalMediaPoolReference = switcher as IBMDSwitcherMediaPool;

            return;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MediaPool"/> class.
        /// </summary>
        ~MediaPool()
        {
            this.InternalMediaPoolReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalMediaPoolReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="MediaPool"/>.
        /// </summary>
        /// <param name="sender">The <see cref="MediaPool"/> that received the event.</param>
        public delegate void MediaPoolEvent(object sender);

        /// <summary>
        /// Called when the maximum frame count changes for one or more clips.
        /// </summary>
        public MediaPoolEvent OnClipFrameMaxCountsChanged;

        /// <summary>
        /// Called when the total number of frames available to clips changes.
        /// </summary>
        public MediaPoolEvent OnFrameTotalForClipsChanged;
        #endregion

        #region Properties
        #endregion

        #region IBMDSwitcherMediaPool
        /// <summary>
        /// The GetStills method gets the IBMDSwitcherStills object interface.
        /// </summary>
        /// <returns>The stills object interface.</returns>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.1</remarks>
        public IBMDSwitcherStills GetStills()
        {
            this.InternalMediaPoolReference.GetStills(out IBMDSwitcherStills stills);
            return stills;
        }

        /// <summary>
        /// The GetClip method gets the IBMDSwitcherClip object interface.
        /// </summary>
        /// <param name="clipIndex">The clip index.</param>
        /// <returns>The clip object interface.</returns>
        /// <exception cref="ArgumentException">The <paramref name="clipIndex"/> parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.2</remarks>
        public IBMDSwitcherClip GetClip(uint clipIndex)
        {
            this.InternalMediaPoolReference.GetClip(clipIndex, out IBMDSwitcherClip clip);
            return clip;
        }

        /// <summary>
        /// The GetClipCount method gets the number of clips.
        /// </summary>
        /// <returns>The number of clips.</returns>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.3</remarks>
        public uint GetClipCount()
        {
            this.InternalMediaPoolReference.GetClipCount(out uint clipCount);
            return clipCount;
        }

        /// <summary>
        /// The CreateFrame method creates an IBMDSwitcherFrame object.
        /// </summary>
        /// <param name="pixelFormat">The pixel format. See BMDSwitcherPixelFormat for a list of supported pixel formats.</param>
        /// <param name="width">The frame width in pixels.</param>
        /// <param name="height">The frame height in pixels.</param>
        /// <returns>The newly created frame.</returns>
        /// <exception cref="ArgumentException">The <paramref name="pixelFormat"/>, <paramref name="width"/> or <paramref name="height"/> parameter is invalid.</exception>
        /// <exception cref="OutOfMemoryException">Unable to allocate required memory.</exception>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.4</remarks>
        public IBMDSwitcherFrame CreateFrame(_BMDSwitcherPixelFormat pixelFormat, uint width, uint height)
        {
            try
            {
                this.InternalMediaPoolReference.CreateFrame(pixelFormat, width, height, out IBMDSwitcherFrame frame);
                return frame;
            }
            catch (COMException e)
            {
                if (FailedException.IsFailedException(e.ErrorCode))
                {
                    throw new FailedException(e);
                }

                throw;
            }
        }

        /// <summary>
        /// The CreateAudio method creates an IBMDSwitcherAudio object.
        /// </summary>
        /// <param name="sizeBytes">The audio’s buffer size in bytes.</param>
        /// <returns>The newly created audio object.</returns>
        /// <exception cref="ArgumentException">The <paramref name="sizeBytes"/> parameter is invalid.</exception>
        /// <exception cref="OutOfMemoryException">Unable to allocate required memory.</exception>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.5</remarks>
        public IBMDSwitcherAudio CreateAudio(uint sizeBytes)
        {
            try
            {
                this.InternalMediaPoolReference.CreateAudio(sizeBytes, out IBMDSwitcherAudio audio);
                return audio;
            }
            catch (COMException e)
            {
                if (FailedException.IsFailedException(e.ErrorCode))
                {
                    throw new FailedException(e);
                }

                throw;
            }
        }

        /// <summary>
        /// The GetFrameTotalForClips method gets the total number of frames available to clips.
        /// </summary>
        /// <returns>The total number of frames available to clips.</returns>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.6</remarks>
        /// <bug>Parameters is cut and paste from ClipCount</bug>
        public uint GetFrameTotalForClips()
        {
            this.InternalMediaPoolReference.GetFrameTotalForClips(out uint total);
            return total;
        }

        /// <summary>
        /// The GetClipMaxFrameCounts method gets the maximum frame count for all clips.
        /// </summary>
        /// <returns>A <see cref="GetClipCount"/> length array, where each element receives the maximum frame count for its respective clip index.</returns>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.7</remarks>
        public uint[] GetClipMaxFrameCounts()
        {
            uint clipCount = this.GetClipCount();
            uint[] clipMaxFrameCounts = new uint[clipCount];

            if (clipCount > 0)
            {
                this.InternalMediaPoolReference.GetClipMaxFrameCounts(clipCount, out clipMaxFrameCounts[0]);
            }
            return clipMaxFrameCounts;
        }

        /// <summary>
        /// The Clear method invalidates all stills, clips and clip audio.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        public void Clear()
        {
            try
            {
                this.InternalMediaPoolReference.Clear();
                return;
            }
            catch (COMException e)
            {
                if (FailedException.IsFailedException(e.ErrorCode))
                {
                    throw new FailedException(e);
                }

                throw;
            }
        }

        /// <summary>
        /// The SetClipMaxFrameCounts method sets the maximum frame count for all clips.
        /// </summary>
        /// <param name="clipMaxFrameCounts">A clipCount length array, where each element sets the maximum frame count for its respective clip index.</param>
        /// <exception cref="ArgumentException">The length of <paramref name="clipMaxFrameCounts"/> is not equal to the value of <see cref="GetClipCount()"/></exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.9</remarks>
        public void SetClipMaxFrameCounts(uint[] clipMaxFrameCounts)
        {
            if (clipMaxFrameCounts.Length != this.GetClipCount())
            {
                throw new ArgumentException($"Length of clipMaxFrameCounts ({clipMaxFrameCounts.Length}) must equal GetClipCount() ({this.GetClipCount()}.", nameof(clipMaxFrameCounts));
            }

            if (clipMaxFrameCounts.Length > 0)
            {
                this.InternalMediaPoolReference.SetClipMaxFrameCounts((uint)clipMaxFrameCounts.Length, clipMaxFrameCounts[0]);
            }
            return;
        }

        /// <summary>
        /// The DoesVideoModeChangeClearMediaPool method determines if changing to the specified video standard will clear the media pool.
        /// </summary>
        /// <param name="videoMode">The video standard.</param>
        /// <returns>Boolean value that is true if changing to the video standard will clear the media pool.</returns>
        /// <exception cref="ArgumentException">The <paramref name="videoMode"/> is not a valid video standard.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.14.10</remarks>
        public bool DoesVideoModeChangeClearMediaPool(_BMDSwitcherVideoMode videoMode)
        {
            this.InternalMediaPoolReference.DoesVideoModeChangeClearMediaPool(videoMode, out int clear);
            return Convert.ToBoolean(clear);
        }
        #endregion

        #region IBMDSwitcherMediaPoolCallback
        /// <summary>
        /// The ClipFrameMaxCountsChanged method is called when the maximum frame count changes for one or more clips. Call <see cref="GetClipMaxFrameCounts()"/> to get the maximum frame counts for clips.
        /// </summary>
        void IBMDSwitcherMediaPoolCallback.ClipFrameMaxCountsChanged()
        {
            this.OnClipFrameMaxCountsChanged?.Invoke(this);
            return;
        }

        /// <summary>
        /// The FrameTotalForClipsChanged method is called when the total number of frames available to clips changes. Call <see cref="GetFrameTotalForClips()"/> to get the the total number of frames available to clips.
        /// </summary>
        void IBMDSwitcherMediaPoolCallback.FrameTotalForClipsChanged()
        {
            this.OnFrameTotalForClipsChanged?.Invoke(this);
            return;
        }
        #endregion
    }
}
