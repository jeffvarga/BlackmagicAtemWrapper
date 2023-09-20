//-----------------------------------------------------------------------------
// <copyright file="TransitionParameters.cs">
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

namespace BlackmagicAtemWrapper.Transitions
{
    using System;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The TransitionParameters class is used for manipulating transition settings specific to mix parameters.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 3.2.11</remarks>
    public class TransitionParameters : IBMDSwitcherTransitionParametersCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMixEffectBlock"/>.
        /// </summary>
        private readonly IBMDSwitcherTransitionParameters InternalTransitionParametersReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransitionParameters"/> class.
        /// </summary>
        /// <param name="transitionParameters">The native <seealso cref="IBMDSwitcherTransitionParameters"/> from the BMDSwitcherAPI.</param>
        public TransitionParameters(IBMDSwitcherTransitionParameters transitionParameters)
        {
            this.InternalTransitionParametersReference = transitionParameters ?? throw new ArgumentNullException(nameof(transitionParameters));
            this.InternalTransitionParametersReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TransitionParameters"/> class.
        /// </summary>
        ~TransitionParameters()
        {
            this.InternalTransitionParametersReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalTransitionParametersReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="TransitionParameters"/>.
        /// </summary>
        /// <param name="sender">The <see cref="TransitionParameters"/> that received the event.</param>
        public delegate void TransitionParametersEventHandler(object sender);

        /// <summary>
        /// <see cref="TransitionStyle"/> value changed.
        /// </summary>
        public event TransitionParametersEventHandler OnTransitionStyleChanged;

        /// <summary>
        /// The <see cref="NextTransitionStyle"/> value changed.
        /// </summary>
        public event TransitionParametersEventHandler OnNextTransitionStyleChanged;

        /// <summary>
        /// The <see cref="TransitionSelection"/> value changed.
        /// </summary>
        public event TransitionParametersEventHandler OnTransitionSelectionChanged;

        /// <summary>
        /// The <see cref="NextTransitionSelection"/> value changed.
        /// </summary>
        public event TransitionParametersEventHandler OnNextTransitionSelectionChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the current transition style.
        /// </summary>
        public _BMDSwitcherTransitionStyle TransitionStyle
        {
            get { return this.GetTransitionStyle(); }
        }

        /// <summary>
        /// Gets or sets the next transition style.
        /// </summary>
        public _BMDSwitcherTransitionStyle NextTransitionStyle
        {
            get { return this.GetNextTransitionStyle(); }
            set { this.SetNextTransitionStyle(value); }
        }

        /// <summary>
        /// Gets the current transition selection.
        /// </summary>
        public _BMDSwitcherTransitionSelection TransitionSelection
        {
            get { return this.GetTransitionSelection(); }
        }

        /// <summary>
        /// Gets or sets the next transition selection.
        /// </summary>
        public _BMDSwitcherTransitionSelection NextTransitionSelection
        {
            get { return this.GetNextTransitionSelection(); }
            set { this.SetNextTransitionSelection(value); }
        }
        #endregion

        #region IBMDSwitcherTransitionParameters
        /// <summary>
        /// The GetTransitionStyle method returns the current transition style.
        /// </summary>
        /// <returns>The current transition style.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.11.1</remarks>
        public _BMDSwitcherTransitionStyle GetTransitionStyle()
        {
            this.InternalTransitionParametersReference.GetTransitionStyle(out _BMDSwitcherTransitionStyle style);
            return style;
        }

        /// <summary>
        /// The GetNextTransitionStyle method returns the next transition style.
        /// </summary>
        /// <returns>The next transition style.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.11.2</remarks>
        public _BMDSwitcherTransitionStyle GetNextTransitionStyle()
        {
            this.InternalTransitionParametersReference.GetNextTransitionStyle(out _BMDSwitcherTransitionStyle style);
            return style;
        }

        /// <summary>
        /// The SetNextTransitionStyle method sets the rate in frames.
        /// </summary>
        /// <param name="style">The desired style.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.11.3</remarks>
        public void SetNextTransitionStyle(_BMDSwitcherTransitionStyle style)
        { 
            try
            {
                this.InternalTransitionParametersReference.SetNextTransitionStyle(style);
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
        /// The GetTransitionSelection method returns the current transition selection.
        /// </summary>
        /// <returns>The current transition selection.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.11.4</remarks>
        public _BMDSwitcherTransitionSelection GetTransitionSelection()
        {
            this.InternalTransitionParametersReference.GetTransitionSelection(out _BMDSwitcherTransitionSelection selection);
            return selection;
        }

        /// <summary>
        /// The SetNextTransitionSelection method sets the next transition selection.
        /// </summary>
        /// <param name="style">The desired next transition selection.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 3.2.11.5</remarks>
        public void SetNextTransitionSelection(_BMDSwitcherTransitionSelection style)
        {
            try
            {
                this.InternalTransitionParametersReference.SetNextTransitionSelection(style);
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
        /// The GetNextTransitionSelection method returns the next transition selection.
        /// </summary>
        /// <returns>The next transition selection.</returns>
        /// <remarks>Blackmagic Switcher SDK - 3.2.11.6</remarks>
        public _BMDSwitcherTransitionSelection GetNextTransitionSelection()
        {
            this.InternalTransitionParametersReference.GetNextTransitionSelection(out _BMDSwitcherTransitionSelection selection);
            return selection;
        }
        #endregion

        #region IBMDSwitcherTransitionParametersCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherTransitionParameters events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherTransitionParametersEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 3.2.12.1</remarks>
        void IBMDSwitcherTransitionParametersCallback.Notify(_BMDSwitcherTransitionParametersEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherTransitionParametersEventType.bmdSwitcherTransitionParametersEventTypeTransitionStyleChanged:
                    this.OnTransitionStyleChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionParametersEventType.bmdSwitcherTransitionParametersEventTypeNextTransitionStyleChanged:
                    this.OnNextTransitionStyleChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionParametersEventType.bmdSwitcherTransitionParametersEventTypeTransitionSelectionChanged:
                    this.OnTransitionSelectionChanged?.Invoke(this);
                    break;

                case _BMDSwitcherTransitionParametersEventType.bmdSwitcherTransitionParametersEventTypeNextTransitionSelectionChanged:
                    this.OnNextTransitionSelectionChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
