//-----------------------------------------------------------------------------
// <copyright file="FairlightAudioInputCollection.cs">
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

namespace BlackmagicAtemWrapper.Audio
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    /// The FairlightAudioInputCollection class is used to enumerate the available audio inputs for the Fairlight audio mixer.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 7.5.3</remarks>
    public class FairlightAudioInputCollection : IEnumerable<FairlightAudioInput>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherFairlightAudioInputIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherFairlightAudioInputIterator InternalFairlightAudioInputIteratorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioInputCollection"/> class.
        /// </summary>
        /// <param name="inputIterator">The native <seealso cref="IBMDSwitcherFairlightAudioInputIterator"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioInputCollection(IBMDSwitcherFairlightAudioInputIterator inputIterator)
        {
            this.InternalFairlightAudioInputIteratorReference = inputIterator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioInputCollection"/> class from a <seealso cref="IBMDSwitcherFairlightAudioMixer"/>.
        /// </summary>
        /// <param name="audioMixer">The native <seealso cref="IBMDSwitcherFairlightAudioMixer"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioInputCollection(IBMDSwitcherFairlightAudioMixer audioMixer)
        {
            if (null == audioMixer)
            {
                throw new ArgumentNullException(nameof(audioMixer));
            }

            audioMixer.CreateIterator(typeof(IBMDSwitcherFairlightAudioInputIterator).GUID, out IntPtr inputIteratorPtr);
            this.InternalFairlightAudioInputIteratorReference = Marshal.GetObjectForIUnknown(inputIteratorPtr) as IBMDSwitcherFairlightAudioInputIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<FairlightAudioInput> GetEnumerator()
        {
            while (true)
            {
                this.InternalFairlightAudioInputIteratorReference.Next(out IBMDSwitcherFairlightAudioInput input);

                if (input != null)
                {
                    yield return new FairlightAudioInput(input);
                }
                else
                {
                    yield break;
                }
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion

        /// <summary>
        /// The GetById method returns the <see cref="IBMDSwitcherFairlightAudioInput"/> object interface corresponding to the specified Id.
        /// </summary>
        /// <param name="audioInputId">BMDSwitcherAudioInputId of input.</param>
        /// <returns>A <see cref="IBMDSwitcherFairlightAudioInput"/> for the specified Id. </returns>
        /// <exception cref="ArgumentException">The <paramref name="audioInputId"/> parameter is invalid.</exception>
        public FairlightAudioInput GetById(long audioInputId)
        {
            this.InternalFairlightAudioInputIteratorReference.GetById(audioInputId, out IBMDSwitcherFairlightAudioInput audioInput);
            return new FairlightAudioInput(audioInput);
        }
    }
}
