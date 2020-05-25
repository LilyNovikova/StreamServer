using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamServer.Configuration;

namespace StreamServer.Video
{
    public class MJPEGVideo
    {
        private string filename;
        private string pathSource;
        private FileStream fsSource = null;

        public MJPEGVideo(string filename)
        {
            this.filename = filename;
            this.pathSource = ServerConfig.FilesDirectory + filename;
            try
            {
                //create file stream to read video file
                this.fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public byte[] GetNextFrame()
        {
            //create empty 5 byte array to store frame length
            var frame_length = new byte[5];

            //read current frame length
            fsSource.Read(frame_length, 0, 5);

            //transform frame_length to integer
            var length_string = Encoding.Default.GetString(frame_length);
            var length = int.Parse(length_string);

            //create bytearray to store the frame
            var newbytearray = new byte[length];
            fsSource.Read(newbytearray, 0, length);

            //return the frame
            return newbytearray;
        }

        public bool EndofFile()
        {
            //check if end of video
            return (fsSource.Position >= fsSource.Length - 1);
        }

        public void ResetVideo()
        {
            //reset video
            this.fsSource.Seek(0, SeekOrigin.Begin);
        }
    }
}
