//-----------------------------------------------------------------------------
// <copyright file="MediaPlayer.cs">
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
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The <see cref="MediaPlayer"/> class provides the ability to play stills and clips sourced from the media pool.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 4.3.3</remarks>
    public class MediaPlayer : IBMDSwitcherMediaPlayerCallback
    {
        /// <summary>
        /// Internal reference to the raw <see cref="IBMDSwitcherMediaPlayer"/>
        /// </summary>
        private readonly IBMDSwitcherMediaPlayer InternalMediaPlayerReference;

        /// <summary>
        /// Initializes an instances of the <see cref="MediaPlayer"/> class.
        /// </summary>
        /// <param name="mediaPlayer">The native <see cref="IBMDSwitcherMediaPlayer"/> from the BMDSwitcherAPI.</param>
        public MediaPlayer(IBMDSwitcherMediaPlayer mediaPlayer)
        {
            this.InternalMediaPlayerReference = mediaPlayer;
            this.InternalMediaPlayerReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MediaPlayer"/> class.
        /// </summary>
        ~MediaPlayer()
        {
            this.InternalMediaPlayerReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalMediaPlayerReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="MediaPlayer"/>.
        /// </summary>
        /// <param name="sender">The <see cref="MediaPlayer"/> that received the event.</param>
        public delegate void MediaPlayerEventHandler(object sender);

        /// <summary>
        /// Called when the media player source changes.
        /// </summary>
        public event MediaPlayerEventHandler OnSourceChanged;

        /// <summary>
        /// Called when the media player playing state changes.
        /// </summary>
        public event MediaPlayerEventHandler OnPlayingChanged;

        /// <summary>
        /// The <see cref="DoesLoop"/> flag changed.
        /// </summary>
        public event MediaPlayerEventHandler OnLoopChanged;

        /// <summary>
        /// The <see cref="IsAtBeginning"/> flag changed.
        /// </summary>
        public event MediaPlayerEventHandler OnAtBeginningChanged;

        /// <summary>
        /// The <see cref="ClipFrameIndex"/> value changed.
        /// </summary>
        public event MediaPlayerEventHandler OnClipFrameIndexChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the media player is in a playing state.
        /// </summary>
        /// <seealso cref="GetPlaying"/>
        /// <seealso cref="SetPlaying(bool)"/>
        public bool IsPlaying
        {
            get { return this.GetPlaying(); }
            set { this.SetPlaying(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the media player loop flag is set.
        /// </summary>
        /// <seealso cref="GetLoop"/>
        /// <seealso cref="SetLoop(bool)"/>
        public bool DoesLoop
        {
            get { return this.GetLoop(); }
            set { this.SetLoop(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the media player is at frame index 0.  It is invalid to set this value to false.
        /// </summary>
        /// <seealso cref="GetAtBeginning"/>
        /// <seealso cref="SetAtBeginning"/>
        public bool IsAtBeginning
        {
            get { return this.GetAtBeginning(); }
            set { if (value) { this.SetAtBeginning(); } else { throw new ArgumentException("Cannot SetAtBeginning with false", nameof(value)); } }
        }

        /// <summary>
        /// Gets or sets the clip frame index.
        /// </summary>
        /// <seealso cref="GetClipFrame"/>
        /// <seealso cref="SetClipFrame(uint)"/>
        public uint ClipFrameIndex
        {
            get { return this.GetClipFrame(); }
            set { this.SetClipFrame(value); }
        }
        #endregion

        #region IBMDSwitcherMediaPlayer
        /// <summary>
        /// The GetSource method gets the source type and index for the media player.
        /// </summary>
        /// <returns>A tuple of &lt;_BMDSwitcherMediaPlayerSourceType, uint&gt; containing the BMDSwitcherMediaPlayerSourceType specifying the source as a still or clip, and the integer specifying the index of the source.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.1</remarks>
        public void GetSource(out _BMDSwitcherMediaPlayerSourceType type, out uint index)
        {
            try
            {
                this.InternalMediaPlayerReference.GetSource(out type, out index);
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
        /// The SetSource method sets the source type and index for the media player.
        /// </summary>
        /// <param name="type">BMDSwitcherMediaPlayerSourceType specifying the source as a still or clip.</param>
        /// <param name="index">Integer specifying the index of the source.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.2</remarks>
        public void SetSource(_BMDSwitcherMediaPlayerSourceType type, uint index)
        {
            try
            {
                this.InternalMediaPlayerReference.SetSource(type, index);
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
        /// The GetPlaying method gets the playing state for the media player.
        /// </summary>
        /// <returns>Boolean value specifying the playing state.</returns>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.3</remarks>
        public bool GetPlaying()
        {
            this.InternalMediaPlayerReference.GetPlaying(out int playing);
            return Convert.ToBoolean(playing);
        }

        /// <summary>
        /// The SetPlaying method sets the playing state for the media player.
        /// </summary>
        /// <param name="playing">Boolean value specifying the playing state</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.4</remarks>
        public void SetPlaying(bool playing)
        {
            try
            {
                this.InternalMediaPlayerReference.SetPlaying(Convert.ToInt32(playing));
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
        /// The GetLoop method gets the loop property for the media player.
        /// </summary>
        /// <returns>Boolean value specifying the loop property.</returns>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.5</remarks>
        public bool GetLoop()
        {
            this.InternalMediaPlayerReference.GetLoop(out int loop);
            return Convert.ToBoolean(loop);
        }

        /// <summary>
        /// The SetLoop method sets the loop property for the media player.
        /// </summary>
        /// <param name="loop">Boolean value specifying the loop property.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.6</remarks>
        public void SetLoop(bool loop)
        {
            try
            {
                this.InternalMediaPlayerReference.SetLoop(Convert.ToInt32(loop));
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
        /// The GetAtBeginning method gets the at beginning property for the media player.
        /// </summary>
        /// <returns>Boolean value that is true when the current frame index is zero and false otherwise.</returns>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.7</remarks>
        public bool GetAtBeginning()
        {
            this.InternalMediaPlayerReference.GetAtBeginning(out int atBeginning);
            return Convert.ToBoolean(atBeginning);
        }

        /// <summary>
        /// The SetAtBeginning method sets the current frame index to zero for the media player.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.8</remarks>
        public void SetAtBeginning()
        {
            try
            {
                this.InternalMediaPlayerReference.SetAtBeginning();
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
        /// The GetClipFrame method gets the clip frame index for the media player when it is not playing.
        /// </summary>
        /// <returns>Integer value specifying the clip frame index.</returns>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.9</remarks>
        public uint GetClipFrame()
        {
            this.InternalMediaPlayerReference.GetClipFrame(out uint clipFrameIndex);
            return clipFrameIndex;
        }

        /// <summary>
        /// The SetClipFrame method sets the clip frame index for the media player if it is not playing.
        /// </summary>
        /// <param name="clipFrameIndex">Integer value specifying the clip frame index.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 4.3.3.10</remarks>
        public void SetClipFrame(uint clipFrameIndex)
        {
            try
            {
                this.InternalMediaPlayerReference.SetClipFrame(clipFrameIndex);
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
        #endregion

        #region IBMDSwitcherMediaPlayerCallback
        /// <summary>
        /// The SourceChanged method is called when the media player source changes.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 4.3.1.1</remarks>
        void IBMDSwitcherMediaPlayerCallback.SourceChanged()
        {
            this.OnSourceChanged?.Invoke(this);
            return;
        }

        /// <summary>
        /// The PlayingChanged method is called when the media player playing state changes.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 4.3.1.2</remarks>
        void IBMDSwitcherMediaPlayerCallback.PlayingChanged()
        {
            this.OnPlayingChanged?.Invoke(this);
            return;
        }

        /// <summary>
        /// The LoopChanged method is called when the media player loop property changes.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 4.3.1.3</remarks>
        void IBMDSwitcherMediaPlayerCallback.LoopChanged()
        {
            this.OnLoopChanged?.Invoke(this);
            return;
        }

        /// <summary>
        /// The AtBeginningChanged method is called when the media player current clip frame index changes to or from zero.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 4.3.1.4</remarks>
        void IBMDSwitcherMediaPlayerCallback.AtBeginningChanged()
        {
            this.OnAtBeginningChanged?.Invoke(this);
            return;
        }

        /// <summary>
        /// The ClipFrameChanged method is called when the media player clip frame index is set.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 4.3.1.5</remarks>
        void IBMDSwitcherMediaPlayerCallback.ClipFrameChanged()
        {
            this.OnClipFrameIndexChanged?.Invoke(this);
            return;
        }
        #endregion
    }
}
