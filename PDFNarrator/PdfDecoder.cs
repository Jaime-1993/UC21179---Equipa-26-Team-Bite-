using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharpTextExtractor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFNarrator
{

    public interface IPdfReader
    {
        string ExtractText();

        void LoadPDF(string path);

        bool isPathValid(string path);

    }

    public class PdfDecoder : IPdfReader
    {
        private PdfDocument pdfDocument;
        private string str_text_extracted = "";
        private string path = "";

        public string CurrentPath 
        { 
            get { return path; } 
        }

        public string TextExtracted
        {
            get { return str_text_extracted; }
        }

        public string ExtractText()
        {
            string data = "";
            // Returns Text extracted from PDF
            try
            {
                str_text_extracted = Extractor.PdfToText(path);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error extracting text from PDF file.");
            }

            return data;
        }

        public void LoadPDF(string pdf_path)
        {
            try
            {
                pdfDocument = PdfReader.Open(pdf_path);     // Usa o campo pdfDocument
                path = pdf_path;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error loading PDF file.");
            }
        }

        public bool isPathValid(string path)
        {
            // Check if the path is valid
            if (string.IsNullOrWhiteSpace(path))
            {
                //data = "The path supllied is empty";
                return false;
            }

            // Check if the file exist in this path
            if (!File.Exists(path))
            {
                //data = "The path supllied doesn't exist";
                return false;
            }
            return true;
        }
    }
}
