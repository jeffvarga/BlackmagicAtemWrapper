//-----------------------------------------------------------------------------
// <copyright file="TransferMacro.cs">
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


namespace BlackmagicAtemWrapper.Macros
{
    using BMDSwitcherAPI;
    using System.Runtime.InteropServices;

    /// <summary>
    /// The <see cref="TransferMacro"/> class provides methods to cancel a macro transfer, monitor transfer progress, and retrieve transferred macro binary data.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 9.3.2</remarks>
    public class TransferMacro
    {
        /// <summary>
        /// Internal reference to the raw <see cref="IBMDSwitcherTransferMacro"/>
        /// </summary>
        private IBMDSwitcherTransferMacro InternalTransferMacroReference;

        /// <summary>
        /// Initializes an instance of the <see cref="TransferMacro"/> class.
        /// </summary>
        /// <param name="transferMacro">The native <see cref="IBMDSwitcherTransferMacro"/> from the BMDSwitcherAPI.</param>
        public TransferMacro(IBMDSwitcherTransferMacro transferMacro)
        {
            this.InternalTransferMacroReference = transferMacro;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="TransferMacro"/> class.
        /// </summary>
        ~TransferMacro()
        {
            _ = Marshal.ReleaseComObject(this.InternalTransferMacroReference);
        }

        #region IBMDSwitcherTransferMacro
        /// <summary>
        /// The GetProgress method gets the progress of the pending transfer.
        /// </summary>
        /// <returns>Transfer progress. Range is between 0.0 to 1.0.</returns>
        /// <remarks>Blackmagic Switcher SDK - 9.3.2.2</remarks>
        public double GetProgress()
        {
            this.InternalTransferMacroReference.GetProgress(out double progress);
            return progress;
        }

        /// <summary>
        /// The GetMacro method gets the transferred IBMDSwitcherMacro object.
        /// </summary>
        /// <returns>Pointer to an IBMDSwitcherMacro object.</returns>
        /// <remarks>Blackmagic Switcher SDK - 9.3.2.3</remarks>
        public Macro GetMacro()
        {
            this.InternalTransferMacroReference.GetMacro(out IBMDSwitcherMacro macro);
            return new(macro);
        }
        #endregion
    }
}
