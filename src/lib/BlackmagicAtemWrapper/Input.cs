//-----------------------------------------------------------------------------
// <copyright file="Input.cs">
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
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The Input object represents an input (physical or virtual) to the switcher.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.5</remarks>
    /// <bug>Documentation has a C&amp;P error from IBMDSwitcher</bug>
    public class Input : IBMDSwitcherInputCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherInput"/>.
        /// </summary>
        internal readonly IBMDSwitcherInput InternalSwitcherInputReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> class.
        /// </summary>
        /// <param name="switcherInput">The native <seealso cref="IBMDSwitcherInput"/> from the BMDSwitcherAPI.</param>
        public Input(IBMDSwitcherInput switcherInput)
        {
            this.InternalSwitcherInputReference = switcherInput;
            this.InternalSwitcherInputReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Input"/> class.
        /// </summary>
        ~Input()
        {
            this.InternalSwitcherInputReference.RemoveCallback(this);
            Marshal.ReleaseComObject(this.InternalSwitcherInputReference);
        }

        /// <summary>
        /// A delegate to handle events from <see cref="Input"/>.
        /// </summary>
        /// <param name="sender">The <see cref="Input"/> that received the event.</param>
        public delegate void InputEventHandler(object sender);

        #region Events
        /// <summary>
        /// The short name of the input changed.
        /// </summary>
        public event InputEventHandler OnShortNameChanged;

        /// <summary>
        /// The long name of the input changed.
        /// </summary>
        public event InputEventHandler OnLongNameChanged;

        /// <summary>
        /// Program tallying for this input was turned on or turned off.
        /// </summary>
        public event InputEventHandler OnProgramTalliedChanged;

        /// <summary>
        /// Preview tallying for this input was turned on or turned off.
        /// </summary>
        public event InputEventHandler OnPreviewTalliedChanged;

        /// <summary>
        /// The external port types available to this input changed.
        /// </summary>
        public event InputEventHandler OnAvailableExternalPortTypesChanged;

        /// <summary>
        /// The current external port type of this input changed.
        /// </summary>
        public event InputEventHandler OnCurrentExternalPortTypeChanged;

        /// <summary>
        /// The long or short names changed from the default OR the long and short names changed to the default.
        /// </summary>
        public event InputEventHandler OnAreNamesDefaultChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the Id of this input.
        /// </summary>
        public long InputId
        {
            get { return this.GetInputId(); }
        }

        /// <summary>
        /// Gets the port type for the input.
        /// </summary>
        public _BMDSwitcherPortType PortType
        {
            get { return this.GetPortType(); }
        }

        /// <summary>
        /// Gets a value indicating the outputs this input can be routed to, as a BMDSwitcherInputAvailability mask.
        /// </summary>
        public _BMDSwitcherInputAvailability InputAvailability
        {
            get { return this.GetInputAvailability(); }
        }

        /// <summary>
        /// Gets or sets the short name describing the switcher input as a string limited to 4 ASCII characters.
        /// </summary>
        public string ShortName
        {
            get { return this.GetShortName(); }
            set { this.SetShortName(value); }
        }

        /// <summary>
        /// Gets or sets the long name describing the switcher input as a Unicode string limited to 20 bytes.
        /// </summary>
        public string LongName
        {
            get { return this.GetLongName(); }
            set { this.SetLongName(value); }
        }

        /// <summary>
        /// Gets the available external port types as a bit mask of BMDSwitcherExternalPortType.
        /// </summary>
        public _BMDSwitcherExternalPortType AvailableExternalPortTypes
        {
            get { return this.GetAvailableExternalPortTypes(); }
        }

        /// <summary>
        /// Sets the external port type for this input using a BMDSwitcherExternalPortType.
        /// </summary>
        public _BMDSwitcherExternalPortType CurrentExternalPortType
        {
            set { this.SetCurrentExternalPortType(value); }
        }

        /// <summary>
        /// Gets a value indicating whether this switcher input is currently program tallied.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.12</remarks>
        public bool IsProgramTallied
        {
            get
            {
                this.InternalSwitcherInputReference.IsProgramTallied(out int isTallied);
                return Convert.ToBoolean(isTallied);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this switcher input is currently preview tallied.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.13</remarks>
        public bool IsPreviewTallied
        {
            get
            {
                this.InternalSwitcherInputReference.IsPreviewTallied(out int isTallied);
                return Convert.ToBoolean(isTallied);
            }
        }

        /// <summary>
        /// Gets or sets the camera model.
        /// </summary>
        public uint CameraModel
        {
            get { return this.GetCameraModel(); }
            set { this.SetCameraModel(value); }
        }

        /// <summary>
        /// Gets the <see cref="SuperSource.InputSuperSource"/> object for this Input.  Returns null if this input is not a SuperSource input.
        /// </summary>
        public SuperSource.InputSuperSource SuperSource
        {
            get
            {
                try
                {
                    return new SuperSource.InputSuperSource(this);
                }
                catch (NotSupportedException)
                {
                    return null;
                }
            }
        }
        #endregion

        #region IBMDSwitcherInput
        /// <summary>
        /// The GetInputId method gets the unique Id for the switcher input.
        /// </summary>
        /// <returns>Unique Id for switcher input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.3</remarks>
        public long GetInputId()
        {
            this.InternalSwitcherInputReference.GetInputId(out long inputId);
            return inputId;
        }

        /// <summary>
        /// The GetPortType method returns the port type of this switcher input as a BMDSwitcherPortType. This can be used to determine if this input is an external port(i.e.bmdSwitcherPortTypeExternal), or any of the internal port types such as color bars(i.e.bmdSwitcherPortTypeColorBars).
        /// </summary>
        /// <returns>The port type.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.4</remarks>
        public _BMDSwitcherPortType GetPortType()
        {
            this.InternalSwitcherInputReference.GetPortType(out _BMDSwitcherPortType type);
            return type;
        }

        /// <summary>
        /// The GetInputAvailability method determines which outputs this input can be routed to. The available output groups are given as a bit mask of BMDSwitcherInputAvailability. The value returned can be bitwise-ANDed with any BMDSwithcherInputAvailabilty value (e.g. bmdSwitcherInputAvailabilityAuxOutputs) to determine the availability of this input to that output group.
        /// </summary>
        /// <returns>The availability of the input.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.5</remarks>
        public _BMDSwitcherInputAvailability GetInputAvailability()
        {
            this.InternalSwitcherInputReference.GetInputAvailability(out _BMDSwitcherInputAvailability availability);
            return availability;
        }

        /// <summary>
        /// The GetShortName method gets the short name describing the switcher input as a string limited to 4 ASCII characters.
        /// </summary>
        /// <returns>The short name for the switcher input, limited to 4 ASCII characters.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.7</remarks>
        public string GetShortName()
        {
            this.InternalSwitcherInputReference.GetShortName(out string name);
            return name;
        }

        /// <summary>
        /// The SetShortName method assigns the short name describing the switcher input as a string limited to 4 ASCII characters.
        /// </summary>
        /// <param name="name">The short name for the switcher input, limited to 4 ASCII characters.</param>
        /// <exception cref="ArgumentException">The name parameter contains non-ASCII characters.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.6</remarks>
        public void SetShortName(string name)
        {
            this.InternalSwitcherInputReference.SetShortName(name);
            return;
        }

        /// <summary>
        /// The GetLongName method gets the long name for the switcher input, describing the input as a Unicode string in UTF-8 format with a maximum length of 20 bytes.
        /// </summary>
        /// <returns>The long name describing the switcher input as a Unicode string with a maximum length of 20 bytes.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.9</remarks>
        public string GetLongName()
        {
            this.InternalSwitcherInputReference.GetLongName(out string name);
            return name;
        }

        /// <summary>
        /// The SetLongName method sets the long name, describing the switcher input as a Unicode string in UTF-8 format with a maximum length of 20 bytes.If a string longer than 20 bytes is provided, it will be truncated to the longest valid UTF-8 string of 20 bytes or less.
        /// </summary>
        /// <param name="name">The long name describing the switcher input as a Unicode string with a maximum length of 20 bytes.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.8</remarks>
        public void SetLongName(string name)
        {
            this.InternalSwitcherInputReference.SetLongName(name);
            return;
        }

        /// <summary>
        /// The AreNamesDefault method is used to check whether the long name and short name for this input are both set to the factory defaults.
        /// </summary>
        /// <returns>Boolean value indicating whether the long name and short name are both set to the factory defaults.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.10</remarks>
        /// <bug>IDL declares AreNamesDefault as an [in] parameter</bug>
        public bool AreNamesDefault()
        {
            this.InternalSwitcherInputReference.AreNamesDefault(out int isDefault);
            return Convert.ToBoolean(isDefault);
        }

        /// <summary>
        /// The ResetNames method resets the long and short names for this switcher input to the factory defaults for this input.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.11</remarks>
        public void ResetNames()
        {
            this.InternalSwitcherInputReference.ResetNames();
            return;
        }

        /// <summary>
        /// The GetAvailableExternalPortTypes method gets the available external port types for this switcher input, given as a bit mask of BMDSwitcherExternalPortType.This bit mask can be bitwise-ANDed with any value of BMDSwitcherExternalPortType(e.g.bmdSwitcherExternalPortTypeSDI) to determine if that external port type is available for this input.
        /// </summary>
        /// <returns>The available external port types for this switcher input as a bit mask of BMDSwitcherExternalPortType.</returns>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.14</remarks>
        public _BMDSwitcherExternalPortType GetAvailableExternalPortTypes()
        {
            this.InternalSwitcherInputReference.GetAvailableExternalPortTypes(out _BMDSwitcherExternalPortType types);
            return types;
        }

        /// <summary>
        /// The SetCurrentExternalPortType method sets the external port type for this input using a BMDSwitcherExternalPortType.The external port type is settable only for some inputs and not all external port types are valid for a given input.Call the GetAvailableExternalPortTypes function to determine the available external port types for this input.
        /// </summary>
        /// <param name="type">The external port type.</param>
        /// <exception cref="ArgumentException">The type parameter is not valid a valid external port type for this input.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.5.15</remarks>
        public void SetCurrentExternalPortType(_BMDSwitcherExternalPortType type)
        {
            this.InternalSwitcherInputReference.SetCurrentExternalPortType(type);
            return;
        }

        /// <summary>
        /// Gets current camera model.
        /// </summary>
        /// <returns>The camera model.</returns>
        /// <bug>Does not exist in documentation.</bug>
        public uint GetCameraModel()
        {
            this.InternalSwitcherInputReference.GetCameraModel(out uint cameraId);
            return cameraId;
        }

        /// <summary>
        /// Sets the current camera model
        /// </summary>
        /// <param name="cameraId">The camera model</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <bug>Does not exist in documentation.</bug>
        public void SetCameraModel(uint cameraId)
        { 
            try
            {
                this.InternalSwitcherInputReference.SetCameraModel(cameraId);
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

        #region IBMDSwitcherInputCallback
        /// <summary>
        /// <para>The Notify method is called when a IBMDSwitcherInput events occur, such as property changes. This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.</para>
        /// <para>Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherInputEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 2.3.6.1</remarks>
        void IBMDSwitcherInputCallback.Notify(_BMDSwitcherInputEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeShortNameChanged:
                    this.OnShortNameChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeLongNameChanged:
                    this.OnLongNameChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsProgramTalliedChanged:
                    this.OnProgramTalliedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsPreviewTalliedChanged:
                    this.OnPreviewTalliedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeAvailableExternalPortTypesChanged:
                    this.OnAvailableExternalPortTypesChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeCurrentExternalPortTypeChanged:
                    this.OnCurrentExternalPortTypeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeAreNamesDefaultChanged:
                    this.OnAreNamesDefaultChanged?.Invoke(this);
                    break;

                default:
                    break;
            }

            return;
        }
        #endregion
    }
}
