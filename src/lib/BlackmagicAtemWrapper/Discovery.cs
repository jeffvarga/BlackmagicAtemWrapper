//-----------------------------------------------------------------------------
// <copyright file="Discovery.cs">
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
    using BMDSwitcherAPI;

    /// <summary>
    /// The Discovery class is used to connect to a physical switcher device.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.1</remarks>
    public class Discovery
    {
        /// <summary>
        /// The ConnectTo method connects to the specified switcher and returns an <see cref="Switcher"/> object for the switcher.
        /// </summary>
        /// <remarks>
        /// <para>ConnectTo performs a synchronous network connection. This may take several seconds depending upon hostname resolution and network response times.</para>
        /// <para>If a network connection cannot be established, ConnectTo will attempt to connect via USB if the switcher supports it</para>
        /// </remarks>
        /// <param name="deviceAddress">Network hostname or IP address of switcher to connect to. Set this empty to only connect via USB.</param>
        /// <returns><see cref="Switcher"/> object for the connected switcher.</returns>
        /// <exception cref="Exception">Reason for connection failure as a BMDSwitcherConnectToFailure value.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.1.1</remarks>
        public static Switcher ConnectTo(string deviceAddress)
        {
            IBMDSwitcherDiscovery discovery = new CBMDSwitcherDiscovery();
            discovery.ConnectTo(deviceAddress, out IBMDSwitcher switcherDevice, out _BMDSwitcherConnectToFailure failReason);

            if (null == switcherDevice)
            {
                throw new System.Exception(failReason.ToString());
            }

            return new Switcher(switcherDevice);
        }
    }
}
