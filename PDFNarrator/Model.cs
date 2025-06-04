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
        public PdfDecoder pdfDecoder;

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

            pdfDecoder = new PdfDecoder();
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
                pdfDecoder.LoadPDF(path);
            } catch (InvalidOperationException e) {
                throw new InvalidOperationException(e.Message);
            }

        }

        public void ExtractText()
        {
            // Returns Text extracted from PDF
            try {
                pdfDecoder.ExtractText();
            } catch (InvalidOperationException e) {
                throw new InvalidOperationException(e.Message);
            }
        }

        public void GetPDFData(string data)
        {
            OnSendPDFData?.Invoke(pdfDecoder.TextExtracted);
        }

        /////////////////////////////////////////////
        public int StartAudioSynthesis()
        {
            // Verifica se o texto extraído está vazio
            if (pdfDecoder.TextExtracted == "") return -1;
            
            // Inicia a síntese de áudio com o texto extraído
            view.OnSyncAudioData += AudioDataUpdate;
            return 0;
        }

        public void AudioDataUpdate()
        {
            // Atualização o texto para a reprodução de áudio e notifica a VIEW
            OnAudioData?.Invoke(pdfDecoder.TextExtracted);
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