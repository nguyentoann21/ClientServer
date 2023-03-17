namespace Server
{
    public static class Utils
    {
        public static async Task<byte[]> ToBytes(Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                await instream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static async Task<string> Upload(IFormFile file)
        {
            var fileBytes = await ToBytes(file.OpenReadStream());
            var res = await Program.Imagekit.UploadAsync(new()
            {
                folder = Program.Config["Imagekit:Path"],
                fileName = file.FileName,
                file = fileBytes,
            });
            Console.WriteLine(res);
            Console.WriteLine($"url={res.url}\npath={res.filePath}\nname={res.name} type={res.fileType}");
            return res.url;
        }

        internal static Task<IFormFile> ToBytes(string productImage)
        {
            throw new NotImplementedException();
        }
    }
}
