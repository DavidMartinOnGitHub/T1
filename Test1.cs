using System;

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

        [DllImport("ftd2xx.dll")]
        public static extern FT_STATUS FT_SetVIDPID(UInt32 vid, UInt32 pid);

        [DllImport("ftd2xx.dll")]
        public static extern FT_STATUS FT_ListDevices(ref UInt32 arg1, ref UInt32 arg2, UInt32 dwFlags);





    }
}

