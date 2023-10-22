namespace K9.WebApplication.Services
{
    public interface IQrCodeService
    {
        System.Drawing.Image GetQrCode(string code, int sizeInPixels = 111);
    }
}