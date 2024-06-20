namespace Almacen.Services
{
    public class AlmacenadorArchivosLocal : IAlmacenadorPdf
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AlmacenadorArchivosLocal(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> GuardarPDF(string pdfBase64, string container)
        {
            byte[] bytes = Convert.FromBase64String(pdfBase64);
            string fileName = Guid.NewGuid().ToString() + ".pdf";
            string folder = Path.Combine(webHostEnvironment.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string filePath = Path.Combine(folder, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, bytes);
            var currentURL = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var pdfURL = Path.Combine(currentURL, container, fileName).Replace("\\", "/");
            return pdfURL;
        }
    }
}
