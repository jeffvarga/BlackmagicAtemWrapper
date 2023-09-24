//-----------------------------------------------------------------------------
// <copyright file="MacroPool.cs">
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
    /// The <see cref="MacroPool"/> class provides functionality for the transfer and deletion of macros and for accessing and modifying macro properties.
    /// </summary>
    /// <remarks>Blackmagic Switcher SDK - 9.3.1</remarks>
    public class MacroPool : IBMDSwitcherMacroPoolCallback
    {
        /// <summary>
        /// Internal reference to the raw <see cref="IBMDSwitcherMacroPool"/>
        /// </summary>
        private readonly IBMDSwitcherMacroPool InternalMacroPoolReference;

        /// <summary>
        /// Initializes an instance of the <see cref="MacroPool"/> class.
        /// </summary>
        /// <param name="macroPool">The native <see cref="IBMDSwitcherMacroPool"/> from the BMDSwitcherAPI.</param>
        public MacroPool(IBMDSwitcherMacroPool macroPool)
        {
            this.InternalMacroPoolReference = macroPool;
            this.InternalMacroPoolReference.AddCallback(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="MacroPool"/> class.
        /// </summary>
        ~MacroPool()
        {
            this.InternalMacroPoolReference.RemoveCallback(this);
            _ = Marshal.ReleaseComObject(this.InternalMacroPoolReference);
        }

        #region Events
        /// <summary>
        /// A delegate to handle events from <see cref="MacroPool"/>.
        /// </summary>
        /// <param name="sender">The <see cref="MacroPool"/> that received the event.</param>
        /// <param name="index">The index of the macro that has changed.</param>
        public delegate void MacroPoolEventHandler(object sender, uint index);

        /// <summary>
        /// A delegate to handle transfer update events from <see cref="MacroPool"/>.
        /// </summary>
        /// <param name="sender">The <see cref="MacroPool"/> that received the event.</param>
        /// <param name="index">The index of the macro that has changed.</param>
        /// <param name="macroTransfer">The <see cref="TransferMacro"/> object for the transfer.</param>
        public delegate void MacroPoolTransferEventHandler(object sender, uint index, TransferMacro macroTransfer);

        /// <summary>
        /// A macro has been created (becomes valid), or deleted (becomes invalid).
        /// </summary>
        public event MacroPoolEventHandler OnValidChanged;

        /// <summary>
        /// A macro's <see cref="HasUnsupportedOps"/> flag has changed.
        /// </summary>
        public event MacroPoolEventHandler OnHasUnsupportedOpsChanged;

        /// <summary>
        /// A macro's <see cref="GetName"/> has changed.
        /// </summary>
        public event MacroPoolEventHandler OnNameChanged;

        /// <summary>
        /// A macro's <see cref="GetDescription"/> has changed.
        /// </summary>
        public event MacroPoolEventHandler OnDescriptionChanged;

        /// <summary>
        /// A macro transfer has completed.
        /// </summary>
        public event MacroPoolTransferEventHandler OnTransferCompleted;

        /// <summary>
        /// A macro transfer has been cancelled.
        /// </summary>
        public event MacroPoolTransferEventHandler OnTransferCancelled;

        /// <summary>
        /// A macro transfer has failed.
        /// </summary>
        public event MacroPoolTransferEventHandler OnTransferFailed;
        #endregion

        #region IBMDSwitcherMacroPool
        /// <summary>
        /// The GetMaxCount method returns the number of macros that can be stored on the switcher.
        /// </summary>
        /// <returns>The maximum number of macros for the switcher.</returns>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.1</remarks>
        public uint GetMaxCount()
        {
            this.InternalMacroPoolReference.GetMaxCount(out uint maxCount);
            return maxCount;
        }

        /// <summary>
        /// The Delete method will delete (set invalid) an existing macro. If the macro is already invalid then this method has no effect.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <exception cref="ArgumentException">The index parameter is out of range.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.2</remarks>
        public void Delete(uint index)
        {
            this.InternalMacroPoolReference.Delete(index);
            return;
        }

        /// <summary>
        /// The IsValid method checks if a macro with the specified index exists.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <returns>Boolean value which is true if the macro is valid.</returns>
        /// <exception cref="ArgumentException">The index parameter is out of range.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.3</remarks>
        public bool IsValid(uint index)
        {
            this.InternalMacroPoolReference.IsValid(index, out int valid);
            return Convert.ToBoolean(valid);
        }

        /// <summary>
        /// The HasUnsupportedOps method indicates whether a macro contains unsupported operations. A macro with unsupported operations can still be played but the unsupported operations will be ignored.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <returns>Boolean value which is true if the macro contains unsupported operations.</returns>
        /// <exception cref="ArgumentException">The index parameter is out of range.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.4</remarks>
        public bool HasUnsupportedOps(uint index)
        {
            this.InternalMacroPoolReference.HasUnsupportedOps(index, out int hasUnsupportedOps);
            return Convert.ToBoolean(hasUnsupportedOps);
        }

        /// <summary>
        /// The GetName method gets the name of a macro.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <returns>Macro name.</returns>
        /// <exception cref="ArgumentException">The index parameter is out of range.</exception>
        /// <exception cref="OutOfMemoryException">Insufficient memory to get the name.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.5</remarks>
        public string GetName(uint index)
        {
            this.InternalMacroPoolReference.GetName(index, out string name);
            return name;
        }

        /// <summary>
        /// The SetName method sets the name of a macro.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <param name="name">Macro name.</param>
        /// <exception cref="ArgumentException">The index parameter is out of range.</exception>
        /// <exception cref="OutOfMemoryException">Insufficient memory to get the name.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.6</remarks>
        public void SetName(uint index, string name)
        {
            this.InternalMacroPoolReference.SetName(index, name);
            return;
        }

        /// <summary>
        /// The GetDescription method gets the description of a macro.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <returns>Macro description.</returns>
        /// <exception cref="ArgumentException">The index parameter is out of range.</exception>
        /// <exception cref="OutOfMemoryException">Insufficient memory to get the description.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.7</remarks>
        public string GetDescription(uint index)
        {
            this.InternalMacroPoolReference.GetDescription(index, out string description);
            return description;
        }

        /// <summary>
        /// The SetDescription method sets the description of a macro.
        /// </summary>
        /// <param name="index">Macro index.</param>
        /// <param name="description">Macro description.</param>
        /// <exception cref="ArgumentException">The index parameter is out of range.</exception>
        /// <exception cref="OutOfMemoryException">Insufficient memory to set the description.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.8</remarks>
        public void SetDescription(uint index, string description)
        {
            this.InternalMacroPoolReference.SetDescription(index, description);
            return;
        }

        /// <summary>
        /// The CreateMacro method creates an <see cref="Macro"/> object. IBMDSwitcherMacro objects are only used for transfers.
        /// </summary>
        /// <param name="sizeBytes">The size of the macro, in bytes.</param>
        /// <returns>The <see cref="Macro"/> object.</returns>
        /// <exception cref="OutOfMemoryException">Insufficient memory to create a macro.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.9</remarks>
        public Macro CreateMacro(uint sizeBytes)
        {
            this.InternalMacroPoolReference.CreateMacro(sizeBytes, out IBMDSwitcherMacro macro);
            return new(macro);
        }

        /// <summary>
        /// The Upload method transfers a macro to the switcher. No more than one transfer can occur at a time.
        /// </summary>
        /// <param name="index">Destination macro index.</param>
        /// <param name="name">Destination macro name.</param>
        /// <param name="description">Destination macro description.</param>
        /// <param name="macro">IBMDSwitcherMacro object containing the macro binary data for the transfer.</param>
        /// <returns><see cref="TransferMacro"/> object for monitoring the progress of the transfer.</returns>
        /// <exception cref="ArgumentException">The index parameter is out of range.</exception>
        /// <exception cref="OutOfMemoryException">Insufficient memory to perform a transfer.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.10</remarks>
        public TransferMacro Upload(uint index, string name, string description, IBMDSwitcherMacro macro)
        {
            this.InternalMacroPoolReference.Upload(index, name, description, macro, out IBMDSwitcherTransferMacro macroTransfer);
            return new TransferMacro(macroTransfer);
        }

        /// <summary>
        /// The Download method transfers a macro from the switcher. No more than one transfer can occur at a time.
        /// </summary>
        /// <param name="index">Destination macro index.</param>
        /// <returns>IBMDSwitcherMacroTransfer object for monitoring the progress of the transfer and retrieving the macro binary data.</returns>
        /// <exception cref="OutOfMemoryException">Insufficient memory to perform a transfer.</exception>
        /// <remarks>Blackmagic Switcher SDK - 9.3.1.11</remarks>
        public TransferMacro Download(uint index)
        {
            this.InternalMacroPoolReference.Download(index, out IBMDSwitcherTransferMacro macro);
            return new(macro);
        }
        #endregion

        #region IBMDSwitcherMacroPoolCallback
        /// <summary>
        /// <para>The Notify method is called when an IBMDSwitcherMacroPool event occurs, such as macro property changes.</para>
        /// <para>This method is called from a separate thread created by the switcher SDK so care should be exercised when interacting with other threads. Callbacks should be processed as quickly as possible to avoid delaying other callbacks or affecting the connection to the switcher.</para>
        /// </summary>
        /// <param name="eventType">BMDSwitcherMacroPoolEventType that describes the type of event that has occurred.</param>
        /// <param name="index">Index of the macro that has changed.</param>
        /// <param name="macroTransfer">If the event type is one of bmdSwitcherMacroPoolEventTypeTransferCompleted, bmdSwitcherMacroPoolEventTypeTransferCancelled, or bmdSwitcherMacroPoolEventTypeTransferFailed then this parameter is a pointer to the affected IBMDSwitcherTransferMacro interface object.</param>
        /// <remarks>Blackmagic Switcher SDK - 9.3.4.1</remarks>
        void IBMDSwitcherMacroPoolCallback.Notify(_BMDSwitcherMacroPoolEventType eventType, uint index, IBMDSwitcherTransferMacro macroTransfer)
        {
            switch (eventType)
            {
                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeValidChanged:
                    this.OnValidChanged?.Invoke(this, index);
                    break;

                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeHasUnsupportedOpsChanged:
                    this.OnHasUnsupportedOpsChanged?.Invoke(this, index);
                    break;

                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeNameChanged:
                    this.OnNameChanged?.Invoke(this, index);
                    break;

                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeDescriptionChanged:
                    this.OnDescriptionChanged?.Invoke(this, index);
                    break;

                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeTransferCompleted:
                    this.OnTransferCompleted?.Invoke(this, index, new TransferMacro(macroTransfer));
                    break;

                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeTransferCancelled:
                    this.OnTransferCancelled?.Invoke(this, index, new TransferMacro(macroTransfer));
                    break;

                case _BMDSwitcherMacroPoolEventType.bmdSwitcherMacroPoolEventTypeTransferFailed:
                    this.OnTransferFailed?.Invoke(this, index, new TransferMacro(macroTransfer));
                    break;
            }
            return;
        }
        #endregion
    }
}
