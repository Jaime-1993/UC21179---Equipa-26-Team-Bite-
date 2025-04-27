using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharpTextExtractor;
using System;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace PDFNarrator
{
    public class Model
    {
        private Controller controller;
        private View view;
        private PdfDocument pdfDocument;

        private string str_text_extracted = "";

        // Evento para notificar a VIEW com a informação do PDF
        public event SendPDFdata_Handler OnSendPDFData;
        public delegate void SendPDFdata_Handler(string data);

        public event AudioData_Handler OnAudioData;
        public delegate void AudioData_Handler(string audio_data);

        public event Action OnExitApp;

        // Delegado para notificar o Controller sobre o estado do PDF ou áudio
        //public delegate void StatusUpdateHandler(string data, EventArgs e);
        //public event StatusUpdateHandler AudioSyncedEvent;
        //public event StatusUpdateHandler AudioStoppedEvent;

        public Model(Controller c, View v)
        {
            controller = c;
            view = v;
        }

        public void setView(View v)
        {
            view = v;
        }

        public void setupEvents()
        {
            // Ligar eventos do Controller aos métodos do Model
            view.OnGetPDFData += GetPDFData;
        }

        /////////////////////////////////////////////
        public void LoadPDFFile(string path)
        {   
            try {
                pdfDocument = PdfReader.Open(path); // Usa o campo pdfDocument
            } catch (Exception) {
                throw new InvalidOperationException("Error loading PDF file.");
            }

        }

        public void ExtractText(string path)
        {
            // Returns Text extracted from PDF
            try {
                str_text_extracted = Extractor.PdfToText(path);
            } catch (Exception) {
                throw new InvalidOperationException("Error extracting text from PDF file.");
            }
        }

        public void GetPDFData(string data)
        {
            OnSendPDFData?.Invoke(str_text_extracted);
        }

        /////////////////////////////////////////////
        public int StartAudioSynthesis()
        {
            // Verifica se o texto extraído está vazio
            if (str_text_extracted == "") return -1;
            
            // Inicia a síntese de áudio com o texto extraído
            view.OnSyncAudioData += AudioDataUpdate;
            return 0;
        }

        public void AudioDataUpdate()
        {
            // Atualização o texto para a reprodução de áudio e notifica a VIEW
            OnAudioData?.Invoke(str_text_extracted);
        }

        public void UpdateAudioStatus()
        {
            // Método vazio
        }

        public void StopAudioSynthesis()
        {
            // Simula paragem da síntese de áudio e notifica o Controller
            view.OnSyncAudioData -= AudioDataUpdate;
        }

        /////////////////////////////////////////////
        public void CloseInterface()
        {
            // Limpa todos os eventos que possam existir
            OnSendPDFData = null;
            OnAudioData = null;

            // Avisa a VIEW que irá fechar a aplicação
            OnExitApp?.Invoke();
        }
    }
}