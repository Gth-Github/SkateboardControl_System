using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SkateboardControl_System
{
    //DbgHelper,数据采集的辅助程序
    class PerformanceCounter
    {
        public long StartTime;  // start time, in cpu ticks
        public long StopTime;   // stop time, in cpu ticks
        public long CPUFreq;    // CPU frequency, ticks per second

        public PerformanceCounter()
        {
            if (QueryPerformanceFrequency(out CPUFreq) == false)
            {
                throw new Win32Exception(); // high-performance counter not supported
            }
        }
        public void Start()
        {
            QueryPerformanceCounter(out StartTime);
        }
        public void Stop()
        {
            QueryPerformanceCounter(out StopTime);
        }
        public double Duration      // Returns the duration of the timer (in seconds)
        {
            get { return (double)(StopTime - StartTime) / (double)CPUFreq; }
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(out long freq);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceCounter(out long count);
    }
}
