//-----------------------------------------------------------------------------
// <copyright file="AudioMixer.cs">
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

namespace BlackmagicAtemWrapper.Audio
{
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The FairlightAudioInput class is used for managing a Fairlight audio input.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 7.5.4</remarks>
    public class FairlightAudioInput : IBMDSwitcherFairlightAudioInputCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherFairlightAudioInput"/>.
        /// </summary>
        private readonly IBMDSwitcherFairlightAudioInput InternalFairlightAudioInputReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioInput"/> class.
        /// </summary>
        /// <param name="audioInput">The native <seealso cref="IBMDSwitcherFairlightAudioInput"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioInput(IBMDSwitcherFairlightAudioInput audioInput)
        {
            this.InternalFairlightAudioInputReference = audioInput;
            this.InternalFairlightAudioInputReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FairlightAudioInput"/> class.
        /// </summary>
        ~FairlightAudioInput()
        {
            this.InternalFairlightAudioInputReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalFairlightAudioInputReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="FairlightAudioInput"/>.
        /// </summary>
        /// <param name="sender">The <see cref="FairlightAudioInput"/> that received the event.</param>
        public delegate void FairlightAudioInputEventHandler(object sender);

        /// <summary>
        /// The audio input’s <see cref="CurrentExternalPortType"/> changed.
        /// </summary>
        public event FairlightAudioInputEventHandler OnCurrentExternalPortTypeChanged;

        /// <summary>
        /// The audio input’s <see cref="Configuration"/> changed.
        /// </summary>
        public event FairlightAudioInputEventHandler OnConfigurationChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the collection of connected audio sources
        /// </summary>
        public FairlightAudioSourceCollection AudioSources
        {
            get { return new FairlightAudioSourceCollection(InternalFairlightAudioInputReference); }
        }

        /// <summary>
        /// Gets the audio input type.
        /// </summary>
        public _BMDSwitcherFairlightAudioInputType Type
        {
            get { return this.GetType(); }
        }

        /// <summary>
        /// Gets the current physical external port type of the Fairlight audio input.
        /// </summary>
        public _BMDSwitcherExternalPortType CurrentExternalPortType
        {
            get { return this.GetCurrentExternalPortType(); }
        }

        /// <summary>
        /// Get the available input configurations.
        /// </summary>
        public _BMDSwitcherFairlightAudioInputConfiguration SupportedConfigurations
        {
            get { return this.GetSupportedConfigurations(); }
        }

        /// <summary>
        /// Gets or sets the current input configuration.
        /// </summary>
        public _BMDSwitcherFairlightAudioInputConfiguration Configuration
        {
            get { return this.GetConfiguration(); }
            set { this.SetConfiguration(value); }
        }

        /// <summary>
        /// Returns the ID of this IBMDSwitcherFairlightAudioInput interface.
        /// </summary>
        public long Id
        {
            get { return this.GetId(); }
        }
        #endregion

        #region IBMDSwitcherFairlightAudioInput
        /// <summary>
        /// The GetType method returns the type of the Fairlight audio input.
        /// </summary>
        /// <returns>The Fairlight audio input type.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.4.1</remarks>
        /// <bug>7.2.2 enum lists MediaPlayer twice, leaves off audioin and embedded</bug>
        public new _BMDSwitcherFairlightAudioInputType GetType()
        {
            try
            {
                this.InternalFairlightAudioInputReference.GetType(out _BMDSwitcherFairlightAudioInputType type);
                return type;
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
        /// The GetCurrentExternalPortType method gets the current physical external port type of the Fairlight audio input.This may change if the physical input is switchable, generating the event bmdSwitcherFairlightAudioInputEventTypeCurrentExternalPortTypeChanged.
        /// </summary>
        /// <returns>The current external port type.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.4.2</remarks>
        public _BMDSwitcherExternalPortType GetCurrentExternalPortType()
        {
            try
            {
                this.InternalFairlightAudioInputReference.GetCurrentExternalPortType(out _BMDSwitcherExternalPortType type);
                return type;
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
        /// The GetSupportedConfigurations method returns the supported input configurations of the Fairlight audio input.
        /// </summary>
        /// <returns>The supported Fairlight audio input configurations.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.4.3</remarks>
        public _BMDSwitcherFairlightAudioInputConfiguration GetSupportedConfigurations()
        {
            this.InternalFairlightAudioInputReference.GetSupportedConfigurations(out _BMDSwitcherFairlightAudioInputConfiguration supportedConfigurations);
            return supportedConfigurations;
        }

        /// <summary>
        /// The GetConfiguration method returns the current Fairlight audio input configuration.
        /// </summary>
        /// <returns>The current Fairlight audio input configuration.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.4.4</remarks>
        public _BMDSwitcherFairlightAudioInputConfiguration GetConfiguration()
        {
            this.InternalFairlightAudioInputReference.GetConfiguration(out _BMDSwitcherFairlightAudioInputConfiguration configuration);
            return configuration;
        }

        /// <summary>
        /// The SetConfiguration method sets the Fairlight audio input configuration.
        /// </summary>
        /// <param name="configuration">The Fairlight audio input configuration.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.4.5</remarks>
        public void SetConfiguration(_BMDSwitcherFairlightAudioInputConfiguration configuration)
        {
            try
            {
                this.InternalFairlightAudioInputReference.SetConfiguration(configuration);
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
        /// The GetId method returns the audio input’s ID, used to uniquely identify an audio input within the Switcher.
        /// </summary>
        /// <returns>BMDSwitcherAudioInputId identifier for the current audio input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.4.6</remarks>
        public long GetId()
        {
            this.InternalFairlightAudioInputReference.GetId(out long id);
            return id;
        }
        #endregion

        #region IBMDSwitcherFairlightAudioInputCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherFairlightAudioInput events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherFairlightAudioInputEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.5.1</remarks>
        void IBMDSwitcherFairlightAudioInputCallback.Notify(_BMDSwitcherFairlightAudioInputEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherFairlightAudioInputEventType.bmdSwitcherFairlightAudioInputEventTypeCurrentExternalPortTypeChanged:
                    this.OnCurrentExternalPortTypeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherFairlightAudioInputEventType.bmdSwitcherFairlightAudioInputEventTypeConfigurationChanged:
                    this.OnConfigurationChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
