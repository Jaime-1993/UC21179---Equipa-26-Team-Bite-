using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharpTextExtractor;
using System;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PDFNarrator
{
    public class Controller
    {
        private View view;
        private Model model;


        // Evento para printar informação na View-Sucess
        public event SuccessMsg_Handler OnSuccessMessage;
        public delegate void SuccessMsg_Handler();

        // Evento para printar informação na View-Sucess
        public event FailedMsg_Handler OnFailedMessage;
        public delegate void FailedMsg_Handler(string text);

        // Evento para notificar o Model de ações como carregar PDF ou iniciar narração
        public event EventHandler PDFLoadRequested;
        public event EventHandler AudioSynthesisRequested;
        public event EventHandler StopAudioSynthesisRequested;

        public Controller()
        {
            model = new Model(this, view);
            view = new View(this, model);

            model.setView(view);
            model.setupEvents();

            // Ligar eventos da View aos métodos do Controller
            view.OnLoadPDF += LoadPDF;

            view.OnStartNarration += BeginNarration;
            view.StopNarrationClicked += (s, e) => EndNarration();
            view.ExitAppRequested += (s, e) => ExitApp();
        }

        public void LaunchApp()
        {
            view.CreateInterface();
            view.AnimateButtons();
        }

        public void CreateInterface()
        {
            // Método vazio
        }

        public void DisplayInterface()
        {
            // Método vazio
        }

        ///////////////////////////////////////////////
        public void LoadPDF(string path)
        {
            string data;

            // Check if the path is valid
            if (string.IsNullOrWhiteSpace(path)) {
                data = "The path supllied is empty";
                AskToShowErrorMessageOnFileLoad(data);
                return;
            }

            // Check if the file exist in this path
            if (!File.Exists(path)) {
                data = "The path supllied doesn't exist";
                AskToShowErrorMessageOnFileLoad(data);
                return;
            }

            // Send path to Model to load the PDF
            // Returns Info or Error
            if (model.LoadPDFFile(path) == 0) {
                if (model.ExtractText(path) == 0) 
                    UpdateFileStatus();
                else
                {
                    data = "Error extracting text from PDF file.";
                    AskToShowErrorMessageOnFileLoad(data);
                }

            } else {
                data = "Error loading PDF file.";
                AskToShowErrorMessageOnFileLoad(data);
            }

        }

        public void UpdateFileStatus()
        {
            OnSuccessMessage?.Invoke();
        }

        public void AskToShowErrorMessageOnFileLoad(string data)
        {
            OnFailedMessage?.Invoke(data);
        }

        public void SetPDFData()
        {
            // Método vazio
        }
        
        ////////////////////////////////////////////////
        public void BeginNarration(object sender, EventArgs e)
        {
            model.StartAudioSynthesis();
        }

        public void StartAudioSynthesis()
        {
            // Método vazio
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

        public void EndNarration()
        {
            // Dispara evento para o Model parar a síntese de áudio
            StopAudioSynthesisRequested?.Invoke(this, EventArgs.Empty);
        }

        public void StopAudioSynthesis()
        {
            // Método vazio
        }

        public void AudioStopped()
        {
            // Método vazio
        }

        public void CloseInterface()
        {
            // Método vazio
        }

        public void ExitApp()
        {
            // Método vazio
        }

    }
}