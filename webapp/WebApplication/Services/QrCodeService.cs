using System.IO;
using System.Net;

namespace K9.WebApplication.Services
{
    public class QrCodeService : IQrCodeService
    {

        public System.Drawing.Image GetQrCode(string code, int sizeInPixels = 111)
        {
            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", code, sizeInPixels, sizeInPixels);
            var response = default(WebResponse);
            var remoteStream = default(Stream);
            var readStream = default(StreamReader);
            var request = WebRequest.Create(url);

            response = request.GetResponse();
            remoteStream = response.GetResponseStream();
            readStream = new StreamReader(remoteStream);
            
            var image = System.Drawing.Image.FromStream(remoteStream);
            
            response.Close();
            remoteStream.Close();
            readStream.Close();

            return image;
        }
    }
}