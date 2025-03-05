using Application.Contracts.Services;
using DocumentFormat.OpenXml.Packaging;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using Microsoft.AspNetCore.Components.Forms;

namespace Infrastructure.Services
{
    public class FileService : IFileService
    {
        public async Task<string?> ExtractTextFromFileAsync(IBrowserFile file)
        {
            if (file == null || file.Size == 0)
                throw new ArgumentException("File is invalid");

            var extension = System.IO.Path.GetExtension(file.Name).ToLower();
            using var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return extension switch
            {
                ".txt" => Encoding.UTF8.GetString(memoryStream.ToArray()),
                ".pdf" => ExtractTextFromPdf(memoryStream),
                ".docx" => ExtractTextFromDocx(memoryStream),
                _ => throw new NotSupportedException($"File type '{extension}' not supported"),
            };
        }

        private static string ExtractTextFromPdf(Stream pdfStream)
        {
            pdfStream.Position = 0;
            using var reader = new PdfReader(pdfStream);
            StringBuilder text = new();

            for (var i = 1; i <= reader.NumberOfPages; i++)
            {
                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
            }

            return text.ToString();
        }

        private static string? ExtractTextFromDocx(Stream docxStream)
        {
            docxStream.Position = 0;
            using var document = WordprocessingDocument.Open(docxStream, false);
            return document.MainDocumentPart?.Document.Body?.InnerText;
        }
    }
}
