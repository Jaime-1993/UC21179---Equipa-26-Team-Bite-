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


        // Evento para printar informação na View ==> Sucess
        public event SuccessMsg_Handler OnSuccessMessage;
        public delegate void SuccessMsg_Handler();

        // Evento para printar informação na View ==> Failed
        public event FailedMsg_Handler OnFailedMessage;
        public delegate void FailedMsg_Handler(string text);

        // Atualiza a informação sobre o estado inicial do text audio data
        public event Action<bool> OnAudioInfoStatus;
        public event Action OnStoppedNarration;

        public Controller()
        {
            model = new Model(this, view);
            view = new View(this, model);

            // Updates model on VIEW info
            model.setView(view);
            model.setupEvents();

            SetupEvents();
        }

        public void SetupEvents()
        {
            // Ligar eventos do Controller aos métodos do Model
            view.OnLoadPDF += LoadPDF;
            view.OnStartNarration += BeginNarration;
            view.OnStopNarration += EndNarration;
            view.OnExitApp += ExitApp;
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
            if (model.StartAudioSynthesis() == 0)
                OnAudioInfoStatus?.Invoke(true);
            else
                OnAudioInfoStatus?.Invoke(false);
        }

        public void EndNarration(object sender, EventArgs e)
        {
            // Stop the audio synthesis
            model.StopAudioSynthesis();
            OnStoppedNarration?.Invoke();
        }

        ////////////////////////////////////////////////
        public void ExitApp()
        {
            OnSuccessMessage = null;
            OnFailedMessage = null;
            OnAudioInfoStatus = null;
            OnStoppedNarration = null;

            model.CloseInterface();
        }

    }
}