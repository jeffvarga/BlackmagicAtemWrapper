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
    using BMDSwitcherAPI;

    public class Input : IBMDSwitcherInputCallback
    {
        private IBMDSwitcherInput switcherInput;

        public Input(IBMDSwitcherInput switcherInput)
        {
            this.switcherInput = switcherInput;
            this.switcherInput.AddCallback(this);
        }

        ~Input()
        {
            this.switcherInput.RemoveCallback(this);
            Marshal.ReleaseComObject(this.switcherInput);
        }

        public long InputId
        {
            get { return this.GetInputId(); }
        }

        public _BMDSwitcherInputAvailability InputAvailability
        {
            get { return this.GetInputAvailability(); }
        }

        public string ShortName
        {
            get { return this.GetShortName(); }
            set { this.SetShortName(value); }
        }

        public string LongName
        {
            get { return this.GetLongName(); }
            set { this.SetLongName(value); }
        }

        public _BMDSwitcherExternalPortType AvailableExternalPortTypes
        {
            get { return this.GetAvailableExternalPortTypes(); }
        }

        public _BMDSwitcherExternalPortType CurrentExternalPortType
        {
            set { this.SetCurrentExternalPortType(value); }
        }

        void IBMDSwitcherInputCallback.Notify(_BMDSwitcherInputEventType eventType)
        {
            switch (eventType)
            {
                default:
                    break;
            }

            return;
        }

        /// <summary>
        /// The GetInputId method gets the unique Id for the switcher input.
        /// </summary>
        /// <returns>Unique Id for switcher input.</returns>
        public long GetInputId()
        {
            this.switcherInput.GetInputId(out long inputId);
            return inputId;
        }

        /// <summary>
        /// The GetPortType method returns the port type of this switcher input as a BMDSwitcherPortType. This can be used to determine if this input is an external port(i.e.bmdSwitcherPortTypeExternal), or any of the internal port types such as color bars(i.e.bmdSwitcherPortTypeColorBars).
        /// </summary>
        /// <returns>The port type.</returns>
        public _BMDSwitcherPortType GetPortType()
        {
            this.switcherInput.GetPortType(out _BMDSwitcherPortType type);
            return type;
        }

        /// <summary>
        /// The GetInputAvailability method determines which outputs this input can be routed to. The available output groups are given as a bit mask of BMDSwitcherInputAvailability. The value returned can be bitwise-ANDed with any BMDSwithcherInputAvailabilty value (e.g. bmdSwitcherInputAvailabilityAuxOutputs) to determine the availability of this input to that output group.
        /// </summary>
        /// <returns>The availability of the input.</returns>
        public _BMDSwitcherInputAvailability GetInputAvailability()
        {
            this.switcherInput.GetInputAvailability(out _BMDSwitcherInputAvailability availability);
            return availability;
        }

        /// <summary>
        /// The GetShortName method gets the short name describing the switcher input as a string limited to 4 ASCII characters.
        /// </summary>
        /// <returns>The short name for the switcher input, limited to 4 ASCII characters.</returns>
        public string GetShortName()
        {
            this.switcherInput.GetShortName(out string name);
            return name;
        }

        /// <summary>
        /// The SetShortName method assigns the short name describing the switcher input as a string limited to 4 ASCII characters.
        /// </summary>
        /// <param name="name">The short name for the switcher input, limited to 4 ASCII characters.</param>
        /// <exception cref="ArgumentException">The name parameter contains non-ASCII characters.</exception>
        public void SetShortName(string name)
        {
            this.switcherInput.SetShortName(name);
            return;
        }

        /// <summary>
        /// The GetLongName method gets the long name for the switcher input, describing the input as a Unicode string in UTF-8 format with a maximum length of 20 bytes.
        /// </summary>
        /// <returns>The long name describing the switcher input as a Unicode string with a maximum length of 20 bytes.</returns>
        public string GetLongName()
        {
            this.switcherInput.GetLongName(out string name);
            return name;
        }

        /// <summary>
        /// The SetLongName method sets the long name, describing the switcher input as a Unicode string in UTF-8 format with a maximum length of 20 bytes.If a string longer than 20 bytes is provided, it will be truncated to the longest valid UTF-8 string of 20 bytes or less.
        /// </summary>
        /// <param name="name">The long name describing the switcher input as a Unicode string with a maximum length of 20 bytes.</param>
        public void SetLongName(string name)
        {
            this.switcherInput.SetLongName(name);
            return;
        }

        /// <summary>
        /// The AreNamesDefault method is used to check whether the long name and short name for this input are both set to the factory defaults.
        /// </summary>
        /// <returns>Boolean value indicating whether the long name and short name are both set to the factory defaults.</returns>
        public bool AreNamesDefault()
        {
            // BUG: Interface specifies ref instead of out
            int isDefault = 0;
            this.switcherInput.AreNamesDefault(ref isDefault);
            return isDefault != 0;
        }

        /// <summary>
        /// The ResetNames method resets the long and short names for this switcher input to the factory defaults for this input.
        /// </summary>
        public void ResetNames()
        {
            this.switcherInput.ResetNames();
            return;
        }

        /// <summary>
        /// The IsProgramTallied method determines whether this switcher input is currently program tallied.
        /// </summary>
        /// <returns>Flag indicating if the input is currently program tallied.</returns>
        public bool IsProgramTallied()
        {
            this.switcherInput.IsProgramTallied(out int isTallied);
            return isTallied != 0;
        }

        /// <summary>
        /// The IsPreviewTallied method determines whether this switcher input is currently preview tallied.
        /// </summary>
        /// <returns>Flag indicating if the input is currently preview tallied.</returns>
        public bool IsPreviewTallied()
        {
            this.switcherInput.IsPreviewTallied(out int isTallied);
            return isTallied != 0;
        }

        /// <summary>
        /// The GetAvailableExternalPortTypes method gets the available external port types for this switcher input, given as a bit mask of BMDSwitcherExternalPortType.This bit mask can be bitwise-ANDed with any value of BMDSwitcherExternalPortType(e.g.bmdSwitcherExternalPortTypeSDI) to determine if that external port type is available for this input.
        /// </summary>
        /// <returns>The available external port types for this switcher input as a bit mask of BMDSwitcherExternalPortType.</returns>
        public _BMDSwitcherExternalPortType GetAvailableExternalPortTypes()
        {
            this.switcherInput.GetAvailableExternalPortTypes(out _BMDSwitcherExternalPortType types);
            return types;
        }

        /// <summary>
        /// The SetCurrentExternalPortType method sets the external port type for this input using a BMDSwitcherExternalPortType.The external port type is settable only for some inputs and not all external port types are valid for a given input.Call the GetAvailableExternalPortTypes function to determine the available external port types for this input.
        /// </summary>
        /// <param name="type">The external port type.</param>
        /// <exception cref="ArgumentException">The type parameter is not valid a valid external port type for this input.</exception>
        public void SetCurrentExternalPortType(_BMDSwitcherExternalPortType type)
        {
            this.switcherInput.SetCurrentExternalPortType(type);
            return;
        }
    }
}
