using System.Security.Cryptography;
using System.Text;
namespace PVMS.Infrastructure.Extensions
{
    public static class InnovaExtentions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        public static string HashedPassword(this string passowrd) => string.Join("", SHA1.HashData(Encoding.UTF8.GetBytes(passowrd)).Select(x => x.ToString("X2"))).ToUpper();
        public static async Task<string> UplodaFiles(this string base64, string type = ".apk", string folder = "applications", string name = "")
        {
            string fileName = string.IsNullOrEmpty(name) ? Guid.NewGuid().ToString("N") + type : name + type;
            string path = @"C:\inetpub\wwwroot\PVMS.FE\assets\" + folder;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, fileName);
            await File.WriteAllBytesAsync(filePath, Convert.FromBase64String(base64));
            return fileName;
        }
    }
}
