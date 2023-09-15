//-----------------------------------------------------------------------------
// <copyright file="SuperSourceBoxCollection.cs">
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

namespace BlackmagicAtemWrapper.SuperSource
{
    using BMDSwitcherAPI;
    using System.Collections;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The SuperSourceBoxCollection class is used to iterate over <see cref="SuperSourceBox"/> objects.
    /// </summary>
    /// <remarks>Wraps Blackmagic Switcher SDK - 6.2.3</remarks>
    public class SuperSourceBoxCollection : IEnumerable<SuperSourceBox>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherInputIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherSuperSourceBoxIterator InternalSuperSourceBoxIteratorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="SuperSourceBoxCollection"/> class.
        /// </summary>
        /// <param name="superSourceBoxIterator">The native <seealso cref="IBMDSwitcherSuperSourceBoxIterator"/> from the BMDSwitcherAPI.</param>
        public SuperSourceBoxCollection(IBMDSwitcherSuperSourceBoxIterator superSourceBoxIterator)
        {
            this.InternalSuperSourceBoxIteratorReference = superSourceBoxIterator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuperSourceBoxCollection"/> class from a <seealso cref="IBMDSwitcherInputSuperSource"/>.
        /// </summary>
        /// <param name="inputSuperSource">The native <seealso cref="IBMDSwitcher"/> from the BMDSwitcherAPI.</param>
        public SuperSourceBoxCollection(IBMDSwitcherInputSuperSource inputSuperSource)
        {
            if (null == inputSuperSource)
            {
                throw new ArgumentNullException(nameof(inputSuperSource));
            }

            inputSuperSource.CreateIterator(typeof(IBMDSwitcherSuperSourceBoxIterator).GUID, out IntPtr superSourceBoxIteratorPtr);
            this.InternalSuperSourceBoxIteratorReference = Marshal.GetObjectForIUnknown(superSourceBoxIteratorPtr) as IBMDSwitcherSuperSourceBoxIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<SuperSourceBox> GetEnumerator()
        {
            while (true)
            {
                this.InternalSuperSourceBoxIteratorReference.Next(out IBMDSwitcherSuperSourceBox superSourceBox);

                if (superSourceBox != null)
                {
                    yield return new SuperSourceBox(superSourceBox);
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
    }
}
