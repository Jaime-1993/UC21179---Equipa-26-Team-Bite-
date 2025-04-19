using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharpTextExtractor;
using System;
using System.Windows.Forms;
using System.IO;
//using System.Xml.Linq; // Comentado: Não é usado no projeto

namespace PDFNarrator
{
    public class Controller
    {
        private View view;
        private Model model;
        private string extractedText;

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
            // Original: view.LoadPDFClicked += (s, e) => LoadPDF();
            view.LoadPDFClicked += (s, e) => LoadPDFFile(); // Ajustado: LoadPDF renomeado para LoadPDFFile para maior clareza
            // Original: view.StartNarrationClicked += (s, e) => StartNarration();
            view.StartNarrationClicked += (s, e) => BeginNarration(); // Ajustado: StartNarration renomeado para BeginNarration para seguir o diagrama
            view.StopNarrationClicked += (s, e) => EndNarration();
            view.ExitAppRequested += (s, e) => ExitApp();

            // Subscreve o evento do Model
            model.PDFLoadedEvent += (s, e) => PDFLoaded();
            // Novo: Subscreve eventos para sincronização e paragem de áudio
            model.AudioSyncedEvent += AudioSynced;
            model.AudioStoppedEvent += (s, e) => AudioStopped();
        }

        public void LaunchApp()
        {
            view.DisplayInterface();
            Application.Run(view);
        }

        // Comentado: Redundante, já implementado na View (DisplayInterface)
        //public void CreateInterface()
        //{
        //    // Método vazio
        //}

        // Comentado: Redundante, já implementado na View (DisplayInterface)
        //public void DisplayInterface()
        //{
        //    // Método vazio
        //}

        // Original: Mantido para referência, mas renomeado para LoadPDFFile
        //public void LoadPDF()
        //{
        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
        //        openFileDialog.Filter = "PDF Files|*.pdf";
        //        openFileDialog.Title = "Select a PDF File";
        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            string filePath = openFileDialog.FileName;
        //            model.UpdateFileStatus(filePath); // Passa o caminho ao Model
        //            PDFLoadRequested?.Invoke(this, EventArgs.Empty);
        //        }
        //        else
        //        {
        //            view.Test_Write_to_TextBox("No PDF file selected.");
        //        }
        //    }
        //}

