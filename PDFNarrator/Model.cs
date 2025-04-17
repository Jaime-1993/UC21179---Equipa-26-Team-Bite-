using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharpTextExtractor;
using System;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace PDFNarrator
{
    public class Model
    {
        private Controller controller;
        private View view;
        private PdfDocument pdfDocument;
        private SpeechSynthesizer synthesizer;

        private string currentFilePath;

        // Delegado para notificar o Controller sobre o estado do PDF ou áudio
        public delegate void StatusUpdateHandler(object sender, EventArgs e);
        public event StatusUpdateHandler PDFLoadedEvent;
        public event StatusUpdateHandler AudioSyncedEvent;
        public event StatusUpdateHandler AudioStoppedEvent;

        public Model(Controller c, View v)
        {
            controller = c;
            view = v;
            synthesizer = new SpeechSynthesizer();

            // Ligar eventos do Controller aos métodos do Model
            if (controller != null)
            {
                controller.PDFLoadRequested += (s, e) => LoadPDFFile();
                controller.AudioSynthesisRequested += (s, e) => StartAudioSynthesis();
                controller.StopAudioSynthesisRequested += (s, e) => StopAudioSynthesis();
            }
        }

        public void SetView(View v)
        {
            view = v;
        }

        public void LoadPDFFile()
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                try
                {
                    pdfDocument = PdfReader.Open(currentFilePath); // Usa o campo pdfDocument
                    string data001 = Extractor.PdfToText(currentFilePath);
                    controller.SetExtractedText(data001); // Usa o método público
                    PDFLoadedEvent?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception)
                {
                    view.Test_Write_to_TextBox("Error loading PDF file.");
                }
            }
        }

        public void PDFLoaded()
        {
            // Método vazio
        }

        public void ExtractText()
        {
            // Método vazio
        }

        public void TextExtracted()
        {
            // Método vazio
        }

        public void UpdateFileStatus(string filePath)
        {
            currentFilePath = filePath;
        }

        public void FileLoadFailed()
        {
            // Método vazio
        }

        public void StartAudioSynthesis()
        {
            // Simula início da síntese de áudio e notifica o Controller
            AudioSyncedEvent?.Invoke(this, EventArgs.Empty);
        }

        public void SyncAudioData()
        {
            // Método vazio
        }

        public void UpdateAudioData()
        {
            // Método vazio
        }

        public void UpdateAudioStatus()
        {
            // Método vazio
        }

        public void SyncFailed()
        {
            // Método vazio
        }

        public void StopAudioSynthesis()
        {
            // Simula paragem da síntese de áudio e notifica o Controller
            AudioStoppedEvent?.Invoke(this, EventArgs.Empty);
        }

        public void AudioStopped()
        {
            // Método vazio
        }

        public void UpdateWindowStatus()
        {
            // Método vazio
        }

        public void CloseInterface()
        {
            // Método vazio
        }
    }
}