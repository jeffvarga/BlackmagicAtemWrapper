//-----------------------------------------------------------------------------
// <copyright file="StreamRTMP.cs">
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

namespace BlackmagicAtemWrapper.Streaming
{
    using System;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The <see cref="StreamRTMP"/> class provides functionality to start and stop streaming of video and audio to a streaming server.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 11.3.1</remarks>
    public class StreamRTMP : IBMDSwitcherStreamRTMPCallback
    {
        /// <summary>
        /// Internal reference to the raw <see cref="IBMDSwitcherStreamRTMP"/>
        /// </summary>
        private readonly IBMDSwitcherStreamRTMP InternalStreamRTMPReference;

        /// <summary>
        /// Initializes an instances of the <see cref="StreamRTMP"/> class.
        /// </summary>
        /// <param name="stream">The native <see cref="IBMDSwitcherStreamRTMP"/> from the BMDSwitcherAPI.</param>
        public StreamRTMP(IBMDSwitcherStreamRTMP stream)
        {
            this.InternalStreamRTMPReference = stream;
            this.InternalStreamRTMPReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="StreamRTMP"/> class.
        /// </summary>
        ~StreamRTMP()
        {
            this.InternalStreamRTMPReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalStreamRTMPReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="StreamRTMP"/>.
        /// </summary>
        /// <param name="sender">The <see cref="StreamRTMP"/> that received the event.</param>
        public delegate void StreamRTMPEventHandler(object sender);

        /// <summary>
        /// A delegate to handle events from <see cref="StreamRTMP"/> streaming state changes.
        /// </summary>
        /// <param name="sender">The <see cref="StreamRTMP"/> that received the event.</param>
        /// <param name="error">BMDSwitcherStreamRTMPError of the error associated with the current streaming state.</param>
        public delegate void StreamRTMPStreamEventHandler(object sender, _BMDSwitcherStreamRTMPError error);

        /// <summary>
        /// The <see cref="ServiceName"/> value changed.
        /// </summary>
        public event StreamRTMPEventHandler OnServiceNameChanged;

        /// <summary>
        /// The <see cref="Url"/> value changed.
        /// </summary>
        public event StreamRTMPEventHandler OnUrlChanged;

        /// <summary>
        /// The <see cref="Key"/> value changed.
        /// </summary>
        public event StreamRTMPEventHandler OnKeyChanged;

        /// <summary>
        /// The video bitrates changed.
        /// </summary>
        /// <see cref="GetVideoBitrates"/>
        public event StreamRTMPEventHandler OnVideoBitratesChanged;

        /// <summary>
        /// The audio bitrates changed.
        /// </summary>
        /// <see cref="GetAudioBitrates"/>
        public event StreamRTMPEventHandler OnAudioBitratesChanged;

        /// <summary>
        /// The <see cref="EncodingBitrate"/> changed.
        /// </summary>
        /// 
        public event StreamRTMPEventHandler OnEncodingBitrateChanged;

        /// <summary>
        /// The <see cref="CacheUsed"/> value changed.
        /// </summary>
        public event StreamRTMPEventHandler OnCacheUsedChanged;

        /// <summary>
        /// The <see cref="Timecode"/> value changed.
        /// </summary>
        public event StreamRTMPEventHandler OnTimecodeChanged;

        /// <summary>
        /// The <see cref="StreamDuration"/> value changed.
        /// </summary>
        public event StreamRTMPEventHandler OnDurationChanged;

        /// <summary>
        /// The authentication credentials changed.
        /// </summary>
        /// <see cref="GetAuthentication"/>
        /// <see cref="SetAuthentication"/>
        public event StreamRTMPEventHandler OnAuthenticationChanged;

        /// <summary>
        /// The <see cref="IsLowLatency"/> flag changed;
        /// </summary>
        public event StreamRTMPEventHandler OnLowLatencyChanged;

        /// <summary>
        /// Not streaming.
        /// </summary>
        public event StreamRTMPStreamEventHandler OnIdle;

        /// <summary>
        /// Connecting to the streaming server.
        /// </summary>
        public event StreamRTMPStreamEventHandler OnConnecting;

        /// <summary>
        /// Streaming is in progress.
        /// </summary>
        public event StreamRTMPStreamEventHandler OnStreaming;

        /// <summary>
        /// Streaming is stopping.
        /// </summary>
        public event StreamRTMPStreamEventHandler OnStopping;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether the switcher is currently streaming.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.3</remarks>
        public bool IsStreaming
        {
            get
            {
                this.InternalStreamRTMPReference.IsStreaming(out int streaming);
                return Convert.ToBoolean(streaming);
            }
        }

        /// <summary>
        /// Gets or sets the service name.
        /// </summary>
        public string ServiceName
        {
            get { return this.GetServiceName(); }
            set { this.SetServiceName(value); }
        }

        /// <summary>
        /// Gets or sets the streaming url.
        /// </summary>
        public string Url
        {
            get { return this.GetUrl(); }
            set { this.SetUrl(value); }
        }

        /// <summary>
        /// Gets or sets the streaming key.
        /// </summary>
        public string Key
        {
            get { return this.GetKey(); }
            set { this.SetKey(value); }
        }

        /// <summary>
        /// Gets the number of frames streamed.
        /// </summary>
        public ulong StreamDuration
        {
            get { return this.GetDuration(); }
        }

        /// <summary>
        /// Gets the current timecode.
        /// </summary>
        public Timecode Timecode
        {
            get { return this.GetTimeCode(); }
        }

        /// <summary>
        /// Gets the current encoding bitrate, in bits per second.
        /// </summary>
        public uint EncodingBitrate
        {
            get { return this.GetEncodingBitrate(); }
        }

        /// <summary>
        /// Gets the current usage level of the streaming cache.
        /// </summary>
        public double CacheUsed
        {
            get { return this.GetCacheUsed(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the switcher is in low latency mode.
        /// </summary>
        public bool IsLowLatency
        {
            get { return this.GetLowLatency(); }
            set { this.SetLowLatency(value); }
        }
        #endregion

        #region IBMDSwitcherStreamRTMP
        /// <summary>
        /// The StartStreaming method starts video and audio streaming to the configured streaming server.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.1</remarks>
        public void StartStreaming()
        {
            try
            {
                this.InternalStreamRTMPReference.StartStreaming();
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
        /// The StopStreaming method stops video and audio streaming to the configured streaming server.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.2</remarks>
        public void StopStreaming()
        {
            try
            {
                this.InternalStreamRTMPReference.StopStreaming();
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
        /// The GetStatus method returns the current streaming status.
        /// </summary>
        /// <param name="state">BMDSwitcherRecordAVState value indicating the current streaming status.</param>
        /// <param name="error">BMDSwitcherStreamRTMPError value indicating the error associated with current streaming status.</param>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.4</remarks>
        public void GetStatus(out _BMDSwitcherStreamRTMPState state, out _BMDSwitcherStreamRTMPError error)
        {
            this.InternalStreamRTMPReference.GetStatus(out state, out error);
        }

        /// <summary>
        /// The SetServiceName method sets the streaming service name. The name is only used for display purposes.
        /// </summary>
        /// <param name="serviceName">Name of the streaming service.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.5</remarks>
        public void SetServiceName(string serviceName)
        {
            try
            {
                this.InternalStreamRTMPReference.SetServiceName(serviceName);
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
        /// The GetServiceName method gets the name of the streaming service.
        /// </summary>
        /// <returns>Name of the streaming service.</returns>
        /// <exception cref="OutOfMemoryException">Insufficient memory to get the service name.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.6</remarks>
        public string GetServiceName()
        {
            this.InternalStreamRTMPReference.GetServiceName(out string serviceName);
            return serviceName;
        }

        /// <summary>
        /// The SetUrl method sets the streaming server URL.
        /// </summary>
        /// <param name="url">Streaming server URL.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.7</remarks>
        public void SetUrl(string url)
        {
            try
            {
                this.InternalStreamRTMPReference.SetUrl(url);
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
        /// The GetUrl method gets the streaming server URL.
        /// </summary>
        /// <returns>Streaming server URL.</returns>
        /// <exception cref="OutOfMemoryException">Insufficient memory to get the URL.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.8</remarks>
        public string GetUrl()
        {
            this.InternalStreamRTMPReference.GetUrl(out string url);
            return url;
        }

        /// <summary>
        /// The SetKey method sets the streaming server key.
        /// </summary>
        /// <param name="key">Streaming server key.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.9</remarks>
        public void SetKey(string key)
        {
            try
            {
                this.InternalStreamRTMPReference.SetKey(key);
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
        /// The GetKey method gets the streaming server key.
        /// </summary>
        /// <returns>Streaming server key.</returns>
        /// <exception cref="OutOfMemoryException">Insufficient memory to get the key.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.10</remarks>
        public string GetKey()
        {
            this.InternalStreamRTMPReference.GetKey(out string key);
            return key;
        }

        /// <summary>
        /// The SetVideoBitrate method sets the maximum video streaming bitrates, in bits per second. The low bitrate is used for framerates of 30p and lower.The high bitrate is used for framerates of p50 and higher.
        /// </summary>
        /// <param name="lowBitrate">Maximum video streaming bitrate for low framerates, in bits per second.</param>
        /// <param name="highBitrate">Maximum video streaming bitrate for high framerates, in bits per second.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.11</remarks>
        public void SetVideoBitrates(uint lowBitrate, uint highBitrate)
        {
            try
            {
                this.InternalStreamRTMPReference.SetVideoBitrates(lowBitrate, highBitrate);
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
        /// The GetVideoBitrate method gets the current maximum video streaming bitrates, in bits per second. The low bitrate is used for framerates of 30p and lower.The high bitrate is used for framerates of p50 and higher.
        /// </summary>
        /// <param name="lowBitrate">Maximum video streaming bitrate for low framerates, in bits per second.</param>
        /// <param name="highBitrate">Maximum video streaming bitrate for high framerates, in bits per second.</param>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.12</remarks>
        public void GetVideoBitrates(out uint lowBitrate, out uint highBitrate)
        {
            this.InternalStreamRTMPReference.GetVideoBitrates(out lowBitrate, out highBitrate);
            return;
        }

        /// <summary>
        /// The SetAudioBitrate method sets the maximum audio streaming bitrates, in bits per second. The low bitrate is used for framerates of 30p and lower.The high bitrate is used for framerates of p50 and higher.
        /// </summary>
        /// <param name="lowBitrate">Maximum audio streaming bitrate for low framerates, in bits per second.</param>
        /// <param name="highBitrate">Maximum audio streaming bitrate for high framerates, in bits per second.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.13</remarks>
        public void SetAudioBitrates(uint lowBitrate, uint highBitrate)
        {
            try
            {
                this.InternalStreamRTMPReference.SetAudioBitrates(lowBitrate, highBitrate);
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
        /// The GetAudioBitrate method gets the current maximum audio streaming bitrates, in bits per second. The low bitrate is used for framerates of 30p and lower.The high bitrate is used for framerates of p50 and higher.
        /// </summary>
        /// <param name="lowBitrate">Maximum audio streaming bitrate for low framerates, in bits per second.</param>
        /// <param name="highBitrate">Maximum audio streaming bitrate for high framerates, in bits per second.</param>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.14</remarks>
        public void GetAudioBitrates(out uint lowBitrate, out uint highBitrate)
        {
            this.InternalStreamRTMPReference.GetAudioBitrates(out lowBitrate, out highBitrate);
            return;
        }

        /// <summary>
        /// The RequestDuration method requests the current streaming duration and timecode from the switcher which will be cached when received.Use the GetDuration and GetTimecode methods to get the cached duration and cached timecode, respectively.
        /// </summary>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.15</remarks>
        public void RequestDuration()
        {
            try
            {
                this.InternalStreamRTMPReference.RequestDuration();
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
        /// The GetDuration method returns the streaming duration (in frames) that was last received from the switcher.
        /// </summary>
        /// <returns>Recording duration (in frames).</returns>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.16</remarks>
        public ulong GetDuration()
        {
            this.InternalStreamRTMPReference.GetDuration(out ulong duration);
            return duration;
        }

        /// <summary>
        /// The GetTimeCode method returns the streaming timecode that was last received from the switcher.
        /// </summary>
        /// <returns>The timecode object for the current timecode.</returns>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.17</remarks>
        /// <bug>Documentation incorrectly cases GetTimeCode as GetTimecode</bug>
        public Timecode GetTimeCode()
        {
            this.InternalStreamRTMPReference.GetTimeCode(
                out byte hours,
                out byte minutes,
                out byte seconds,
                out byte frames,
                out int dropFrame);
            return new(hours, minutes, seconds, frames, Convert.ToBoolean(dropFrame));
        }

        /// <summary>
        /// The GetEncodingBitrate method returns the current encoding bitrate, in bits per second.
        /// </summary>
        /// <returns>The current encoding bitrate.</returns>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.18</remarks>
        public uint GetEncodingBitrate()
        {
            this.InternalStreamRTMPReference.GetEncodingBitrate(out uint encodingBitrate);
            return encodingBitrate;
        }

        /// <summary>
        /// The GetCacheUsed method returns the current usage level of the streaming cache, as a value with range 0.0 to 1.0.
        /// </summary>
        /// <returns>Current usage level of the streaming cache.</returns>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.19</remarks>
        public double GetCacheUsed()
        {
            this.InternalStreamRTMPReference.GetCacheUsed(out double cacheUsed);
            return cacheUsed;
        }

        /// <summary>
        /// The SetAuthentication method sets the streaming server authentication credentials.
        /// </summary>
        /// <param name="username">Streaming server authentication username.</param>
        /// <param name="password">Streaming server authentication password.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.20</remarks>
        public void SetAuthentication(string username, String password)
        {
            try
            {
                this.InternalStreamRTMPReference.SetAuthentication(username, password);
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
        /// The GetAuthentication method gets the streaming server authentication credentials.
        /// </summary>
        /// <param name="username">Streaming server authentication username.</param>
        /// <param name="password">Streaming server authentication password.</param>
        /// <exception cref="OutOfMemoryException">Out of memory, could not assign the username and/or password parameters.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.21</remarks>
        public void GetAuthentication(out string username, out string password)
        {
            this.InternalStreamRTMPReference.GetAuthentication(out username, out password);
            return;
        }

        /// <summary>
        /// The SetLowLatency method changes the low latency mode. If low latency is enabled, frames will be dropped during times of poor network performance to prevent the stream from lagging too far behind the live output of the switcher.This flag can only be changed when the switcher is not recording.
        /// </summary>
        /// <param name="lowLatency">Boolean value indicating whether low latency is active.</param>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.22</remarks>
        public void SetLowLatency(bool lowLatency)
        {
            try
            {
                this.InternalStreamRTMPReference.SetLowLatency(Convert.ToInt32(lowLatency));
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
        /// The GetLowLatency method returns the low latency mode.
        /// </summary>
        /// <returns>Boolean value indicating whether low latency is active.</returns>
        /// <remarks>Blackmagic Switcher SDK - 11.3.1.23</remarks>
        public bool GetLowLatency()
        {
            this.InternalStreamRTMPReference.GetLowLatency(out int lowLatency);
            return Convert.ToBoolean(lowLatency);
        }
        #endregion

        #region IBMDSwitcherStreamRTMPCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherStreamRTMP events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherStreamRTMPEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 11.3.2.1</remarks>
        void IBMDSwitcherStreamRTMPCallback.Notify(_BMDSwitcherStreamRTMPEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeServiceNameChanged:
                    this.OnServiceNameChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeUrlChanged:
                    this.OnUrlChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeKeyChanged:
                    this.OnKeyChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeVideoBitratesChanged:
                    this.OnVideoBitratesChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeAudioBitratesChanged:
                    this.OnAudioBitratesChanged.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeEncodingBitrateChanged:
                    this.OnEncodingBitrateChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeCacheUsedChanged:
                    this.OnCacheUsedChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeTimecodeChanged:
                    this.OnTimecodeChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeDurationChanged:
                    this.OnDurationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeAuthenticationChanged:
                    this.OnAuthenticationChanged?.Invoke(this);
                    break;

                case _BMDSwitcherStreamRTMPEventType.bmdSwitcherStreamRTMPEventTypeLowLatencyChanged:
                    this.OnLowLatencyChanged?.Invoke(this);
                    break;
            }

            return;
        }

        /// <summary>
        /// <para>The NotifyStatus method is called when the streaming status has changed.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads.Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="stateType">BMDSwitcherStreamRTMPState that describes the current streaming state.</param>
        /// <param name="error">BMDSwitcherStreamRTMPError of the error associated with the current streaming state.</param>
        /// <remarks>Blackmagic Switcher SDK - 11.3.2.2</remarks>
        void IBMDSwitcherStreamRTMPCallback.NotifyStatus(_BMDSwitcherStreamRTMPState stateType, _BMDSwitcherStreamRTMPError error)
        {
            switch (stateType)
            {
                case _BMDSwitcherStreamRTMPState.bmdSwitcherStreamRTMPStateIdle:
                    this.OnIdle?.Invoke(this, error);
                    break;

                case _BMDSwitcherStreamRTMPState.bmdSwitcherStreamRTMPStateConnecting:
                    this.OnConnecting?.Invoke(this, error);
                    break;

                case _BMDSwitcherStreamRTMPState.bmdSwitcherStreamRTMPStateStreaming:
                    this.OnStreaming?.Invoke(this, error);
                    break;

                case _BMDSwitcherStreamRTMPState.bmdSwitcherStreamRTMPStateStopping:
                    this.OnStopping?.Invoke(this, error);
                    break;
            }

            return;
        }
        #endregion
    }
}
