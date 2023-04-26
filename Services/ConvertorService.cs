using FFMpegCore;
using FFMpegCore.Enums;

namespace VideoToMp3.Mp3ConvertorService;


    class Convertor
    {


        public Convertor()
        {
            GlobalFFOptions.
               Configure(options => options.BinaryFolder = "C:\\ffmpeg");
        }

        public bool ExtractAudio(string userUploadDir, string specificFilePath)
        {
            bool ExtrationStatus = FFMpeg.ExtractAudio(userUploadDir, specificFilePath);
                        
            if (ExtrationStatus)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

       


    }

