using K9.Base.WebApplication.Config;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using System.IO;
using System.Net;

namespace K9.WebApplication.Services
{
    public class QrCodeService : IQrCodeService
    {
        private readonly ApiConfiguration _config;

        public QrCodeService(IOptions<ApiConfiguration> config)
        {
            _config = config.Value;
        }

        public System.Drawing.Image GetQrCode(string code, int sizeInPixels = 111)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var url = string.Format(_config.QrCodeApiUrl, sizeInPixels, code);
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