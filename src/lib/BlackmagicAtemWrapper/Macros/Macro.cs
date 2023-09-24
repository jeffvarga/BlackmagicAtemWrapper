//-----------------------------------------------------------------------------
// <copyright file="Macro.cs">
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
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// <para>The IBMDSwitcherMacro object interface provides access to macro binary data used for transferring macros.</para>
    /// <para>This interface does not provide access to macro properties or control to record or playback a macro.</para>
    /// <para>To access properties use the IBMDSwitcherMacroPool interface. To record or playback a macro use the IBMDSwitcherMacroControl interface.</para>
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 9.3.3</remarks>
    public class Macro
    {
        /// <summary>
        /// Internal reference to the raw <see cref="IBMDSwitcherMacro"/>
        /// </summary>
        private readonly IBMDSwitcherMacro InternalMacroReference;

        /// <summary>
        /// Initializes an instance of the <see cref="Macro"/> class.
        /// </summary>
        /// <param name="macro">The native <see cref="IBMDSwitcherMacro"/> from the BMDSwitcherAPI.</param>
        public Macro(IBMDSwitcherMacro macro)
        {
            this.InternalMacroReference = macro;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Macro"/> class.
        /// </summary>
        ~Macro()
        {
            _ = Marshal.ReleaseComObject(this.InternalMacroReference);
        }

        /// <summary>
        /// The GetSize method returns the size (in bytes) of the macro binary data.
        /// </summary>
        /// <returns>Size (in bytes) of the macro binary data.</returns>
        /// <remarks>Blackmagic Switcher SDK - 9.3.3.1</remarks>
        public int GetSize()
        {
            return this.InternalMacroReference.GetSize();
        }

        /// <summary>
        /// The GetBytes method returns a pointer to the macro binary data buffer.
        /// </summary>
        /// <returns>Pointer to the macro binary data.</returns>
        /// <remarks>Blackmagic Switcher SDK - 9.3.3.2</remarks>
        public byte[] GetBytes()
        {
            byte[] toReturn = new byte[this.GetSize()];
            this.InternalMacroReference.GetBytes(out IntPtr buffer);

            Marshal.Copy(buffer, toReturn, 0, toReturn.Length);
            return toReturn;
        }
    }
}
