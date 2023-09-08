//-----------------------------------------------------------------------------
// <copyright file="MixMinusOutput.cs">
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
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    /// The MixMinusOutput object is used for managing a mix minus output port.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.21</remarks>
    public class MixMinusOutput : IBMDSwitcherMixMinusOutputCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixMinusOutput"/>.
        /// </summary>
        private readonly IBMDSwitcherMixMinusOutput InternalMixMinusOutputReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="MixMinusOutput"/> class.
        /// </summary>
        /// <param name="mixMinusOutput">The native <seealso cref="IBMDSwitcherMixMinusOutput"/> from the BMDSwitcherAPI.</param>
        public MixMinusOutput(IBMDSwitcherMixMinusOutput mixMinusOutput)
        {
            this.InternalMixMinusOutputReference = mixMinusOutput;
            this.InternalMixMinusOutputReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MixMinusOutput"/> class.
        /// </summary>
        ~MixMinusOutput()
        {
            this.InternalMixMinusOutputReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalMixMinusOutputReference);
            return;
        }

        /// <summary>
        /// A delegate to handle events from <see cref="MixMinusOutput"/>.
        /// </summary>
        /// <param name="sender">The <see cref="MixMinusOutput"/> that received the event.</param>
        public delegate void MixMinusOutputEventHandler(object sender);

        #region Events
        /// <summary>
        /// The audio modes available to this mix minus output have changed.
        /// </summary>
        public event MixMinusOutputEventHandler OnAvailableAudioModesChanged;

        /// <summary>
        /// The mix minus output audio mode changed.
        /// </summary>
        public event MixMinusOutputEventHandler OnAudioModeChanged;

        /// <summary>
        /// The mix minus output has minus audio input ID flag has changed.
        /// </summary>
        public event MixMinusOutputEventHandler OnHasMinusAudioInputIdChanged;

        /// <summary>
        /// The mix minus output minus audio input ID has changed.
        /// </summary>
        public event MixMinusOutputEventHandler OnMinusAudioInputIdChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current audio mode of the output as a BMDSwitcherMixMinusOutputAudioMode.
        /// </summary>
        public _BMDSwitcherMixMinusOutputAudioMode AudioMode
        {
            get { return this.GetAudioMode(); }
            set { this.SetAudioMode(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the current audio has a minus audio input ID.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.21.4</remarks>
        public bool HasMinusAudioInputId
        {
            get
            {
                this.InternalMixMinusOutputReference.HasMinusAudioInputId(out int hasMinusAudioInputId);
                return hasMinusAudioInputId != 0;
            }
        }

        /// <summary>
        /// Gets the BMDSwitcherAudioInputId of the audio input that is subtracted when in mix minus audio mode.
        /// </summary>
        public long MixMinusAudioInputId
        {
            get { return this.GetMinusAudioInputId(); }
        }
        #endregion

        #region IBMDSwitcherMixMinusOutput
        /// <summary>
        /// The GetAvailableAudioModes method gets the available mix minus output audio modes for this switcher, given as a bit mask of BMDSwitcherMixMinusOutputAudioMode.This bit mask can be bitwise-ANDed with any value of BMDSwitcherMixMinusOutputAudioMode(e.g. bmdSwitcherMixMinusOutputAudioModeProgramOut) to determine if that mix minus output audio mode is available.
        /// </summary>
        /// <returns>The available mix minus output audio modes as a bit mask of BMDSwitcherMixMinusOutputAudioMode.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.21.1</remarks>
        public _BMDSwitcherMixMinusOutputAudioMode GetAvailableAudioModes()
        {
            this.InternalMixMinusOutputReference.GetAvailableAudioModes(out _BMDSwitcherMixMinusOutputAudioMode audioModes);
            return audioModes;
        }

        /// <summary>
        /// The GetAudioMode method returns the current audio mode of the mix minus output.
        /// </summary>
        /// <returns>The current audio mode as a BMDSwitcherMixMinusOutputAudioMode.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.21.2</remarks>
        public _BMDSwitcherMixMinusOutputAudioMode GetAudioMode()
        {
            this.InternalMixMinusOutputReference.GetAudioMode(out _BMDSwitcherMixMinusOutputAudioMode audioMode);
            return audioMode;
        }

        /// <summary>
        /// The SetAudioMode method sets the audio mode of the mix minus output.
        /// </summary>
        /// <param name="audioMode">The desired audio mode in BMDSwitcherMixMinusOutputAudioMode.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.21.3</remarks>
        public void SetAudioMode(_BMDSwitcherMixMinusOutputAudioMode audioMode)
        {
            try
            {
                this.InternalMixMinusOutputReference.SetAudioMode(audioMode);
                return;
            }
            catch (COMException e)
            {
                if(FailedException.IsFailedException(e.ErrorCode))
                {
                    throw new FailedException(e);
                }
                throw;
            }
        }

        /// <summary>
        /// The GetMinusAudioInputId method gets the BMDSwitcherAudioInputId of the audio input that is subtracted when the output is in mix minus audio mode.
        /// </summary>
        /// <returns>The BMDSwitcherAudioInputId of the audio input used for mix minus.</returns>
        /// <exception cref="FailedException">No minus audio input assigned.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.21.5</remarks>
        public long GetMinusAudioInputId()
        {
            try
            {
                this.InternalMixMinusOutputReference.GetMinusAudioInputId(out long audioInputId);
                return audioInputId;
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

        #region IBMDSwitcherMixMinusOutputCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherMixMinusOutput events occur, such as audio mode changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.</para>
        /// <para>Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherMixMinusOutputEventType that describes the type of event that occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.22.1</remarks>
        void IBMDSwitcherMixMinusOutputCallback.Notify(_BMDSwitcherMixMinusOutputEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherMixMinusOutputEventType.bmdSwitcherMixMinusOutputEventTypeAvailableAudioModesChanged:
                    this.OnAvailableAudioModesChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixMinusOutputEventType.bmdSwitcherMixMinusOutputEventTypeAudioModeChanged:
                    this.OnAudioModeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixMinusOutputEventType.bmdSwitcherMixMinusOutputEventTypeHasMinusAudioInputIdChanged:
                    this.OnHasMinusAudioInputIdChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMixMinusOutputEventType.bmdSwitcherMixMinusOutputEventTypeMinusAudioInputIdChanged:
                    this.OnMinusAudioInputIdChanged?.Invoke(this);
                    break;
            }
        }
        #endregion
    }
}
