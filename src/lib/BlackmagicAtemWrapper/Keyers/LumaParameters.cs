//-----------------------------------------------------------------------------
// <copyright file="LumaParameters.cs">
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

namespace BlackmagicAtemWrapper.Keyers
{
    using System;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The LumaParameters object is used for manipulating parameters specific to luminance type key.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 5.2.4</remarks>
    public class LumaParameters : IBMDSwitcherKeyLumaParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKeyLumaParameters"/>.
        /// </summary>
        private readonly IBMDSwitcherKeyLumaParameters InternalKeyLumaParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="LumaParameters"/> class.
        /// </summary>
        /// <param name="switcherKeyLumaParameters">The native <seealso cref="IBMDSwitcherKeyLumaParameters"/> from the BMDSwitcherAPI.</param>
        public LumaParameters(IBMDSwitcherKeyLumaParameters switcherKeyLumaParameters)
        {
            this.InternalKeyLumaParametersReference = switcherKeyLumaParameters;
            this.InternalKeyLumaParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="LumaParameters"/> class.
        /// </summary>
        ~LumaParameters()
        {
            this.InternalKeyLumaParametersReference.RemoveCallback(this);
            Marshal.ReleaseComObject(this.InternalKeyLumaParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="LumaParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="LumaParameters"/> that received the event.</param>
        public delegate void KeyLumaParametersEventHandler(object sender);

        /// <summary>
        /// The <see cref="IsPreMultiplied"/> value changed.
        /// </summary>
        public event KeyLumaParametersEventHandler OnPreMultipliedChanged;

        /// <summary>
        /// The <see cref="Clip"/> value changed.
        /// </summary>
        public event KeyLumaParametersEventHandler OnClipChanged;

        /// <summary>
        /// The <see cref="Gain"/> value changed.
        /// </summary>
        public event KeyLumaParametersEventHandler OnGainChanged;

        /// <summary>
        /// The <see cref="IsInverse"/> value changed.
        /// </summary>
        public event KeyLumaParametersEventHandler OnInverseChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the keyer is pre-multiplied.
        /// </summary>
        public bool IsPreMultiplied
        {
            get { return this.GetPreMultiplied(); }
            set { this.SetPreMultiplied(value); }
        }

        /// <summary>
        /// Gets or sets the clip value.
        /// </summary>
        public double Clip
        {
            get { return this.GetClip(); }
            set { this.SetClip(value); }
        }

        /// <summary>
        /// Gets or sets the gain value.
        /// </summary>
        public double Gain
        {
            get { return this.GetGain(); }
            set { this.SetGain(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the keyer is inverse.
        /// </summary>
        public bool IsInverse
        {
            get { return this.GetInverse(); }
            set { this.SetInverse(value); }
        }
        #endregion

        #region IBMDSwitcherKeyLumaParameters
        /// <summary>
        /// The GetPreMultiplied method returns the current pre-multiplied flag.
        /// </summary>
        /// <returns>The current pre-multiplied flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.4.1</remarks>
        public bool GetPreMultiplied()
        {
            this.InternalKeyLumaParametersReference.GetPreMultiplied(out int preMultiplied);
            return Convert.ToBoolean(preMultiplied);
        }

        /// <summary>
        /// <para>The SetPreMultiplied method sets the pre-multiplied flag.</para>
        /// <para>NOTE - That clip, gain and inverse controls are not used when pre-multiplied flag is set to true.</para>
        /// </summary>
        /// <param name="preMulitiplied">The desired pre-multiplied flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.4.2</remarks>
        public void SetPreMultiplied(bool preMulitiplied)
        {
            try
            {
                this.InternalKeyLumaParametersReference.SetPreMultiplied(Convert.ToInt32(preMulitiplied));
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
        /// The GetClip method returns the current clip value.
        /// </summary>
        /// <returns>The current clip value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.4.3</remarks>
        public double GetClip()
        {
            this.InternalKeyLumaParametersReference.GetClip(out double clip);
            return clip;
        }

        /// <summary>
        /// The SetClip method sets the clip value.
        /// </summary>
        /// <param name="clip">The desired clip value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.4.4</remarks>
        public void SetClip(double clip)
        {
            try
            {
                this.InternalKeyLumaParametersReference.SetClip(clip);
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
        /// The GetGain method returns the current gain value.
        /// </summary>
        /// <returns>The current gain value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.4.5</remarks>
        public double GetGain()
        {
            this.InternalKeyLumaParametersReference.GetGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetGain method sets the gain value.
        /// </summary>
        /// <param name="gain">The desired gain value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.4.6</remarks>
        public void SetGain(double gain)
        {
            try
            {
                this.InternalKeyLumaParametersReference.SetGain(gain);
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
        /// The GetInverse method returns the current inverse flag.
        /// </summary>
        /// <returns>The current inverse flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.4.7</remarks>
        public bool GetInverse()
        {
            this.InternalKeyLumaParametersReference.GetInverse(out int inverse);
            return Convert.ToBoolean(inverse);
        }

        /// <summary>
        /// The SetInverse method sets the inverse flag.
        /// </summary>
        /// <param name="inverse">The desired inverse flag.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 5.2.4.8</remarks>
        public void SetInverse(bool inverse)
        {
            try
            {
                this.InternalKeyLumaParametersReference.SetInverse(Convert.ToInt32(inverse));
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

        #region IBMDSwitcherKeyLumaParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherKeyLumaParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherKeyLumaParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.5.1</remarks>
        void IBMDSwitcherKeyLumaParametersCallback.Notify(_BMDSwitcherKeyLumaParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherKeyLumaParametersEventType.bmdSwitcherKeyLumaParametersEventTypePreMultipliedChanged:
                    this.OnPreMultipliedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyLumaParametersEventType.bmdSwitcherKeyLumaParametersEventTypeClipChanged:
                    this.OnClipChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyLumaParametersEventType.bmdSwitcherKeyLumaParametersEventTypeGainChanged:
                    this.OnGainChanged?.Invoke(this);
                    break;

                case _BMDSwitcherKeyLumaParametersEventType.bmdSwitcherKeyLumaParametersEventTypeInverseChanged:
                    this.OnInverseChanged?.Invoke(this);
                    break;
            }
        }
        #endregion
    }
}
