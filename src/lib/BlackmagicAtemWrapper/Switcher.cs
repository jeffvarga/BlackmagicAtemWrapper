//-----------------------------------------------------------------------------
// <copyright file="Switcher.cs">
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
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.device;
    using BMDSwitcherAPI;

    /// <summary>
    /// The Switcher class represents a physical switcher device.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.2</remarks>
    public class Switcher : IBMDSwitcherCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcher"/>.
        /// </summary>
        internal readonly IBMDSwitcher InternalSwitcherReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="Switcher"/> class.
        /// </summary>
        /// <param name="switcher">The native <seealso cref="IBMDSwitcher"/> from the BMDSwitcherAPI.</param>
        public Switcher(IBMDSwitcher switcher)
        {
            this.InternalSwitcherReference = switcher;
            this.InternalSwitcherReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Switcher"/> class.
        /// </summary>
        ~Switcher()
        {
            this.InternalSwitcherReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalSwitcherReference);
        }

        public delegate void VideoModeChangedHandler(object sender, _BMDSwitcherVideoMode arg);
        public delegate void SwitcherEventHandler(object sender, object args);

        #region Events
        /// <summary>
        /// The video standard changed.
        /// </summary>
        public event VideoModeChangedHandler OnVideoModeChanged;

        /// <summary>
        /// The method for down converted SD output has changed.
        /// </summary>
        public event SwitcherEventHandler OnMethodForDownConvertedSDChanged;

        /// <summary>
        /// The down converted HD output video standard changed for a particular core video standard.
        /// </summary>
        public event VideoModeChangedHandler OnDownConvertedHDVideoModeChanged;

        /// <summary>
        /// The MultiView standard changed for a particular core video standard.
        /// </summary>
        public event VideoModeChangedHandler OnMultiViewVideoModeChanged;

        /// <summary>
        /// The power status changed.
        /// </summary>
        public event SwitcherEventHandler OnPowerStatusChanged;

        /// <summary>
        /// The switcher disconnected.
        /// </summary>
        public event SwitcherEventHandler OnDisconnected;

        /// <summary>
        /// The 3GSDI output level changed.
        /// </summary>
        public event SwitcherEventHandler On3GSDIOutputLevelChanged;

        /// <summary>
        /// The current timecode has changed. This only occurs when another event causes the currently cached timecode to be updated.
        /// </summary>
        public event SwitcherEventHandler OnTimeCodeChanged;

        /// <summary>
        /// The editable status of the timecode has changed.
        /// </summary>
        public event SwitcherEventHandler OnTimeCodeLockedChanged;

        /// <summary>
        /// The current timecode mode has changed.
        /// </summary>
        public event SwitcherEventHandler OnTimeCodeModeChanged;

        /// <summary>
        /// The Supersource cascade mode has changed.
        /// </summary>
        public event SwitcherEventHandler OnSuperSourceCascadeChanged;

        /// <summary>
        /// The auto video mode state has changed.
        /// </summary>
        public event SwitcherEventHandler OnAutoVideoModeChanged;

        /// <summary>
        /// The auto video mode detection state has changed.
        /// </summary>
        public event SwitcherEventHandler OnAutoVideoModeDetectedChanged;
        #endregion

        /// <summary>
        /// Returns only the <see cref="_BMDSwitcherVideoMode"/> values supported by the connected Switcher.
        /// </summary>
        public IEnumerable<_BMDSwitcherVideoMode> SupportedVideoModes
        {
            get
            {
                foreach (_BMDSwitcherVideoMode videoMode in typeof(_BMDSwitcherVideoMode).GetEnumValues())
                {
                    if (this.DoesSupportVideoMode(videoMode))
                    {
                        yield return videoMode;
                    }
                }
            }
        }

        public MultiViewCollection MultiViews
        {
            get { return new MultiViewCollection(this.InternalSwitcherReference); }
        }

        /// <summary>
        /// Gets the <see cref="Identity"/> object representing the switcher's identity.
        /// </summary>
        /// <exception cref="NotSupportedException">Unable to get a reference to <see cref="Identity"/>.  Likely because switcher or ATEM software is not version 9.0 or higher.</exception>
        public Identity Identity
        {
            get { return new Identity(this); }
        }

        /// <summary>
        /// Gets the <see cref="SerialPortCollection"/> representing the available serial ports on the switcher.
        /// </summary>
        public SerialPortCollection SerialPorts
        {
            get { return new SerialPortCollection(this.InternalSwitcherReference); } 
        }

        /// <summary>
        /// Gets the <see cref="MixMinusOutputCollection"/> representing the available mix minus outputs on the switcher.
        /// </summary>
        public MixMinusOutputCollection MixMinusOutputs
        {
            get { return new MixMinusOutputCollection(this.InternalSwitcherReference); }
        }

        /// <summary>
        /// Gets the <see cref="MixEffectBlockCollection"/> representing the available Mix Effect blocks on the switcher.
        /// </summary>
        public MixEffectBlockCollection MixEffectBlocks
        {
            get { return new MixEffectBlockCollection(this.InternalSwitcherReference); }
        }

        /// <summary>
        /// Gets the <see cref="InputCollection"/> representing the available inputs on the switcher.
        /// </summary>
        public InputCollection Inputs
        {
            get { return new InputCollection(this.InternalSwitcherReference); }
        }

        /// <summary>
        /// Gets the operating state object which can be used to save and recall state.
        /// </summary>
        public SaveRecall OperatingState
        {
            get { return new SaveRecall(this.InternalSwitcherReference as IBMDSwitcherSaveRecall); }
        }

        /// <summary>
        /// Gets the switcher's <see cref="audio.FairlightAudioMixer"/>
        /// </summary>
        public audio.FairlightAudioMixer AudioMixer
        {
            get
            {
                Guid guidAudioMixer = typeof(IBMDSwitcherFairlightAudioMixer).GUID;
                Marshal.QueryInterface(Marshal.GetIUnknownForObject(this.InternalSwitcherReference), ref guidAudioMixer, out IntPtr ppv);

                return new audio.FairlightAudioMixer(this.InternalSwitcherReference as IBMDSwitcherFairlightAudioMixer);
            }
        }

        /// <summary>
        /// Gets the product name of the switcher.
        /// </summary>
        public string ProductName
        {
            get { return this.GetProductName(); }
        }

        /// <summary>
        /// Gets or sets the current video standard applied across the switcher.
        /// </summary>
        public _BMDSwitcherVideoMode VideoMode
        {
            get { return this.GetVideoMode(); }
            set { this.SetVideoMode(value); }
        }

        /// <summary>
        /// Gets or sets the SD conversion method applied when down converting between broadcast standards.
        /// </summary>
        public _BMDSwitcherDownConversionMethod MethodForDownConvertedSD
        {
            get { return this.GetMethodForDownConvertedSD(); }
            set { this.SetMethodForDownConvertedSD(value); }
        }

        /// <summary>
        /// Gets or sets the current 3G-SDI output encoding level of the switcher.
        /// </summary>
        public _BMDSwitcher3GSDIOutputLevel _3GSDIOutputLevel
        {
            get { return this.Get3GSDIOutputLevel(); }
            set { this.Set3GSDIOutputLevel(value); }
        }

        /// <summary>
        /// Gets a value indicating the current timecode locked flag.
        /// </summary>
        public bool IsTimeCodeLocked
        {
            get { return this.GetTimeCodeLocked(); }
        }

        /// <summary>
        /// Gets or sets the current timecode mode.
        /// </summary>
        public _BMDSwitcherTimeCodeMode TimeCodeMode
        {
            get { return this.GetTimeCodeMode(); }
            set { this.SetTimeCodeMode(value); }
        }

        /// <summary>
        /// Gets a value indicating whether outputs are configurable.
        /// </summary>
        public bool AreOutputsConfigurable
        {
            get { return this.GetAreOutputsConfigurable(); }
        }

        /// <summary>
        /// Gets a value indicating whether SuperSource cascade mode is enabled.
        /// </summary>
        public bool IsSuperSourceCascadeEnabled
        {
            get { return this.GetSuperSourceCascade(); }
            set { this.SetSuperSourceCascade(value); }
        }

        /// <summary>
        /// Gets a value indicating whether the switcher supports auto video mode.
        /// </summary>
        /// <exception cref="NotImplementedException">The switcher does not support auto video mode.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.26</remarks>
        public bool DoesSupportAutoVideoMode
        {
            get
            {
                this.InternalSwitcherReference.DoesSupportAutoVideoMode(out int supported);
                return supported != 0;
            }
        }

        /// <summary>
        /// Gets a value indicating the current state of the input video mode detection.
        /// </summary>
        public bool IsAutoVideoModeDetected
        {
            get { return this.GetAutoVideoModeDetected(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether auto video mode is enabled.
        /// </summary>
        public bool IsAutoVideoMode
        {
            get { return this.GetAutoVideoMode(); }
            set { this.SetAutoVideoMode(value); }
        }

        #region IBMDSwitcherCallback interface
        /// <summary>
        /// <para>The Notify is called when IBMDSwitcher events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.</para>
        /// <para>Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType"><seealso cref="_BMDSwitcherEventType"/> that describes the type of event that has occurred.</param>
        /// <param name="coreVideoMode">Video standard for which the event was triggered. This parameter is used in bmdSwitcherEventTypeDownConverted, HDVideoModeChanged and bmdSwitcherEventTypeMultiViewVideo ModeChanged event types.</param>
        void IBMDSwitcherCallback.Notify(_BMDSwitcherEventType eventType, _BMDSwitcherVideoMode coreVideoMode)
        {
            switch (eventType)
            {
                case _BMDSwitcherEventType.bmdSwitcherEventTypeVideoModeChanged:
                    this.OnVideoModeChanged?.Invoke(this, coreVideoMode);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeMethodForDownConvertedSDChanged:
                    this.OnMethodForDownConvertedSDChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeDownConvertedHDVideoModeChanged:
                    this.OnDownConvertedHDVideoModeChanged?.Invoke(this, coreVideoMode);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeMultiViewVideoModeChanged:
                    this.OnMultiViewVideoModeChanged?.Invoke(this, coreVideoMode);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypePowerStatusChanged:
                    this.OnPowerStatusChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeDisconnected:
                    this.OnDisconnected?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventType3GSDIOutputLevelChanged:
                    this.On3GSDIOutputLevelChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeTimeCodeChanged:
                    this.OnTimeCodeChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeTimeCodeLockedChanged:
                    this.OnTimeCodeLockedChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeTimeCodeModeChanged:
                    this.OnTimeCodeModeChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeSuperSourceCascadeChanged:
                    this.OnSuperSourceCascadeChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeAutoVideoModeChanged:
                    this.OnAutoVideoModeChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherEventType.bmdSwitcherEventTypeAutoVideoModeDetectedChanged:
                    this.OnAutoVideoModeDetectedChanged?.Invoke(this, null);
                    break;
            }

            return;
        }
        #endregion IBMDSwitcherCallback interface

        /// <summary>
        /// The GetProductName method gets the product name of the switcher.
        /// </summary>
        /// <returns>The product name of the switcher.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.1</remarks>
        public string GetProductName()
        {
            this.InternalSwitcherReference.GetProductName(out string productName);
            return productName;
        }

        /// <summary>
        /// The GetVideoMode method gets the current video standard applied across the switcher.
        /// </summary>
        /// <returns>The current video standard applied across the switcher.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.2</remarks>
        public _BMDSwitcherVideoMode GetVideoMode()
        {
            this.InternalSwitcherReference.GetVideoMode(out _BMDSwitcherVideoMode videoMode);
            return videoMode;
        }

        /// <summary>
        /// The SetVideoMode method sets the video standard applied across the switcher.
        /// </summary>
        /// <param name="videoMode">The video standard applied across the switcher.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.3</remarks>
        public void SetVideoMode(_BMDSwitcherVideoMode videoMode)
        {
            this.InternalSwitcherReference.SetVideoMode(videoMode);
            return;
        }

        /// <summary>
        /// The DoesSupportVideoMode method determines if a video standard is supported by the switcher
        /// </summary>
        /// <param name="videoMode">The video standard.</param>
        /// <returns>Boolean value that is true if the video standard is supported.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.4</remarks>
        public bool DoesSupportVideoMode(_BMDSwitcherVideoMode videoMode)
        {
            this.InternalSwitcherReference.DoesSupportVideoMode(videoMode, out int supported);
            return supported != 0;
        }

        /// <summary>
        /// The DoesVideoModeChangeRequireReconfiguration method determines if changing to the specified video standard
        /// will cause the switcher to be reconfigured, which may result in the switcher restarting.
        /// </summary>
        /// <param name="videoMode">The video standard.</param>
        /// <returns>Boolean value that is true if changing to the video standard will reconfigure the switcher.</returns>
        /// <exception cref="ArgumentException">The videoMode is not a valid video standard.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.5</remarks>
        public bool DoesVideoModeChangeRequireReconfiguration(_BMDSwitcherVideoMode videoMode)
        {
            this.InternalSwitcherReference.DoesVideoModeChangeRequireReconfiguration(videoMode, out int required);
            return required != 0;
        }

        /// <summary>
        /// The GetMethodForDownConvertedSD method gets the SD conversion method applied when down converting between
        /// broadcast standards.
        /// </summary>
        /// <returns>The conversion method.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.6</remarks>
        public _BMDSwitcherDownConversionMethod GetMethodForDownConvertedSD()
        {
            this.InternalSwitcherReference.GetMethodForDownConvertedSD(out _BMDSwitcherDownConversionMethod method);
            return method;
        }

        /// <summary>
        /// The SetMethodForDownConvertedSD method sets the SD conversion method applied when down converting between
        /// broadcast standards.
        /// </summary>
        /// <param name="method">The conversion method.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.7</remarks>
        public void SetMethodForDownConvertedSD(_BMDSwitcherDownConversionMethod method)
        {
            this.InternalSwitcherReference.SetMethodForDownConvertedSD(method);
            return;
        }

        /// <summary>
        /// The GetDownConvertedHDVideoMode method gets the down converted HD output video standard for a particular
        /// core video standard.
        /// </summary>
        /// <param name="coreVideoMode">The core video standard to be down converted.</param>
        /// <returns>The mode to which the core video standard is down converted.</returns>
        /// <exception cref="ArgumentOutOfRangeException">The <paramref name="coreVideoMode"/> parameter is invalid or not supported.</exception>
        /// <exception cref="NotImplementedException">HD down conversion is not supported.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.8</remarks>
        public _BMDSwitcherVideoMode GetDownConvertedHDVideoMode(_BMDSwitcherVideoMode coreVideoMode)
        {
            this.InternalSwitcherReference.GetDownConvertedHDVideoMode(coreVideoMode, out _BMDSwitcherVideoMode downConvertedHDVideoMode);
            return downConvertedHDVideoMode;
        }

        /// <summary>
        /// The SetDownConvertedHDVideoMode method sets the down converted HD output video standard for a particular
        /// core video standard.
        /// </summary>
        /// <param name="coreVideoMode">The core video standard to be down converted.</param>
        /// <param name="downConvertedHDVideoMode">The mode to which the core video standard is to be down converted.</param>
        /// <exception cref="ArgumentException">The coreVideoMode or <paramref name="downConvertedHDVideoMode"/> parameter is invalid or not supported.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.9</remarks>
        public void SetDownConvertedHDVideoMode(_BMDSwitcherVideoMode coreVideoMode, _BMDSwitcherVideoMode downConvertedHDVideoMode)
        {
            this.InternalSwitcherReference.SetDownConvertedHDVideoMode(coreVideoMode, downConvertedHDVideoMode);
            return;
        }

        /// <summary>
        /// The DoesSupportDownConvertedHDVideoMode method determines if a down converted HD output video standard is
        /// supported by a particular core video standard.
        /// </summary>
        /// <param name="coreVideoMode">The core video standard to be down converted.</param>
        /// <param name="downConvertedHDVideoMode">The down converted video standard to determine support for.</param>
        /// <returns>Boolean value that is true if the <paramref name="downConvertedHDVideoMode"/> is supported for the core video standard.</returns>
        /// <exception cref="ArgumentException">The <paramref name="downConvertedHDVideoMode"/> parameter is invalid.</exception>
        /// <exception cref="NotImplementedException">The switcher does not support HD down conversion.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.10</remarks>
        public bool DoesSupportDownConvertedHDVideoMode(_BMDSwitcherVideoMode coreVideoMode, _BMDSwitcherVideoMode downConvertedHDVideoMode)
        {
            this.InternalSwitcherReference.DoesSupportDownConvertedHDVideoMode(coreVideoMode, downConvertedHDVideoMode, out int supported);
            return supported != 0;
        }

        /// <summary>
        /// The GetMultiViewVideoMode method gets the MultiView video standard for a particular core video standard.
        /// </summary>
        /// <param name="coreVideoMode">The core video standard.</param>
        /// <returns>The MultiView standard used with the core video standard.</returns>
        /// <exception cref="ArgumentException">The coreVideoMode parameter is invalid or not supported.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.11</remarks>
        public _BMDSwitcherVideoMode GetMultiViewVideoMode(_BMDSwitcherVideoMode coreVideoMode)
        {
            this.InternalSwitcherReference.GetMultiViewVideoMode(coreVideoMode, out _BMDSwitcherVideoMode multiviewVideoMode);
            return multiviewVideoMode;
        }

        /// <summary>
        /// The SetMultiViewVideoMode method gets the MultiView video standard for a particular core video standard.
        /// </summary>
        /// <param name="coreVideoMode">The core video standard.</param>
        /// <param name="multiviewVideoMode">The MultiView standard to set with the core video standard.</param>
        /// <exception cref="ArgumentException">The <paramref name="coreVideoMode"/> or <paramref name="multiviewVideoMode"/> parameter is invalid or not supported.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.12</remarks>
        public void SetMultiViewVideoMode(_BMDSwitcherVideoMode coreVideoMode, _BMDSwitcherVideoMode multiviewVideoMode)
        {
            this.InternalSwitcherReference.SetMultiViewVideoMode(coreVideoMode, multiviewVideoMode);
            return;
        }

        /// <summary>
        /// The Get3GSDIOutputLevel method gets the output encoding level for all 3G-SDI outputs of the switcher, on
        /// models supporting 3G-SDI video formats.
        /// </summary>
        /// <returns>The current 3G-SDI output level.</returns>
        /// <exception cref="NotImplementedException">The connected switcher does not support 3G-SDI output.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.13</remarks>
        public _BMDSwitcher3GSDIOutputLevel Get3GSDIOutputLevel()
        {
            this.InternalSwitcherReference.Get3GSDIOutputLevel(out _BMDSwitcher3GSDIOutputLevel outputLevel);
            return outputLevel;
        }

        /// <summary>
        /// The Set3GSDIOutputLevel method sets the output encoding level for all 3G-SDI outputs of the switcher, on
        /// models supporting 3G-SDI video formats.
        /// </summary>
        /// <param name="outputLevel">The desired 3G-SDI output level.</param>
        /// <exception cref="NotImplementedException">The connected switcher does not support 3G-SDI output.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.14</remarks>
        public void Set3GSDIOutputLevel(_BMDSwitcher3GSDIOutputLevel outputLevel)
        {
            this.InternalSwitcherReference.Set3GSDIOutputLevel(outputLevel);
            return;
        }

        /// <summary>
        /// The GetPowerStatus method gets the connected power status, useful for models supporting multiple power
        /// sources.
        /// </summary>
        /// <returns>The power status.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.16</remarks>
        public _BMDSwitcherPowerStatus GetPowerStatus()
        {
            this.InternalSwitcherReference.GetPowerStatus(out _BMDSwitcherPowerStatus powerStatus);
            return powerStatus;
        }

        /// <summary>
        /// The GetTimeCode method returns the timecode that was last received from the switcher.
        /// </summary>
        /// <returns>The timecode.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.17</remarks>
        public Timecode GetTimeCode()
        {
            this.InternalSwitcherReference.GetTimeCode(out byte hours, out byte minutes, out byte seconds, out byte frames, out int dropFrame);
            return new Timecode(hours, minutes, seconds, frames, dropFrame != 0);
        }

        /// <summary>
        /// The SetTimeCode method sets the timecode of the switcher.
        /// </summary>
        /// <param name="hours">The hours value of the timecode.</param>
        /// <param name="minutes">The minutes value of the timecode.</param>
        /// <param name="seconds">The seconds value of the timecode.</param>
        /// <param name="frames">The frames value of the timecode.</param>
        /// <exception cref="ArgumentException">A parameter is not a valid value.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.18</remarks>
        public void SetTimeCode(byte hours, byte minutes, byte seconds, byte frames)
        {
            this.InternalSwitcherReference.SetTimeCode(hours, minutes, seconds, frames);
            return;
        }

        /// <summary>
        /// The SetTimeCode method sets the timecode of the switcher.
        /// </summary>
        /// <param name="timecode">A <see cref="Timecode"/> object containing the timecode to set.</param>
        public void SetTimeCode(Timecode timecode)
        {
            this.SetTimeCode(timecode.Hours, timecode.Minutes, timecode.Seconds, timecode.Frames);
            return;
        }

        /// <summary>
        /// The RequestTimeCode method requests the current timecode from the switcher which will be cached when
        /// received. Use the GetTimeCode method to get the cached timecode.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.19</remarks>
        public void RequestTimeCode()
        {
            this.InternalSwitcherReference.RequestTimeCode();
            return;
        }

        /// <summary>
        /// The GetTimeCodeLocked method indicates whether the timecode can be changed with SetTimeCode.
        /// </summary>
        /// <returns>The current timecode locked flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.20</remarks>
        public bool GetTimeCodeLocked()
        {
            this.InternalSwitcherReference.GetTimeCodeLocked(out int timeCodeLocked);
            return timeCodeLocked != 0;
        }

        /// <summary>
        /// The GetTimeCodeMode method returns the current timecode mode of the switcher.
        /// </summary>
        /// <returns>The current timecode mode.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.21</remarks>
        public _BMDSwitcherTimeCodeMode GetTimeCodeMode()
        {
            this.InternalSwitcherReference.GetTimeCodeMode(out _BMDSwitcherTimeCodeMode timeCodeMode);
            return timeCodeMode;
        }

        /// <summary>
        /// The SetTimeCodeMode method sets the timecode mode of the switcher.
        /// </summary>
        /// <param name="timeCodeMode">The timecode mode to be set.</param>
        /// <exception cref="ArgumentException">The timeCodeMode parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.22</remarks>
        public void SetTimeCodeMode(_BMDSwitcherTimeCodeMode timeCodeMode)
        {
            this.InternalSwitcherReference.SetTimeCodeMode(timeCodeMode);
            return;
        }

        /// <summary>
        /// The GetAreOutputsConfigurable method indicates whether all of the switcher’s outputs can be configured.
        /// Some switchers have mostly fixed outputs and only a small number of configurable outputs. Other switchers
        /// only have configurable outputs.
        /// </summary>
        /// <returns>Boolean that indicates if the switcher only has configurable outputs.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.23</remarks>
        public bool GetAreOutputsConfigurable()
        {
            this.InternalSwitcherReference.GetAreOutputsConfigurable(out int configurable);
            return configurable != 0;
        }

        /// <summary>
        /// The GetSuperSourceCascade method indicates whether the SuperSource cascade mode is currently enabled.
        /// </summary>
        /// <returns>The current SuperSource cascade flag.</returns>
        /// <exception cref="NotImplementedException">The switcher does not support SuperSource cascade</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.24</remarks>
        public bool GetSuperSourceCascade()
        {
            this.InternalSwitcherReference.GetSuperSourceCascade(out int cascade);
            return cascade != 0;
        }

        /// <summary>
        /// The SetSuperSourceCascade method is used to enable or disable the SuperSource cascade mode.
        /// </summary>
        /// <param name="cascade">The desired SuperSource cascade flag.</param>
        /// <exception cref="NotImplementedException">The switcher does not support SuperSource cascade.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.25</remarks>
        public void SetSuperSourceCascade(bool cascade)
        {
            this.InternalSwitcherReference.SetSuperSourceCascade(cascade ? 1 : 0);
            return;
        }

        /// <summary>
        /// The GetAutoVideoMode method indicates whether auto video mode is currently enabled.
        /// </summary>
        /// <returns>A Boolean value indicating whether auto video mode is enabled.</returns>
        /// <exception cref="NotImplementedException">The switcher does not support auto video mode.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.27</remarks>
        public bool GetAutoVideoMode()
        {
            this.InternalSwitcherReference.GetAutoVideoMode(out int enabled);
            return enabled != 0;
        }

        /// <summary>
        /// The GetAutoVideoModeDetected method indicates whether an input video mode has been detected.
        /// </summary>
        /// <returns>A Boolean value indicating whether an input video mode has been detected.</returns>
        /// <exception cref="NotImplementedException">The switcher does not support auto video mode.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.28</remarks>
        public bool GetAutoVideoModeDetected()
        {
            this.InternalSwitcherReference.GetAutoVideoModeDetected(out int detected);
            return detected != 0;
        }

        /// <summary>
        /// The GetTimeCode method is used to enable or disable auto video mode.
        /// </summary>
        /// <param name="enabled">A Boolean value indicating whether auto video mode should be enabled.</param>
        /// <exception cref="NotImplementedException">The switcher does not support auto video mode.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.2.29</remarks>
        public void SetAutoVideoMode(bool enabled)
        {
            this.InternalSwitcherReference.SetAutoVideoMode(enabled ? 1 : 0);
            return;
        }
    }
}
