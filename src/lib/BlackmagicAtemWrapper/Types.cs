using System;
using System.Runtime.InteropServices;

namespace BlackmagicAtemWrapper
{
    public class FailedException : Exception
    {
        public FailedException(Exception e) : base("E_FAIL", e) { }

        public static bool IsFailedException(int hr)
        {
            return (uint)hr == 0x80004005;
        }
    }

    public class Timecode
    {
        public Timecode(byte hours, byte minutes, byte seconds, byte frames, bool dropFrame = false)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
            this.Frames = frames;
            this.DropFrame = dropFrame;
        }

        public Timecode() : this(0, 0, 0, 0) { }

        public override string ToString()
        {
            return string.Format("{1:00}{0}{2:00}{0}{3:00}{0}{4:00}", this.DropFrame ? ";" : ":", Hours, Minutes, Seconds, Frames);
        }

        public byte Hours { get; set; }
        public byte Minutes { get; set; }
        public byte Seconds { get; set; }
        public byte Frames { get; set; }
        public bool DropFrame { get; }
    }
}