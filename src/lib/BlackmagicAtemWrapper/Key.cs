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

namespace BlackmagicAtemWrapper
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
        private readonly IBMDSwitcherKey switcherKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="Key" /> class.
        /// </summary>
        /// <param name="switcherKey">The native <seealso cref="IBMDSwitcherKey"/> from the BMDSwitcherAPI.</param>
        public Key(IBMDSwitcherKey switcherKey)
        {
            this.switcherKey = switcherKey;
            this.switcherKey.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Key"/> class.
        /// </summary>
        ~Key()
        {
            this.switcherKey.RemoveCallback(this);
            Marshal.ReleaseComObject(this.switcherKey);
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
        /// Gets the <see cref="SwitcherKeyPatternParameters"/> property
        /// </summary>
        public SwitcherKeyPatternParameters SwitcherKeyPatternParameters => new SwitcherKeyPatternParameters(this.switcherKey as IBMDSwitcherKeyPatternParameters);

        /// <summary>
        /// Gets the <see cref="SwitcherKeyDVEParameters"/> property
        /// </summary>
        public SwitcherKeyDVEParameters SwitcherKeyDVEParameters => new SwitcherKeyDVEParameters(this.switcherKey as IBMDSwitcherKeyDVEParameters);

        /// <summary>
        /// Gets the <see cref="SwitcherKeyFlyParameters"/> property
        /// </summary>
        public SwitcherKeyFlyParameters SwitcherKeyFlyParameters => new SwitcherKeyFlyParameters(this.switcherKey as IBMDSwitcherKeyFlyParameters);
        #endregion

        /// <summary>
        /// Gets a value indicating whether this key can be set to the DVE type. The DVE
        /// hardware is a shared resource; if another component is currently using the resource, it may not be
        /// available for this key
        /// </summary>
        /// <returns>Boolean status of whether this key can be a DVE key</returns>
        public bool CanBeDVEKey
        {
            get
            {
                this.switcherKey.CanBeDVEKey(out int canDVE);
                return canDVE != 0;
            }
        }

        #region Properties
        public _BMDSwitcherKeyType Type
        {
            get { return this.GetType(); }
            set { this.SetType(value); }
        }

        public long InputCut
        {
            get { return this.GetInputCut(); }
            set { this.SetInputCut(value); }
        }

        public long InputFill
        {
            get { return this.GetInputFill(); }
            set { this.SetinputFill(value); }
        }

        public _BMDSwitcherInputAvailability FillInputAvailabilityMask
        {
            get { return this.GetFillInputAvailabilityMask(); }
        }

        public _BMDSwitcherInputAvailability CutInputAvailabilityMask
        {
            get { return this.GetCutInputAvailabilityMask(); }
        }

        public bool OnAir
        {
            get { return this.GetOnAir(); }
            set { this.SetOnAir(value); }
        }

        public bool Masked
        {
            get { return this.GetMasked(); }
            set { this.SetMasked(value); }
        }

        public double MaskTop
        {
            get { return this.GetMaskTop(); }
            set { this.SetMaskTop(value); }
        }

        public double MaskBottom
        {
            get { return this.GetMaskBottom(); }
            set { this.SetMaskBottom(value); }
        }

        public double MaskLeft
        {
            get { return this.GetMaskLeft(); }
            set { this.SetMaskLeft(value); }
        }

        public double MaskRight
        {
            get { return this.GetMaskRight(); }
            set { this.SetMaskRight(value); }
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

        /// <summary>
        /// The GetType method returns the current key type.
        /// </summary>
        /// <returns>The current key type.</returns>
        public new _BMDSwitcherKeyType GetType()
        {
            this.switcherKey.GetType(out _BMDSwitcherKeyType type);
            return type;
        }

        /// <summary>
        /// The SetType method sets the key to the specified type.
        /// </summary>
        /// <param name="type">The desired key type.</param>
        public void SetType(_BMDSwitcherKeyType type)
        {
            this.switcherKey.SetType(type);
            return;
        }

        /// <summary>
        /// The GetInputCut method returns the selected cut input source.
        /// </summary>
        /// <returns>BMDSwitcherInputId of the selected cut input source.</returns>
        public long GetInputCut()
        {
            this.switcherKey.GetInputCut(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputCut method sets the cut input source.
        /// </summary>
        /// <param name="input">The desired cut input source’s BMDSwitcherInputId.</param>
        public void SetInputCut(long input)
        {
            this.switcherKey.SetInputCut(input);
            return;
        }

        /// <summary>
        /// The GetInputFill method returns the selected fill input source.
        /// </summary>
        /// <returns>BMDSwitcherInputId of the selected fill input source.</returns>
        public long GetInputFill()
        {
            this.switcherKey.GetInputFill(out long input);
            return input;
        }

        /// <summary>
        /// The SetInputFill method sets the fill input source.
        /// </summary>
        /// <param name="input">The desired fill input source’s BMDSwitcherInputId.</param>
        public void SetinputFill(long input)
        {
            this.switcherKey.SetInputFill(input);
            return;
        }

        /// <summary>
        /// The GetFillInputAvailabilityMask method returns the corresponding <seealso cref="_BMDSwitcherInputAvailability">BMDSwitcherInputAvailability</seealso> bit mask
        /// value for fill inputs available to this key. The input availability property of an IBMDSwitcherInput can be
        /// bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this
        /// input is available for use as a fill input for this key.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        public _BMDSwitcherInputAvailability GetFillInputAvailabilityMask()
        {
            this.switcherKey.GetFillInputAvailabilityMask(out _BMDSwitcherInputAvailability availabilityMask);
            return availabilityMask;
        }

        /// <summary>
        /// The GetCutInputAvailabilityMask method returns the corresponding BMDSwitcherInputAvailability bit mask
        /// value for cut inputs available to this key. The input availability property of an IBMDSwitcherInput can be
        /// bitwise-ANDed with this mask value. If the result of the bitwise-AND is equal to the mask value then this
        /// input is available for use as a cut input for this key.
        /// </summary>
        /// <returns>BMDSwitcherInputAvailability bit mask.</returns>
        public _BMDSwitcherInputAvailability GetCutInputAvailabilityMask()
        {
            this.switcherKey.GetCutInputAvailabilityMask(out _BMDSwitcherInputAvailability availabilityMask);
            return availabilityMask;
        }

        /// <summary>
        /// The GetOnAir method returns the on-air flag.
        /// </summary>
        /// <returns>Boolean on-air flag.</returns>
        public bool GetOnAir()
        {
            this.switcherKey.GetOnAir(out int onAir);
            return onAir != 0;
        }

        /// <summary>
        /// The SetOnAir method sets the on-air flag.
        /// </summary>
        /// <param name="onAir">The desired on-air flag.</param>
        public void SetOnAir(bool onAir)
        {
            this.switcherKey.SetOnAir(onAir ? 1 : 0);
            return;
        }

        /// <summary>
        /// The GetMasked method returns whether masking is enabled or not.
        /// </summary>
        /// <returns>Boolean flag of whether masking is enabled.</returns>
        public bool GetMasked()
        {
            this.switcherKey.GetMasked(out int maskEnabled);
            return maskEnabled != 0;
        }

        /// <summary>
        /// Use SetMasked method to enable or disable masking.
        /// </summary>
        /// <param name="maskEnabled">The desired masked value.</param>
        public void SetMasked(bool maskEnabled)
        {
            this.switcherKey.SetMasked(maskEnabled ? 1 : 0);
            return;
        }

        /// <summary>
        /// The GetMaskTop method returns the current mask top value.
        /// </summary>
        /// <returns>The current mask top value.</returns>
        public double GetMaskTop()
        {
            this.switcherKey.GetMaskTop(out double top);
            return top;
        }

        /// <summary>
        /// The SetMaskTop method sets the mask top value.
        /// </summary>
        /// <param name="top">The desired mask top value.</param>
        public void SetMaskTop(double top)
        {
            this.switcherKey.SetMaskTop(top);
            return;
        }

        /// <summary>
        /// The GetMaskBottom method returns the current mask bottom value.
        /// </summary>
        /// <returns>The current mask bottom value.</returns>
        public double GetMaskBottom()
        {
            this.switcherKey.GetMaskBottom(out double bottom);
            return bottom;
        }

        /// <summary>
        /// The SetMaskBottom method sets the mask bottom value.
        /// </summary>
        /// <param name="bottom">The desired mask bottom value.</param>
        public void SetMaskBottom(double bottom)
        {
            this.switcherKey.SetMaskBottom(bottom);
            return;
        }

        /// <summary>
        /// The GetMaskLeft method returns the current mask left value.
        /// </summary>
        /// <returns>The current mask left value.</returns>
        public double GetMaskLeft()
        {
            this.switcherKey.GetMaskLeft(out double left);
            return left;
        }

        /// <summary>
        /// The SetMaskLeft method sets the mask left value.
        /// </summary>
        /// <param name="left">The desired mask left value.</param>
        public void SetMaskLeft(double left)
        {
            this.switcherKey.SetMaskLeft(left);
            return;
        }

        /// <summary>
        /// The GetMaskRight method returns the current mask right value.
        /// </summary>
        /// <returns>The current mask right value.</returns>
        public double GetMaskRight()
        {
            this.switcherKey.GetMaskRight(out double right);
            return right;
        }

        /// <summary>
        /// The SetMaskRight method sets the mask right value.
        /// </summary>
        /// <param name="right">The desired mask right value.</param>
        public void SetMaskRight(double right)
        {
            this.switcherKey.SetMaskRight(right);
            return;
        }

        /// <summary>
        /// Use the ResetMask method to reset mask settings to default values.
        /// </summary>
        public void ResetMask()
        {
            this.switcherKey.ResetMask();
        }

        /// <summary>
        /// The GetTransitionSelectionMask method returns the corresponding BMDSwitcherTransitionSelection bit mask for
        /// this key.
        /// </summary>
        /// <returns>BMDSwitcherTransitionSelection bit mask.</returns>
        public _BMDSwitcherTransitionSelection GetTransitionSelectionMask()
        {
            this.switcherKey.GetTransitionSelectionMask(out _BMDSwitcherTransitionSelection selectionMask);
            return selectionMask;
        }
    }
}
