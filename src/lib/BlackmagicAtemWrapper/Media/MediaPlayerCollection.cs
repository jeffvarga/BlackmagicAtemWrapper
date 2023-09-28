//-----------------------------------------------------------------------------
// <copyright file="MediaPlayerCollection.cs">
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

namespace BlackmagicAtemWrapper.Media
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using BMDSwitcherAPI;

    /// <summary>
    /// The MediaPlayerCollection class is used to enumerate the available media players on the switcher.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 4.3.2</remarks>
    public class MediaPlayerCollection : IEnumerable<MediaPlayer>
    {
        /// <summary>
        /// Internal reference to the raw <seealso cref="IBMDSwitcherMediaPlayerIterator"/>.
        /// </summary>
        private readonly IBMDSwitcherMediaPlayerIterator InternalMediaPlayerIteratorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPlayerCollection"/> class.
        /// </summary>
        /// <param name="mediaPlayerIterator">The native <seealso cref="IBMDSwitcherMediaPlayerIterator"/> from the BMDSwitcherAPI.</param>
        public MediaPlayerCollection(IBMDSwitcherMediaPlayerIterator mediaPlayerIterator)
        {
            this.InternalMediaPlayerIteratorReference = mediaPlayerIterator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPlayerCollection"/> class from a <seealso cref="IBMDSwitcher"/>.
        /// </summary>
        /// <param name="switcher">The native <seealso cref="IBMDSwitcher"/> from the BMDSwitcherAPI.</param>
        public MediaPlayerCollection(IBMDSwitcher switcher)
        {
            if (null == switcher)
            {
                throw new ArgumentNullException(nameof(switcher));
            }

            switcher.CreateIterator(typeof(IBMDSwitcherMediaPlayerIterator).GUID, out IntPtr inputIteratorPtr);
            this.InternalMediaPlayerIteratorReference = Marshal.GetObjectForIUnknown(inputIteratorPtr) as IBMDSwitcherMediaPlayerIterator;

            return;
        }

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>Enumerator that iterates through the collection.</returns>
        public IEnumerator<MediaPlayer> GetEnumerator()
        {
            while (true)
            {
                this.InternalMediaPlayerIteratorReference.Next(out IBMDSwitcherMediaPlayer input);

                if (input != null)
                {
                    yield return new MediaPlayer(input);
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
