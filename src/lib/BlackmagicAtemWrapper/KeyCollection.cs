//-----------------------------------------------------------------------------
// <copyright file="InputCollection.cs">
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
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    public class KeyCollection : IEnumerable<Key>
    {
        private readonly IBMDSwitcherKeyIterator InternalSwitcherKeyIteratorReference;

        public KeyCollection(IBMDSwitcherKeyIterator ski)
        {
            this.InternalSwitcherKeyIteratorReference = ski;
        }

        public KeyCollection(IBMDSwitcherMixEffectBlock meb)
        {
            meb.CreateIterator(typeof(IBMDSwitcherKeyIterator).GUID, out IntPtr mebIteratorPtr);
            this.InternalSwitcherKeyIteratorReference = Marshal.GetObjectForIUnknown(mebIteratorPtr) as IBMDSwitcherKeyIterator;

            return;
        }

        #region IEnumerable
        public IEnumerator<Key> GetEnumerator()
        {
            while (true)
            {
                this.InternalSwitcherKeyIteratorReference.Next(out IBMDSwitcherKey input);

                if (input != null)
                {
                    yield return new Key(input);
                }
                else
                {
                    yield break;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
