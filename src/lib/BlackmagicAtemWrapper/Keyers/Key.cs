//-----------------------------------------------------------------------------
// <copyright file="Key.cs">
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
    using BMDSwitcherAPI;

    /// <summary>
    /// The IBMDSwitcherKey object interface is used for manipulating the basic
    /// settings of a key. Please note that the mask settings in this interface
    /// only apply to luminance, chroma and pattern key types; DVE type key
    /// uses its own mask settings available in the <seealso cref="IBMDSwitcherKeyDVEParameters"/> interface. 
    /// </summary>
    public class Key : IBMDSwitcherKeyCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherKey"/>
        /// </summary>
        private readonly IBMDSwitcherKey InternalSwitcherKeyReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="Key" /> class.
        /// </summary>
        /// <param name="switcherKey">The native <seealso cref="IBMDSwitcherKey"/> from the BMDSwitcherAPI.</param>
        public Key(IBMDSwitcherKey switcherKey)
        {
            this.InternalSwitcherKeyReference = switcherKey;
            this.InternalSwitcherKeyReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Key"/> class.
        /// </summary>
        ~Key()
        {
            this.InternalSwitcherKeyReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalSwitcherKeyReference);
        }

        #region Events
        /// <summary>
        /// The can-be-DVE flag changed.
        /// </summary>
        public event SwitcherEventHandler OnCanBeDVEKeyChanged;

        /// <summary>
        /// The cut input source changed.
        /// </summary>
        public event SwitcherEventHandler OnInputCutChanged;

        /// <summary>
        /// The fill input source changed. 
        /// </summary>
        public event SwitcherEventHandler OnInputFillChanged;

        /// <summary>
        /// The mask bottom value changed.
        /// </summary>
        public event SwitcherEventHandler OnMaskBottomChanged;

        /// <summary>
        /// The masked flag changed.
        /// </summary>
        public event SwitcherEventHandler OnMaskedChanged;

        /// <summary>
        /// The mask left value changed. 
        /// </summary>
        public event SwitcherEventHandler OnMaskLeftChanged;

        /// <summary>
        /// The mask right value changed.
        /// </summary>
        public event SwitcherEventHandler OnMaskRightChanged;

        /// <summary>
        /// The mask top value changed.
        /// </summary>
        public event SwitcherEventHandler OnMaskTopChanged;

        /// <summary>
        /// The on-air flag changed.
        /// </summary>
        public event SwitcherEventHandler OnOnAirChanged;

        /// <summary>
        /// The type changed.
        /// </summary>
        public event SwitcherEventHandler OnTypeChanged;

        /// <summary>
        /// An IBMDSwitcherKeyCallback event occurred.
        /// </summary>
        public event SwitcherEventHandler OnNotify;
        #endregion

        #region QueryInterface fields
        /// <summary>
        /// Gets the <see cref="KeyLumaParameters"/> property.
        /// </summary>
        public KeyLumaParameters LumaParameters
        {
            get { return new KeyLumaParameters(this.InternalSwitcherKeyReference as IBMDSwitcherKeyLumaParameters); }
        }

        /// <summary>
        /// Gets the <see cref="KeyChromaParameters"/> property.
        /// </summary>
        public KeyChromaParameters ChromaParameters
        {
            get { return new KeyChromaParameters(this.InternalSwitcherKeyReference as IBMDSwitcherKeyChromaParameters); }
        }

        /// <summary>
        /// Gets the <see cref="AdvancedChromaParameters"/> object.
        /// </summary>
        public AdvancedChromaParameters AdvancedChromaParameters
        {
            get { return new AdvancedChromaParameters(this.InternalSwitcherKeyReference as IBMDSwitcherKeyAdvancedChromaParameters); }
        }

        /// <summary>
        /// Gets the <see cref="SwitcherKeyPatternParameters"/> property
        /// </summary>
        public SwitcherKeyPatternParameters SwitcherKeyPatternParameters
        {
            get { return new SwitcherKeyPatternParameters(this.InternalSwitcherKeyReference as IBMDSwitcherKeyPatternParameters); }
        }

        /// <summary>
        /// Gets the <see cref="SwitcherKeyDVEParameters"/> property
        /// </summary>
        public KeyDVEParameters SwitcherKeyDVEParameters
        {
            get { return new KeyDVEParameters(this.InternalSwitcherKeyReference as IBMDSwitcherKeyDVEParameters); }
        }

        /// <summary>
        /// Gets the <see cref="SwitcherKeyFlyParameters"/> property
        /// </summary>
        public SwitcherKeyFlyParameters SwitcherKeyFlyParameters
        {
            get { return new SwitcherKeyFlyParameters(this.InternalSwitcherKeyReference as IBMDSwitcherKeyFlyParameters); }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether advanced chroma key is supported by the switcher.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.1</remarks>
        public bool DoesSupportAdvancedChroma
        {
            get
            {
                this.InternalSwitcherKeyReference.DoesSupportAdvancedChroma(out int supportsAdvancedChroma);
                return Convert.ToBoolean(supportsAdvancedChroma);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this key can be set to the DVE type. The DVE
        /// hardware is a shared resource; if another component is currently using the resource, it may not be
        /// available for this key
        /// </summary>
        /// <returns>Boolean status of whether this key can be a DVE key</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.12</remarks>
        public bool CanBeDVEKey
        {
            get
            {
                this.InternalSwitcherKeyReference.CanBeDVEKey(out int canDVE);
                return Convert.ToBoolean(canDVE);
            }
        }
        
        /// <summary>
        /// Gets or sets the current key type.
        /// </summary>
        public _BMDSwitcherKeyType Type
        {
            get { return this.GetType(); }
            set { this.SetType(value); }
        }

        /// <summary>
        /// Gets or sets the current cut input source.
        /// </summary>
        public long InputCut
        {
            get { return this.GetInputCut(); }
            set { this.SetInputCut(value); }
        }

        /// <summary>
        /// Gets or sets the current fill input source.
        /// </summary>
        public long InputFill
        {
            get { return this.GetInputFill(); }
            set { this.SetinputFill(value); }
        }

        /// <summary>
        /// Gets the availability mask for the fill of this input.
        /// </summary>
        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get { return this.GetFillInputAvailabilityMask(); }
        }

        /// <summary>
        /// Gets the availability mask for the cut of this input.
        /// </summary>
        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get { return this.GetCutInputAvailabilityMask(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current key is on-air.
        /// </summary>
        public bool OnAir
        {
            get { return this.GetOnAir(); }
            set { this.SetOnAir(value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current key is masked.
        /// </summary>
        public bool Masked
        {
            get { return this.GetMasked(); }
            set { this.SetMasked(value); }
        }

        /// <summary>
        /// Gets or sets the current mask top value.
        /// </summary>
        public double MaskTop
        {
            get { return this.GetMaskTop(); }
            set { this.SetMaskTop(value); }
        }

        /// <summary>
        ///  Gets or sets the current mask bottom value.
        /// </summary>
        public double MaskBottom
        {
            get { return this.GetMaskBottom(); }
            set { this.SetMaskBottom(value); }
        }

        /// <summary>
        /// Gets or sets the current mask left value.
        /// </summary>
        public double MaskLeft
        {
            get { return this.GetMaskLeft(); }
            set { this.SetMaskLeft(value); }
        }

        /// <summary>
        /// Gets or sets the current mask right value.
        /// </summary>
        public double MaskRight
        {
            get { return this.GetMaskRight(); }
            set { this.SetMaskRight(value); }
        }
        #endregion

        #region IBMDSwitcherKey
        /// <summary>
        /// The GetType method returns the current key type.
        /// </summary>
        /// <returns>The current key type.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.2</remarks>
        public new _BMDSwitcherKeyType GetType()
        {
            this.InternalSwitcherKeyReference.GetType(out _BMDSwitcherKeyType type);
            return type;
        }

        /// <summary>
        /// The SetType method sets the key to the specified type.
        /// </summary>
        /// <param name="type">The desired key type.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.3</remarks>
        public void SetType(_BMDSwitcherKeyType type)
        {
            this.InternalSwitcherKeyReference.SetType(type);
            return;
        }

        /// <summary>
        /// The GetInputCut method returns the selected cut input source.
        /// </summary>
        /// <returns>BMDSwitcherInputId of the selected cut input source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.4</remarks>
        public long GetInputCut()
        {
            this.InternalSwitcherKeyReference.GetInputCut(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputCut method sets the cut input source.
        /// </summary>
        /// <param name="input">The desired cut input source’s BMDSwitcherInputId.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.5</remarks>
        public void SetInputCut(long input)
        {
            this.InternalSwitcherKeyReference.SetInputCut(input);
            return;
        }

        /// <summary>
        /// The GetInputFill method returns the selected fill input source.
        /// </summary>
        /// <returns>BMDSwitcherInputId of the selected fill input source.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.6</remarks>
        public long GetInputFill()
        {
            this.InternalSwitcherKeyReference.GetInputFill(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputFill method sets the fill input source.
        /// </summary>
        /// <param name="input">The desired fill input source’s BMDSwitcherInputId.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.7</remarks>
        public void SetinputFill(long input)
        {
            this.InternalSwitcherKeyReference.SetInputFill(input);
            return;
        }

        /// <summary>
        /// The GetFillInputAvailabilityMask method returns the corresponding <seealso cref="_BMDSwitcherInputAvailability">BMDSwitcherInputAvailability</seealso> bit mask
        /// value for fill inputs available to this key. The input availability property of an IBMDSwitcherInput can be
        /// bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this
        /// input is available for use as a fill input for this key.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.8</remarks>
        public _BMDSwitcherInputAvailability GetFillInputAvailabilityMask()
        {
            this.InternalSwitcherKeyReference.GetFillInputAvailabilityMask(out _BMDSwitcherInputAvailability availabilityMask);
            return availabilityMask;
        }

        /// <summary>
        /// The GetCutInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask
        /// value for cut inputs available to this key. The input availability property of an IBMDSwitcherInput can be
        /// bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this
        /// input is available for use as a cut input for this key.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.9</remarks>
        public _BMDSwitcherInputAvailability GetCutInputAvailabilityMask()
        {
            this.InternalSwitcherKeyReference.GetCutInputAvailabilityMask(out _BMDSwitcherInputAvailability availabilityMask);
            return availabilityMask;
        }

        /// <summary>
        /// The GetOnAir method returns the on-air flag.
        /// </summary>
        /// <returns>Boolean on-air flag.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.10</remarks>
        public bool GetOnAir()
        {
            this.InternalSwitcherKeyReference.GetOnAir(out int onAir);
            return Convert.ToBoolean(onAir);
        }

        /// <summary>
        /// The SetOnAir method sets the on-air flag.
        /// </summary>
        /// <param name="onAir">The desired on-air flag.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.11</remarks>
        public void SetOnAir(bool onAir)
        {
            this.InternalSwitcherKeyReference.SetOnAir(Convert.ToInt32(onAir));
            return;
        }

        /// <summary>
        /// The GetMasked method returns whether masking is enabled or not.
        /// </summary>
        /// <returns>Boolean flag of whether masking is enabled.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.13</remarks>
        public bool GetMasked()
        {
            this.InternalSwitcherKeyReference.GetMasked(out int maskEnabled);
            return Convert.ToBoolean(maskEnabled);
        }

        /// <summary>
        /// Use SetMasked method to enable or disable masking.
        /// </summary>
        /// <param name="maskEnabled">The desired masked value.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.14</remarks>
        public void SetMasked(bool maskEnabled)
        {
            this.InternalSwitcherKeyReference.SetMasked(Convert.ToInt32(maskEnabled));
            return;
        }

        /// <summary>
        /// The GetMaskTop method returns the current mask top value.
        /// </summary>
        /// <returns>The current mask top value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.15</remarks>
        public double GetMaskTop()
        {
            this.InternalSwitcherKeyReference.GetMaskTop(out double top);
            return top;
        }

        /// <summary>
        /// The SetMaskTop method sets the mask top value.
        /// </summary>
        /// <param name="top">The desired mask top value.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.16</remarks>
        public void SetMaskTop(double top)
        {
            this.InternalSwitcherKeyReference.SetMaskTop(top);
            return;
        }

        /// <summary>
        /// The GetMaskBottom method returns the current mask bottom value.
        /// </summary>
        /// <returns>The current mask bottom value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.17</remarks>
        public double GetMaskBottom()
        {
            this.InternalSwitcherKeyReference.GetMaskBottom(out double bottom);
            return bottom;
        }

        /// <summary>
        /// The SetMaskBottom method sets the mask bottom value.
        /// </summary>
        /// <param name="bottom">The desired mask bottom value.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.18</remarks>
        public void SetMaskBottom(double bottom)
        {
            this.InternalSwitcherKeyReference.SetMaskBottom(bottom);
            return;
        }

        /// <summary>
        /// The GetMaskLeft method returns the current mask left value.
        /// </summary>
        /// <returns>The current mask left value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.19</remarks>
        public double GetMaskLeft()
        {
            this.InternalSwitcherKeyReference.GetMaskLeft(out double left);
            return left;
        }

        /// <summary>
        /// The SetMaskLeft method sets the mask left value.
        /// </summary>
        /// <param name="left">The desired mask left value.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.20</remarks>
        public void SetMaskLeft(double left)
        {
            this.InternalSwitcherKeyReference.SetMaskLeft(left);
            return;
        }

        /// <summary>
        /// The GetMaskRight method returns the current mask right value.
        /// </summary>
        /// <returns>The current mask right value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.21</remarks>
        public double GetMaskRight()
        {
            this.InternalSwitcherKeyReference.GetMaskRight(out double right);
            return right;
        }

        /// <summary>
        /// The SetMaskRight method sets the mask right value.
        /// </summary>
        /// <param name="right">The desired mask right value.</param>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.22</remarks>
        public void SetMaskRight(double right)
        {
            this.InternalSwitcherKeyReference.SetMaskRight(right);
            return;
        }

        /// <summary>
        /// Use the ResetMask method to reset mask settings to default values.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.23</remarks>
        public void ResetMask()
        {
            this.InternalSwitcherKeyReference.ResetMask();
            return;
        }

        /// <summary>
        /// The GetTransitionSelectionMask method returns the corresponding BMDSwitcherTransitionSelection bit mask for
        /// this key.
        /// </summary>
        /// <returns>BMDSwitcherTransitionSelection bit mask.</returns>
        /// <remarks>Blackmagic Switcher SDK - 5.2.2.24</remarks>
        public _BMDSwitcherTransitionSelection GetTransitionSelectionMask()
        {
            this.InternalSwitcherKeyReference.GetTransitionSelectionMask(out _BMDSwitcherTransitionSelection selectionMask);
            return selectionMask;
        }
        #endregion

        #region IBMDSwitcherKeyCallback
        /// <summary>
        /// The Notify method is called when IBMDSwitcherKey events occur, such as property changes.
        /// This method is called from a separate thread created by the switcher SDK so care should be exercised when
        /// interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other
        /// callbacks or affecting the connection to the switcher.
        /// The return value (required by COM) is ignored by the caller.
        /// </summary>
        /// <param name="eventType">BMDSwitcherKeyEventType that describes the type of event that has occurred.</param>
        void IBMDSwitcherKeyCallback.Notify(_BMDSwitcherKeyEventType eventType)
        {
            this.OnNotify?.Invoke(this, eventType);

            switch (eventType)
            {
                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeCanBeDVEKeyChanged:
                    this.OnCanBeDVEKeyChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeInputCutChanged:
                    this.OnInputCutChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeInputFillChanged:
                    this.OnInputFillChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskBottomChanged:
                    this.OnMaskBottomChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskedChanged:
                    this.OnMaskedChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskLeftChanged:
                    this.OnMaskLeftChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskRightChanged:
                    this.OnMaskRightChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeMaskTopChanged:
                    this.OnMaskTopChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeOnAirChanged:
                    this.OnOnAirChanged?.Invoke(this, null);
                    break;

                case _BMDSwitcherKeyEventType.bmdSwitcherKeyEventTypeTypeChanged:
                    this.OnTypeChanged?.Invoke(this, null);
                    break;
            }

            return;
        }
        #endregion
    }
}
