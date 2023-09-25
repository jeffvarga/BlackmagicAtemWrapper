//-----------------------------------------------------------------------------
// <copyright file="FairlightAudioDynamicsProcessor.cs">
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


namespace BlackmagicAtemWrapper.Audio.Effects
{
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The <see cref="FairlightAudioDynamicsProcessor"/> class is the root object for all Fairlight audio dynamics processing.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 7.5.14</remarks>
    public class FairlightAudioDynamicsProcessor : IBMDSwitcherFairlightAudioDynamicsProcessorCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherFairlightAudioDynamicsProcessor"/>.
        /// </summary>
        private readonly IBMDSwitcherFairlightAudioDynamicsProcessor InternalFairlightAudioDynamicsProcessorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioDynamicsProcessor"/> class.
        /// </summary>
        /// <param name="audioMixer">The native <seealso cref="IBMDSwitcherFairlightAudioDynamicsProcessor"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioDynamicsProcessor(IBMDSwitcherFairlightAudioDynamicsProcessor audioMixer)
        {
            this.InternalFairlightAudioDynamicsProcessorReference = audioMixer ?? throw new System.ArgumentNullException(nameof(audioMixer));
            this.InternalFairlightAudioDynamicsProcessorReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FairlightAudioDynamicsProcessor"/> class.
        /// </summary>
        ~FairlightAudioDynamicsProcessor()
        {
            this.InternalFairlightAudioDynamicsProcessorReference.RemoveCallback(this);
            Marshal.ReleaseComObject(this.InternalFairlightAudioDynamicsProcessorReference);
        }

        #region Events
        /// <summary>
        /// Handles a <see cref="FairlightAudioDynamicsProcessor"/> event.
        /// </summary>
        /// <param name="sender">The object that received the event.</param>
        public delegate void FairlightAudioDynamicsProcessorEventHandler(object sender);

        /// <summary>
        /// The <see cref="MakeupGain"/> value changed.
        /// </summary>
        public event FairlightAudioDynamicsProcessorEventHandler OnMakeupGainChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the make up gain value.
        /// </summary>
        public double MakeupGain
        {
            get { return this.GetMakeupGain(); }
            set { this.SetMakeupGain(value); }
        }
        #endregion

        #region IBMDSwitcherFairlightAudioDynamicsProcessor
        /// <summary>
        /// The CreateIterator method returns the dynamics processor object interface for the specified interface ID, such as IBMDSwitcherFairlightAudioLimiter, IBMDSwitcherFairlightAudioCompressor, IBMDSwitcherFairlightAudioExpander.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 7.5.14.1</remarks>
        /// <bug>Documentation refers to CreateIterator</bug>
        void GetProcessor()
        {
            return;
        }

        /// <summary>
        /// The GetMakeupGain method returns the current make up gain value.
        /// </summary>
        /// <returns>The current make up gain value.</returns>
        /// <remarks>Blackmagic Switcher SDK - 7.5.14.2</remarks>
        public double GetMakeupGain()
        {
            this.InternalFairlightAudioDynamicsProcessorReference.GetMakeupGain(out double gain);
            return gain;
        }

        /// <summary>
        /// The SetMakeupGain method sets the make up gain value.
        /// </summary>
        /// <param name="gain">The desired make up gain value.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.14.3</remarks>
        public void SetMakeupGain(double gain)
        { 
            try
            {
                this.InternalFairlightAudioDynamicsProcessorReference.SetMakeupGain(gain);
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
        /// The Reset method resets the dynamics to its default state.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.14.4</remarks>
        public void Reset()
        { 
            try
            {
                this.InternalFairlightAudioDynamicsProcessorReference.Reset();
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
        /// The ResetInputPeakLevels method resets the peak input level statistics.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.14.5</remarks>
        /// <bug>Title has a space in function name.</bug>
        public void ResetInputPeakLevels()
        { 
            try
            {
                this.InternalFairlightAudioDynamicsProcessorReference.ResetInputPeakLevels();
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
        /// The ResetOutputPeakLevels method resets the peak output level statistics.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 7.5.14.6</remarks>
        /// <bug>Title has a space in function name</bug>
        public void ResetOutputPeakLevels()
        { 
            try
            {
                this.InternalFairlightAudioDynamicsProcessorReference.ResetOutputPeakLevels();
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

        #region IBMDSwitcherFairlightAudioDynamicsProcessorCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherFairlightAudioDynamicsProcessor events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherFairlightAudioDynamics ProcessorEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.15.1</remarks>
        void IBMDSwitcherFairlightAudioDynamicsProcessorCallback.Notify(_BMDSwitcherFairlightAudioDynamicsProcessorEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherFairlightAudioDynamicsProcessorEventType.bmdSwitcherFairlightAudioDynamicsProcessorEventTypeMakeupGainChanged:
                    this.OnMakeupGainChanged?.Invoke(this);
                    break;
            }

            return;
        }

        /// <summary>
        /// <para>The InputLevelNotification method is called periodically to report the current dB input levels and the last known peak levels.These peak levels can be reset using IBMSwitcherFairlightAudioDynamicsProcessor::ResetInputPeakLevels.</para>
        /// <para>Note that this is an opt-in subscription.Enable or disable receiving these calls using IBMDSwitcherFairlightAudioMixer::SetAllLevelNotificationsEnabled.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="numLevels">The number of input levels.</param>
        /// <param name="levels">The current input dB levels.</param>
        /// <param name="numPeakLevels">The number of input peak levels.</param>
        /// <param name="peakLevels">The highest encountered input peak dB level.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.15.2</remarks>
        void IBMDSwitcherFairlightAudioDynamicsProcessorCallback.InputLevelNotification(uint numLevels, ref double levels, uint numPeakLevels, ref double peakLevels)
        {
        }

        /// <summary>
        /// <para>The OutputLevelNotification method is called periodically to report the current dB output levels and the last known peak levels.These peak levels can be reset using IBMDSwitcherFairlightAudioDynamicsProcessor::ResetOutputPeakLevels.</para>
        /// <para>Note that this is an opt-in subscription.Enable or disable receiving these calls using IBMDSwitcherFairlightAudioMixer::SetAllLevelNotificationsEnabled.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="numLevels">The number of output levels.</param>
        /// <param name="levels">The current output dB levels.</param>
        /// <param name="numPeakLevels">The number of output peak levels.</param>
        /// <param name="peakLevels">The highest encountered output peak level.</param>
        /// <remarks>Blackmagic Switcher SDK - 7.5.15.3</remarks>
        void IBMDSwitcherFairlightAudioDynamicsProcessorCallback.OutputLevelNotification(uint numLevels, ref double levels, uint numPeakLevels, ref double peakLevels)
        {
        }
        #endregion
    }
}
