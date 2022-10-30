using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Linq;

namespace FuncEvent
{
    public class ClassPcStatus
    {
        #region Field
        /// <summary> /// 
        /// CPU 카운터
        /// </summary>
        private PerformanceCounter cpuCounter;
        /// <summary> /// 메모리 카운터 /// </summary>
        private PerformanceCounter memoryCounter; /// <summary> /// 디스크 I/O 카운터 /// </summary> 

        private PerformanceCounter diskIOCounter;
        #endregion
        public ClassPcStatus()
        {
            this.cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            this.memoryCounter = new PerformanceCounter("Memory", "Available KBytes", true);
            string drive = AppDomain.CurrentDomain.BaseDirectory.Substring(0, 2).ToUpper();
            string[] instanceNameArray = new PerformanceCounterCategory("PhysicalDisk").GetInstanceNames();
            string instanceName = instanceNameArray.FirstOrDefault(s => s.IndexOf(drive) > -1);
            this.diskIOCounter = new PerformanceCounter("PhysicalDisk", "% Idle Time", instanceName, true);
        }
        public float GetCPURate()
        {
            float rate = this.cpuCounter.NextValue();
            rate = Math.Min(100f, Math.Max(0f, rate));
            return rate;
        }
        public float GetMemoryRate()
        {
            using (ManagementClass mc = new ManagementClass("Win32_OperatingSystem"))
            {
                using (ManagementObject o = mc.GetInstances().Cast<ManagementObject>().FirstOrDefault())
                {
                    float physicalMemorySize = float.Parse(o["TotalVisibleMemorySize"].ToString());
                    float rate = ((physicalMemorySize - this.memoryCounter.NextValue()) / physicalMemorySize) * 100;
                    rate = Math.Min(100f, Math.Max(0f, rate));
                    return rate;
                }
            }
        }

        public float GetDiskIORate()
        {
            float rate = 100 - this.diskIOCounter.NextValue();
            rate = Math.Min(100f, Math.Max(0f, rate));
            return rate;
        }
        void MemoryCheck()
        {
            try
            {
                int itotalMem = 0; // 총 메모리 KB 단위 
                int itotalMemMB = 0; // 총 메모리 MB 단위 
                int ifreeMem = 0; // 사용 가능 메모리 KB 단위 
                int ifreeMemMB = 0; // 사용 가능 메모리 MB 단위
                ManagementClass cls = new ManagementClass("Win32_OperatingSystem");
                ManagementObjectCollection moc = cls.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    itotalMem = int.Parse(mo["TotalVisibleMemorySize"].ToString());
                    ifreeMem = int.Parse(mo["FreePhysicalMemory"].ToString());
                }
                itotalMemMB = itotalMem / 1024; // 총 메모리 MB 단위 변경 
                ifreeMemMB = ifreeMem / 1024; // 사용 가능 메모리 MB 단위 변경 //Progressbar Max Setting... 

                //pbUse.Maximum = pbFree.Maximum = pbTotal.Maximum = itotalMemMB;

                //int tomb = pbTotal.Value = itotalMemMB;
                //int frmb = pbFree.Value = ifreeMemMB;
                //int cumb = pbUse.Value = (itotalMemMB - ifreeMemMB);
                //lblTotal.Text = string.Format($"Total Memory: {itotalMemMB}");
                //lblFree.Text = string.Format($"Free Memory: {ifreeMemMB}");
                //lblUse.Text = string.Format($"Current Memory: {itotalMemMB - ifreeMemMB}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }


}
