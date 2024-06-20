namespace Almacen.Services
{
    public interface IAlmacenadorPdf
    {
        Task<string> GuardarPDF(string pdfBase64, string container);
    }
}
