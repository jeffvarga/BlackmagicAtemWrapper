//-----------------------------------------------------------------------------
// <copyright file="SaveRecall.cs">
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
    using BMDSwitcherAPI;

    /// <summary>
    /// The SaveRecall class provides functionality for storing and clearing operating states.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 2.3.23</remarks>
    public class SaveRecall
    {
        /// <summary>
        /// The MixMinusOutput object is used for storing and clearing operating states.
        /// </summary>
        /// <remarks>Blackmagic Switcher SDK - 2.3.23</remarks>
        private readonly IBMDSwitcherSaveRecall InternalSaveRecallReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveRecall"/> class.
        /// </summary>
        /// <param name="saveRecall">The native <seealso cref="IBMDSwitcherSaveRecall"/> from the BMDSwitcherAPI.</param>
        public SaveRecall(IBMDSwitcherSaveRecall saveRecall)
        {
            this.InternalSaveRecallReference = saveRecall;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SaveRecall"/> class.
        /// </summary>
        ~SaveRecall()
        {
            _ = Marshal.ReleaseComObject(this.InternalSaveRecallReference);
        }

        #region IBMDSwitcherSaveRecall
        /// <summary>
        /// The Save method stores the current operating state to the switcher’s peristent memory.
        /// </summary>
        /// <param name="type">The type of operating state to store.</param>
        /// <exception cref="ArgumentException">The type parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.23.1</remarks>
        public void Save(_BMDSwitcherSaveRecallType type = _BMDSwitcherSaveRecallType.bmdSwitcherSaveRecallTypeStartupState)
        {
            this.InternalSaveRecallReference.Save(type);
            return;
        }

        /// <summary>
        /// The Clear method clears a stored operating state from the switcher’s persistent memory.
        /// </summary>
        /// <param name="type">The type of operating state to clear.</param>
        /// <exception cref="ArgumentException">The type parameter is invalid.</exception>
        /// <remarks>Blackmagic Switcher SDK - 2.3.23.2</remarks>
        public void Clear(_BMDSwitcherSaveRecallType type = _BMDSwitcherSaveRecallType.bmdSwitcherSaveRecallTypeStartupState)
        {
            this.InternalSaveRecallReference.Clear(type);
            return;
        }
        #endregion
    }
}
