//-----------------------------------------------------------------------------
// <copyright file="Identity.cs">
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

namespace BlackmagicAtemWrapper.device
{
    using System;
    using System.Runtime.InteropServices;
    using BlackmagicAtemWrapper;
    using BlackmagicAtemWrapper.utility;
    using BMDSwitcherAPI;

    /// <summary>
    /// The Identity class is used for managing the individual identity of the switcher.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 14.2.1</remarks>
    public class Identity : IBMDSwitcherIdentityInformationCallback
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherIdentityInformation"/>.
        /// </summary>
        private readonly IBMDSwitcherIdentityInformation InternalIdentityInformationReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        /// <param name="identityObject">The native <seealso cref="IBMDSwitcherIdentityInformation"/> from the BMDSwitcherAPI.</param>
        public Identity(IBMDSwitcherIdentityInformation identityObject)
        {
            InternalIdentityInformationReference = identityObject ?? throw new ArgumentNullException(nameof(identityObject));
            InternalIdentityInformationReference.AddCallback(this);
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        /// <param name="switcher">The <see cref="Switcher"/> instance from which to derive the IdentityInformation.</param>
        /// <exception cref="ArgumentNullException">Caller passed a null <see cref="Switcher"/>.</exception>
        /// <exception cref="NotSupportedException">Unable to get a reference to <seealso cref="IBMDSwitcherIdentityInformation"/>.  Likely because switcher or ATEM software is not version 9.0 or higher.</exception>
        internal Identity(Switcher switcher)
        {
            if (null == switcher) { throw new ArgumentNullException(nameof(switcher)); }

            InternalIdentityInformationReference = switcher.InternalSwitcherReference as IBMDSwitcherIdentityInformation;

            if (InternalIdentityInformationReference == null)
            {
                throw new NotSupportedException("Requires at least ATEM Switchers 9.0 software and firmware.");
            }

            InternalIdentityInformationReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Identity"/> class.
        /// </summary>
        ~Identity()
        {
            InternalIdentityInformationReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(InternalIdentityInformationReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="Identity"/>.
        /// </summary>
        /// <param name="sender">The <see cref="Identity"/> that received the event.</param>
        public delegate void IdentityEventHandler(object sender);

        /// <summary>
        /// The identity information for the switcher has changed.
        /// </summary>
        public event IdentityEventHandler OnFieldsChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Get the unique ID of the switcher.
        /// </summary>
        public string UniqueId
        {
            get { return GetUniqueId(); }
        }

        /// <summary>
        /// Get the IP address of the switcher.
        /// </summary>
        public string IpAddress
        {
            get { return GetIpAddress(); }
        }

        /// <summary>
        /// Get the MDNS name of the switcher.
        /// </summary>
        public string MdnsName
        {
            get { return GetMdnsName(); }
        }

        /// <summary>
        /// Get the device name of the switcher.
        /// </summary>
        public string DeviceName
        {
            get { return GetDeviceName(); }
        }
        #endregion

        #region IBMDSwitcherIdentityInformation
        /// <summary>
        /// The GetUniqueId method returns an ID that can be used to uniquely identify the switcher.
        /// </summary>
        /// <returns>Unique ID of the switcher.</returns>
        /// <exception cref="FailedException">Failure</exception>
        /// <remarks>Blackmagic Switcher SDK - 14.2.1.1</remarks>
        public string GetUniqueId()
        {
            try
            {
                InternalIdentityInformationReference.GetUniqueId(out string uniqueId);
                return uniqueId;
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
        /// The GetIpAddress method returns the current IP address of the switcher.
        /// </summary>
        /// <returns>Local IP address of the switcher.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 14.2.1.2</remarks>
        public string GetIpAddress()
        {
            try
            {
                InternalIdentityInformationReference.GetIpAddress(out string ipAddress);
                return ipAddress;
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
        /// The GetMdnsName method returns the current MDNS (Bonjour) name of the switcher. This property is generated based on the name configured in ATEM Setup.
        /// </summary>
        /// <returns>Local hostname of the switcher.</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 14.2.1.3</remarks>
        public string GetMdnsName()
        {
            try
            {
                InternalIdentityInformationReference.GetMdnsName(out string mdnsName);
                return mdnsName;
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
        /// The GetDeviceName method returns the model name of the switcher.
        /// </summary>
        /// <returns>Device name of the switcher</returns>
        /// <exception cref="FailedException">Failure.</exception>
        /// <remarks>Blackmagic Switcher SDK - 14.2.1.4</remarks>
        public string GetDeviceName()
        {
            try
            {
                InternalIdentityInformationReference.GetDeviceName(out string deviceName);
                return deviceName;
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

        #region IBMDSwitcherIdentityInformationCallback
        /// <summary>
        /// <para>The Notify method is called when IBMDSwitcherIdentityInformation events occur, such as property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// <para>The return value (required by COM) is ignored by the caller.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherIdentityInformationEventType that describes the type of event that has occurred.</param>
        /// <remarks>Blackmagic Switcher SDK - 14.2.2.1</remarks>
        void IBMDSwitcherIdentityInformationCallback.Notify(_BMDSwitcherIdentityInformationEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherIdentityInformationEventType.bmdSwitcherIdentityInformationEventTypeFieldsChanged:
                    OnFieldsChanged?.Invoke(this);
                    break;
            }

            return;
        }
        #endregion
    }
}
