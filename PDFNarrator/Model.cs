using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf.Content;
using PdfSharpTextExtractor;
using System;
using System.Drawing;
using System.IO;
using System.Speech.Synthesis;

namespace PDFNarrator
{
    public class Model
    {
        private Controller controller;
        private View view;
        private PdfDocument pdfDocument;
        private SpeechSynthesizer synthesizer;

        private string str_text_extracted = "";

        // Evento para notificar a VIEW com a informação do PDF
        public event SendPDFdata_Handler OnSendPDFData;
        public delegate void SendPDFdata_Handler(string data);

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
        }

        public void setView(View v)
        {
            view = v;
        }

        public void setupEvents()
        {
            // Ligar eventos do Controller aos métodos do Model
            //view.OnGetPDFData += GetPDFData;
            view.OnGetPDFData += GetPDFData;
            controller.AudioSynthesisRequested += (s, e) => StartAudioSynthesis();
            controller.StopAudioSynthesisRequested += (s, e) => StopAudioSynthesis();
        }

        public int LoadPDFFile(string path)
        {
            int result;
            
            try {
                pdfDocument = PdfReader.Open(path); // Usa o campo pdfDocument
                result = 0;
            } catch (Exception) {
                result = -1;
            }

            return result;
        }

        public int ExtractText(string path)
        {
            // Returns Text extracted from PDF
            try
            {
                str_text_extracted = Extractor.PdfToText(path);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void GetPDFData(string data)
        {
            OnSendPDFData?.Invoke(str_text_extracted);
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