        // Novo: Renomeado de LoadPDF para maior clareza e consistência com o diagrama
        public void LoadPDFFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "PDF Files|*.pdf";
                openFileDialog.Title = "Select a PDF File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    model.UpdateFileStatus(filePath);
                    PDFLoadRequested?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    view.Test_Write_to_TextBox("No PDF file selected.");
                }
            }
        }

        public void AskToShowPopUpForPathOfPDF()
        {
            // Método vazio
        }

        public void PDFLoaded()
        {
            view.ShowSuccessMessage(extractedText);
        }

        // Comentado: Redundante, já implementado no Model (LoadPDFFile)
        //public void ExtractText()
        //{
        //    // Método vazio
        //}

        // Comentado: Redundante, já implementado no Model (PDFLoaded)
        //public void TextExtracted()
        //{
        //    // Método vazio
        //}

        // Comentado: Redundante, já implementado no Model (UpdateFileStatus)
        //public void UpdateFileStatus()
        //{
        //    // Método vazio
        //}

        // Novo: Reage a falhas no carregamento do ficheiro, conforme diagrama
        public void FileLoadFailed()
        {
            AskToShowErrorMessageOnFileLoad();
        }

        // Novo: Solicita à View para mostrar mensagem de erro ao carregar o ficheiro
        public void AskToShowErrorMessageOnFileLoad()
        {
            view.ShowErrorMessage();
        }

        // Original: Mantido para referência, mas renomeado para BeginNarration
        //public void StartNarration()
        //{
        //    // Dispara evento para o Model iniciar a síntese de áudio
        //    AudioSynthesisRequested?.Invoke(this, EventArgs.Empty);
        //}

        // Novo: Renomeado de StartNarration para BeginNarration para seguir o diagrama
        public void BeginNarration()
        {
            AudioSynthesisRequested?.Invoke(this, EventArgs.Empty);
        }

        // Comentado: Redundante, já implementado no Model (StartAudioSynthesis)
        //public void StartAudioSynthesis()
        //{
        //    // Método vazio
        //}

        // Novo: Reage ao AudioSyncedEvent, atualizando a View com a palavra atual
        private void AudioSynced(object sender, WordEventArgs e)
        {
            // Verifica se o áudio está sincronizado corretamente
            if (!string.IsNullOrEmpty(e.Word))
            {
                AudioDataOK();
            }
            else
            {
                AudioDataNOK();
            }
        }

        // Novo: Sincroniza os dados de áudio durante a narração
        public void SyncAudioData()
        {
            // Método vazio, a implementar conforme necessário
        }

        // Novo: Atualiza os dados de áudio (chamado pelo Model)
        public void UpdateAudioData()
        {
            // Método vazio, a implementar conforme necessário
        }

        // Novo: Atualiza o estado do áudio e verifica se está tudo OK
        public void UpdateAudioStatus()
        {
            // Método vazio, a implementar conforme necessário
        }

        // Comentado: Não usado no diagrama, substituído por AudioDataNOK
        //public void SyncFailed()
        //{
        //    // Método vazio
        //}

        // Novo: Chamado quando os dados de áudio estão OK
        public void AudioDataOK()
        {
            view.StartNarrationButtonAnimation();
            view.PlayAudio(extractedText); // Usa o texto completo para animação inicial
        }

        // Novo: Chamado quando os dados de áudio não estão OK
        public void AudioDataNOK()
        {
            view.ShowErrorMessage();
        }

        public void EndNarration()
        {
            // Dispara evento para o Model parar a síntese de áudio
            StopAudioSynthesisRequested?.Invoke(this, EventArgs.Empty);
        }

        // Comentado: Redundante, já implementado no Model (StopAudioSynthesis)
        //public void StopAudioSynthesis()
        //{
        //    // Método vazio
        //}

        // Novo: Reage ao AudioStoppedEvent, notificando a View
        public void AudioStopped()
        {
            view.AudioStopped();
            view.ShowConfirmationMessage();
        }

        // Novo: Fecha a interface (chamado pelo ExitApp)
        public void CloseInterface()
        {
            model.UpdateWindowStatus();
        }

        public void ExitApp()
        {
            // Método vazio
            // Novo: Adicionada lógica para fechar a interface antes de sair
            CloseInterface();
        }

        public void SetExtractedText(string text)
        {
            extractedText = text;
        }

        // Novo: Obtém o texto extraído do PDF
        public string GetExtractedText()
        {
            return extractedText;
        }

        // Comentado: Método de teste, não usado no diagrama
        //public void TEST_READPDF()
        //{
        //    // Criar um OpenFileDialog para selecionar o PDF
        //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
        //    {
        //        openFileDialog.Filter = "PDF Files|*.pdf"; // Filtra apenas ficheiros PDF
        //        openFileDialog.Title = "Select a PDF File";
        //
        //        // Mostrar o pop-up e verificar se o utilizador selecionou um ficheiro
        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            // Obter o caminho do ficheiro selecionado
        //            string filePath = openFileDialog.FileName;
        //
        //            // Abrir o PDF e extrair texto
        //            PdfDocument doc = PdfReader.Open(filePath);
        //            string data001 = Extractor.PdfToText(filePath);
        //
        //            // Mostrar o texto extraído na View
        //            view.Test_Write_to_TextBox(data001);
        //        }
        //        else
        //        {
        //            // O utilizador cancelou a seleção
        //            view.Test_Write_to_TextBox("No PDF file selected.");
        //        }
        //    }
        //}
    }
}