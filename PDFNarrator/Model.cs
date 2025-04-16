using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Reflection;
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

        // Delegado para notificar o Controller sobre o estado do PDF ou áudio
        public delegate void StatusUpdateHandler(object sender, EventArgs e);
        public event StatusUpdateHandler PDFLoadedEvent;
        public event StatusUpdateHandler FileLoadFailedEvent;
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

        private void LoadPDFFile()
        {
            // Usar janela de procura do Windows
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*";
                openFileDialog.Title = "Escolher PDF";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        // pdfSharp para carregar documento
                        PdfDocument document = PdfReader.Open(filePath, PdfDocumentOpenMode.Import);

                        Console.WriteLine("PDF Loaded. Total pages: " + document.PageCount);

                        // pdf load sucesso
                        PDFLoadedEvent?.Invoke(this, EventArgs.Empty);
                    }
                    catch (Exception ex)
                    {
                        // pdf load sucesso
                        MessageBox.Show("Erro: " + ex.Message);
                        FileLoadFailedEvent?.Invoke(this, EventArgs.Empty);

                        
                    }
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

        public void UpdateFileStatus()
        {
            // Método vazio
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