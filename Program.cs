using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FTD2XX_NET;

namespace Direct
{
  class Program
  {
    static void Main(string[] args)
    {
      RequestMicroNIRFirmware();

      Console.WriteLine("DONE");

      // Console.WriteLine("Press any key to continue...");
      // Console.ReadKey();
    }

    //---------------------------

        private static bool RequestMicroNIRFirmware()
    {
      string serialNumber = "N1-00152";

      Console.WriteLine("Serial Number = {0}", serialNumber);

      FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

            Console.WriteLine(">>NEW");
            FTD2XX_NET.FTDI ftdi = new FTD2XX_NET.FTDI();
            Console.WriteLine("<<NEW");

      #if LINUXBUILD
            Console.WriteLine(">>SetVIDPID"");      
            ftStatus = ftdi.SetVIDPID(0x0403, 0x7cb0);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                string msg = string.Format("SetVIDPID failed with '{0}'.", ftStatus.ToString());
                throw new Exception(msg);
            }
            Console.WriteLine("<<SetVIDPID"");      
#endif

            Console.WriteLine(">>OpenBySerialNumber");

            ftStatus = ftdi.OpenBySerialNumber(serialNumber);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                string msg = string.Format("OpenBySerialNumber failed with '{0}' on Serial Number '{1}'", ftStatus.ToString(), serialNumber);
                throw new Exception(msg);
            }
            Console.WriteLine("<<OpenBySerialNumber");

            ftdi.ResetDevice();
            ftdi.SetTimeouts(200, 200);


            string command = "V\r"; // firmware
            int dwell = 50;

            Console.WriteLine(">>Purge");
            ftdi.Purge(FTD2XX_NET.FTDI.FT_PURGE.FT_PURGE_RX);
            Console.WriteLine("<<Purge");

            Console.WriteLine(">>Write");
            uint numBytesWritten = 255;
            byte[] cmd = System.Text.Encoding.ASCII.GetBytes(command);
            ftStatus = ftdi.Write(cmd, cmd.Length, ref numBytesWritten);
            Console.WriteLine("<<Write");

            System.Threading.Thread.Sleep(dwell);

            Console.WriteLine(">>Read");
            uint numBytesRead = 0;
            byte[] rxbuff = new byte[512];
            ftStatus = ftdi.Read(rxbuff, 512, ref numBytesRead);
            Console.WriteLine("<<Read");

            string response = ConvertResponseBytesToString(rxbuff);
            Console.WriteLine("RESPONSE = '{0}'", response);

            if (ftdi != null)
            {
                ftdi.Close();
                ftdi = null;
            }

            return true;
        }

        private static string ConvertResponseBytesToString(byte[] bytes)
        {
            int idx = -1;
            for (idx = 0; idx < bytes.Length; idx++)
            {
                byte ch = bytes[idx];

                if (ch == 13 || ch == 0 || ch == 10) break; // break on CR, NULL or LF
            }

            string response = System.Text.Encoding.ASCII.GetString(bytes, 0, idx);
            return response;
        }
        //---------------------------


    }
}
