using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharpTextExtractor;
using System;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.Threading.Tasks; // Novo: Adicionado para suportar narração assíncrona

namespace PDFNarrator
{
    public class Model
    {
        private Controller controller;
        private View view;
        private PdfDocument pdfDocument;
        private SpeechSynthesizer synthesizer;
        private string currentFilePath;
        // Novo: Adicionado para controlar o estado da narração e evitar múltiplas narrações simultâneas
        private bool isNarrating;

        // Delegado para notificar o Controller sobre o estado do PDF ou áudio
        // Original: public delegate void StatusUpdateHandler(object sender, EventArgs e);
        // Novo: Ajustado para suportar WordEventArgs no AudioSyncedEvent
        public delegate void StatusUpdateHandler(object sender, WordEventArgs e);
        // Novo: Adicionado para eventos simples que não precisam de WordEventArgs
        public delegate void SimpleStatusUpdateHandler(object sender, EventArgs e);

        public event StatusUpdateHandler AudioSyncedEvent;
        // Original: public event StatusUpdateHandler PDFLoadedEvent;
        public event SimpleStatusUpdateHandler PDFLoadedEvent; // Ajustado para usar SimpleStatusUpdateHandler
        // Original: public event StatusUpdateHandler AudioStoppedEvent;
        public event SimpleStatusUpdateHandler AudioStoppedEvent; // Ajustado para usar SimpleStatusUpdateHandler

        public Model(Controller c, View v)
        {
            controller = c;
            view = v;
            synthesizer = new SpeechSynthesizer();
            // Novo: Inicializa o estado da narração
            isNarrating = false;

            // Ligar eventos do Controller aos métodos do Model
            if (controller != null)
            {
                controller.PDFLoadRequested += (s, e) => LoadPDFFile();
                controller.AudioSynthesisRequested += (s, e) => StartAudioSynthesis();
                controller.StopAudioSynthesisRequested += (s, e) => StopAudioSynthesis();
            }

            // Novo: Subscreve eventos do SpeechSynthesizer para acompanhar o progresso
            synthesizer.SpeakCompleted += (s, e) => OnSpeakCompleted();
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
                    // Novo: Notifica o Controller em caso de falha
                    controller.FileLoadFailed();
                    view.Test_Write_to_TextBox("Error loading PDF file.");
                }
            }
        }

        public void PDFLoaded()
        {
            // Método vazio
            // Novo: Não é necessário chamar TextExtracted, o Controller já reage ao PDFLoadedEvent
        }

        // Comentado: Redundante, já implementado em LoadPDFFile
        //public void ExtractText()
        //{
        //    // Método vazio
        //}

        // Comentado: Redundante, já implementado em PDFLoaded
        //public void TextExtracted()
        //{
        //    // Método vazio
        //}

        public void UpdateFileStatus(string filePath)
        {
            currentFilePath = filePath;
        }

        // Comentado: Redundante, já implementado no Controller (FileLoadFailed)
        //public void FileLoadFailed()
        //{
        //    // Método vazio
        //}

        // Original: Mantido para referência, mas substituído pela nova implementação assíncrona
        //public void StartAudioSynthesis()
        //{
        //    // Simula início da síntese de áudio e notifica o Controller
        //    AudioSyncedEvent?.Invoke(this, EventArgs.Empty);
        //}

        // Novo: Implementa a síntese de áudio palavra a palavra de forma assíncrona
        public async Task StartAudioSynthesis()
        {
            if (isNarrating) return; // Evita múltiplas narrações simultâneas
            isNarrating = true;

            // Obtém o texto a narrar
            string textToNarrate = controller.GetExtractedText() ?? "No text to narrate.";
            string[] words = textToNarrate.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Narra cada palavra de forma assíncrona
            foreach (string word in words)
            {
                if (!isNarrating) break; // Para a narração se StopAudioSynthesis for chamado

                synthesizer.SpeakAsync(word);
                AudioSyncedEvent?.Invoke(this, new WordEventArgs(word));
                SyncAudioData();
                UpdateAudioData();

                // Aguarda até que a fala da palavra termine antes de passar à próxima
                await Task.Run(() =>
                {
                    while (synthesizer.State == SynthesizerState.Speaking)
                    {
                        Task.Delay(50).Wait();
                    }
                });
            }

            if (isNarrating)
            {
                controller.UpdateAudioStatus();
                isNarrating = false;
            }
        }

        // Novo: Chamado quando a fala de uma palavra termina
        private void OnSpeakCompleted()
        {
            // Método vazio, pode ser usado para ações adicionais
        }

        public void SyncAudioData()
        {
            // Método vazio
            // Novo: A implementar conforme necessário
        }

        public void UpdateAudioData()
        {
            // Método vazio
            // Novo: A implementar conforme necessário
        }

        public void UpdateAudioStatus()
        {
            // Método vazio
            // Novo: A implementar conforme necessário
        }

        // Comentado: Não usado no diagrama, substituído por lógica no Controller (AudioDataNOK)
        //public void SyncFailed()
        //{
        //    // Método vazio
        //}

        public void StopAudioSynthesis()
        {
            // Simula paragem da síntese de áudio e notifica o Controller
            // Novo: Adicionada lógica para cancelar a narração assíncrona
            isNarrating = false;
            synthesizer.SpeakAsyncCancelAll();
            AudioStoppedEvent?.Invoke(this, EventArgs.Empty);
        }

        public void AudioStopped()
        {
            // Método vazio
            // Novo: A implementar conforme necessário
        }

        public void UpdateWindowStatus()
        {
            // Método vazio
            // Novo: Chama InterfaceClosed na View para fechar a interface
            view.InterfaceClosed();
        }
    }

    public class WordEventArgs : EventArgs
    {
        // Método vazio
        // Novo: Adicionada propriedade e construtor para suportar narração palavra a palavra
        public string Word { get; }

        public WordEventArgs(string word)
        {
            Word = word;
        }
    }
}