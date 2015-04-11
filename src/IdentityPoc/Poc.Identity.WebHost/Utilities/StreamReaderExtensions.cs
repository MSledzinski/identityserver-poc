namespace Poc.Identity.WebHost.Utilities
{
    using System.IO;

    public static class StreamReaderExtensions
    {
        public static byte[] ToArray(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var memStream = new MemoryStream())
            {
                int bytesRed;
                while ((bytesRed = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memStream.Write(buffer, 0, bytesRed);
                }

                return memStream.ToArray();
            }
        }
    }
}