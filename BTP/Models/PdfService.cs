using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Text;

public class PdfService
{
    public static string ExtractTextFromPdf(string filePath)
    {
        StringBuilder text = new StringBuilder();

        using (PdfReader reader = new PdfReader(filePath))
        {
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text.Append(PdfTextExtractor.GetTextFromPage(reader, page));
            }
        }

        return text.ToString();
    }
}
