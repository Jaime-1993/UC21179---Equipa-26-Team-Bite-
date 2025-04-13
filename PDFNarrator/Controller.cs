using System;
using System.Windows.Forms;

namespace PDFNarrator
{
    public class Controller
    {
        private View view;
        private Model model;

        // Evento para notificar o Model de ações como carregar PDF ou iniciar narração
        public event EventHandler PDFLoadRequested;
        public event EventHandler AudioSynthesisRequested;
        public event EventHandler StopAudioSynthesisRequested;

        public Controller()
        {
            model = new Model(this, null);
            view = new View(this, model);
            model.SetView(view);

            // Ligar eventos da View aos métodos do Controller
            view.LoadPDFClicked += (s, e) => LoadPDF();
            view.StartNarrationClicked += (s, e) => StartNarration();
            view.StopNarrationClicked += (s, e) => EndNarration();
            view.ExitAppRequested += (s, e) => ExitApp();
        }

        public void LaunchApp()
        {
            view.DisplayInterface();
            Application.Run(view);
        }

        public void CreateInterface()
        {
            // Método vazio
        }

        public void DisplayInterface()
        {
            // Método vazio
        }

        public void LoadPDF()
        {
            // Dispara evento para o Model carregar o PDF
            PDFLoadRequested?.Invoke(this, EventArgs.Empty);
        }

        public void AskToShowPopUpForPathOfPDF()
        {
            // Método vazio
        }

        public void LoadPDFFile()
        {
            // Método vazio
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

        public void AskToShowErrorMessageOnFileLoad()
        {
            // Método vazio
        }

        public void BeginNarration()
        {
            // Método vazio
        }

        public void StartNarration()
        {
            // Dispara evento para o Model iniciar a síntese de áudio
            AudioSynthesisRequested?.Invoke(this, EventArgs.Empty);
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