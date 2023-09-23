//-----------------------------------------------------------------------------
// <copyright file="MacroControl.cs">
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

namespace BlackmagicAtemWrapper.Macros
{
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The <see cref="MacroControl"/> class provides macro recording state, playback state, and control.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 9.3.5</remarks>
    public class MacroControl : IBMDSwitcherMacroControlCallback
    {
        /// <summary>
        /// Internal reference to the raw <see cref="IBMDSwitcherMacroControl"/>
        /// </summary>
        private readonly IBMDSwitcherMacroControl InternalMacroControlReference;

        /// <summary>
        /// Initializes an instance of the <see cref="MacroControl"/> class.
        /// </summary>
        /// <param name="macro">The native <see cref="IBMDSwitcherMacroControl"/> from the BMDSwitcherAPI.</param>
        public MacroControl(IBMDSwitcherMacroControl macro)
        {
            this.InternalMacroControlReference = macro;
            this.InternalMacroControlReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MacroControl"/> class.
        /// </summary>
        ~MacroControl()
        {
            this.InternalMacroControlReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalMacroControlReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="MacroControl"/>.
        /// </summary>
        /// <param name="sender">The <see cref="MacroControl"/> that received the event.</param>
        public delegate void MacroControlEventHandler(object sender);

        /// <summary>
        /// The switcher’s macro playback state has changed.
        /// </summary>
        public event MacroControlEventHandler OnRunStatusChanged;

        /// <summary>
        /// The switcher’s macro record state has changed.
        /// </summary>
        public event MacroControlEventHandler OnRecordStatusChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether the loop flag is enabled.
        /// </summary>
        public bool Loop
        {
            get { return this.GetLoop(); }
            set { this.SetLoop(value); }
        }
        #endregion

        #region IBMDSwitcherMacroControl
        /// <summary>
        /// The Run method begins playback of a macro.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <exception cref="ArgumentException">The <paramref name="index"/> parameter is out of range or invalid.</exception>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.1</remarks>
        public void Run(uint index)
        {
            try
            {
                this.InternalMacroControlReference.Run(index);
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
        /// The GetLoop method gets the current loop setting. When true, a running macro will loop back to the start when the last operation completes.
        /// </summary>
        /// <returns>Boolean value which is true if playback will loop.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.2</remarks>
        public bool GetLoop()
        {
            try
            {
                this.InternalMacroControlReference.GetLoop(out int loop);
                return Convert.ToBoolean(loop);
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
        /// The GetLoop method gets the current loop setting. When true, a running macro will loop back to the start when the last operation completes.
        /// </summary>
        /// <param name="loop">Boolean value which is true if playback will loop.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.2</remarks>
        public void SetLoop(bool loop)
        {
            try
            {
                this.InternalMacroControlReference.SetLoop(Convert.ToInt32(loop));
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
        /// The ResumeRunning method continues playback of a macro that is waiting for the user. If there is no macro currently waiting then this method has no effect.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.4</remarks>
        public void ResumeRunning()
        {
            try
            {
                this.InternalMacroControlReference.ResumeRunning();
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
        /// The StopRunning method stops the currently playing macro. If there is no macro currently playing then this method has no effect.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.5</remarks>
        public void StopRunning()
        {
            try
            {
                this.InternalMacroControlReference.StopRunning();
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
        /// The Record method begins recording of a new macro.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <param name="name">Name of the macro.</param>
        /// <param name="description">Description of the macro.</param>
        /// <exception cref="ArgumentException">The <paramref name="index"/> parameter is out of range.</exception>
        /// <exception cref="OutOfMemoryException">Insufficient memory to record a new macro.</exception>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.6</remarks>
        public void Record(uint index, string name, string description)
        {
            try
            {
                this.InternalMacroControlReference.Record(index, name, description);
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
        /// The RecordUserWait method inserts a user wait into the currently recording macro. If there is no macro currently recording then this method has no effect.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.7</remarks>
        public void RecordUserWait()
        {
            try
            {
                this.InternalMacroControlReference.RecordUserWait();
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
        /// The RecordPause method inserts a timed pause into the currently recording macro. If there is no macro currently recording then this method has no effect.
        /// </summary>
        /// <param name="frames">Number of frames to pause for.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.8</remarks>
        public void RecordPause(uint frames)
        {
            try
            {
                this.InternalMacroControlReference.RecordPause(frames);
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
        /// The StopRecording method stops the currently recording macro. If there is no macro currently recording then this method has no effect.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.9</remarks>
        public void StopRecording()
        {
            try
            {
                this.InternalMacroControlReference.StopRecording();
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
        /// The GetRunStatus method gets the current playback state of the switcher.
        /// </summary>
        /// <param name="status">BMDSwitcherMacroRunStatus value indicating the macro playback state.</param>
        /// <param name="loop">Boolean value which is true if playback is set to loop.</param>
        /// <param name="index">Index of the macro that is playing/waiting.</param>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.10</remarks>
        public void GetRunStatus(out _BMDSwitcherMacroRunStatus status, out bool loop, out uint index)
        {
            this.InternalMacroControlReference.GetRunStatus(out status, out int loopI, out index);
            loop = Convert.ToBoolean(loopI);
            return;
        }

        /// <summary>
        /// The GetRecordStatus method gets the current record state of the switcher.
        /// </summary>
        /// <param name="status">BMDSwitcherMacroRecordStatus value indicating the macro recording state.</param>
        /// <param name="index">Index of the macro that is recording.</param>
        /// <remarks>Blackmagic Switcher SDK - 9.3.5.11</remarks>
        public void GetRecordStatus(out _BMDSwitcherMacroRecordStatus status, out uint index)
        {
            this.InternalMacroControlReference.GetRecordStatus(out status, out index);
            return;
        }
        #endregion

        #region IBMDSwitcherMacroControlCallback
        /// <summary>
        /// <para>The Notify method is called when an IBMDSwitcherMacroControl event occurs, such as a macro playback and recording states.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherMacroControlEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 9.3.6.1</remarks>
        void IBMDSwitcherMacroControlCallback.Notify(_BMDSwitcherMacroControlEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherMacroControlEventType.bmdSwitcherMacroControlEventTypeRunStatusChanged:
                    this.OnRunStatusChanged?.Invoke(this);
                    break;

                case _BMDSwitcherMacroControlEventType.bmdSwitcherMacroControlEventTypeRecordStatusChanged:
                    this.OnRecordStatusChanged?.Invoke(this);
                    break;
            }
        }
        #endregion
    }
}
