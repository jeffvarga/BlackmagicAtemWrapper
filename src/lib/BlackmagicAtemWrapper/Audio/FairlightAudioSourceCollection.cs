//-----------------------------------------------------------------------------
// <copyright file="FairlightAudioSourceCollection.cs">
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
    /// The MultiViewCollection class is used to iterate over <see cref="FairlightAudioSource"/> instances.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 7.5.6</remarks>
    public class FairlightAudioSourceCollection : IEnumerable<FairlightAudioSource>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherFairlightAudioSourceIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherFairlightAudioSourceIterator InternalFairlightAudioSourceIteratorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioSourceCollection"/> class.
        /// </summary>
        /// <param name="audioSourceIterator">The native <seealso cref="IBMDSwitcherFairlightAudioSourceIterator"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioSourceCollection(IBMDSwitcherFairlightAudioSourceIterator audioSourceIterator)
        {
            this.InternalFairlightAudioSourceIteratorReference = audioSourceIterator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FairlightAudioSourceCollection"/> class from a <seealso cref="IBMDSwitcherFairlightAudioInput"/>.
        /// </summary>
        /// <param name="audioInput">The native <seealso cref="IBMDSwitcherFairlightAudioInput"/> from the BMDSwitcherAPI.</param>
        public FairlightAudioSourceCollection(IBMDSwitcherFairlightAudioInput audioInput)
        {
            if (null == audioInput)
            {
                throw new ArgumentNullException(nameof(audioInput));
            }

            audioInput.CreateIterator(typeof(IBMDSwitcherFairlightAudioSourceIterator).GUID, out IntPtr audioSourceIteratorPtr);
            this.InternalFairlightAudioSourceIteratorReference = Marshal.GetObjectForIUnknown(audioSourceIteratorPtr) as IBMDSwitcherFairlightAudioSourceIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<FairlightAudioSource> GetEnumerator()
        {
            while (true)
            {
                this.InternalFairlightAudioSourceIteratorReference.Next(out IBMDSwitcherFairlightAudioSource audioSource);

                if (audioSource != null)
                {
                    yield return new FairlightAudioSource(audioSource);
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
        /// The GetById method returns the <see cref="FairlightAudioSource"/> object interface corresponding to the specified Id.
        /// </summary>
        /// <param name="inputId">BMDSwitcherFairlightAudioSourceId of input.</param>
        /// <returns>A <see cref="FairlightAudioSource"/> for the specified Id. </returns>
        /// <exception cref="ArgumentException">The <paramref name="inputId"/> parameter is invalid.</exception>
        public FairlightAudioSource GetById(long inputId)
        {
            this.InternalFairlightAudioSourceIteratorReference.GetById(inputId, out IBMDSwitcherFairlightAudioSource input);
            return new FairlightAudioSource(input);
        }
    }
}
