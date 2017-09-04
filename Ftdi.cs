using System;
using System.Runtime.InteropServices;

namespace Direct
{
	public class Ftdi
    {
		public Ftdi()
        {

        }

        /// <summary>
        /// Enumaration containing the varios return status for the DLL functions.
        /// </summary>
        public enum FT_STATUS
        {
            FT_OK = 0,
            FT_INVALID_HANDLE,
            FT_DEVICE_NOT_FOUND,
            FT_DEVICE_NOT_OPENED,
            FT_IO_ERROR,
            FT_INSUFFICIENT_RESOURCES,
            FT_INVALID_PARAMETER,
            FT_INVALID_BAUD_RATE,
            FT_DEVICE_NOT_OPENED_FOR_ERASE,
            FT_DEVICE_NOT_OPENED_FOR_WRITE,
            FT_FAILED_TO_WRITE_DEVICE,
            FT_EEPROM_READ_FAILED,
            FT_EEPROM_WRITE_FAILED,
            FT_EEPROM_ERASE_FAILED,
            FT_EEPROM_NOT_PRESENT,
            FT_EEPROM_NOT_PROGRAMMED,
            FT_INVALID_ARGS,
            FT_OTHER_ERROR
        };

#if LINUXBUILD
        [DllImport("ftd2xx.dll")]
        private static extern FT_STATUS FT_SetVIDPID(UInt32 vid, UInt32 pid);
#endif

        [DllImport("ftd2xx.dll")]
        private static extern FT_STATUS FT_ListDevices(ref UInt32 arg1, ref UInt32 arg2, UInt32 dwFlags);


		public void Initialize()
        {
            Console.WriteLine(">>Initialize");
#if LINUXBUILD
            FT_STATUS ftstatus = FT_SetVIDPID(0x0403, 0x7cb0);
			Console.WriteLine("FT_STATUS = {0}", ftstatus.ToString());
#endif
            Console.WriteLine("<<Initialize");
        }

		public void ListDevices()
        {
            Console.WriteLine(">>ListDevices");

            UInt32 arg1 = 99;
            UInt32 arg2 = 98;
            UInt32 flags = 0x80000000;


            FT_STATUS ftstatus = FT_ListDevices(ref arg1, ref arg2, flags);
//			if(ftstatus == FT_STATUS.FT_OK)
            Console.WriteLine("FT_STATUS = {0}", ftstatus.ToString());
            Console.WriteLine("arg1 = {0}, arg2 = {1}", arg1, arg2);

            Console.WriteLine("<<ListDevices");

        }
    }
}

