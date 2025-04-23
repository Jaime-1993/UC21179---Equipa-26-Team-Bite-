using System;
using System.IO;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace PDFNarrator
{
    public partial class View : Form
    {
        private Controller controller;
        private Model model;
        private SpeechSynthesizer synthesizer;

        //===============================
        //        Init Variables
        //===============================
        // Inicializa o diretório inicial para o OpenFileDialog
        private string LoadPDF_path = AppDomain.CurrentDomain.BaseDirectory;


        //===============================
        //    Generate Events Data
        //===============================
        // Evento para notificar o Controller quando o utilizador clica em "Load PDF"
        public event LoadPDFpath_Handler OnLoadPDF;
        public delegate void LoadPDFpath_Handler(string path);

        // Evento para notificar a MODEL com a informação do PDF
        public event GetPDFdata_Handler OnGetPDFData;
        public delegate void GetPDFdata_Handler(string data);

        // Evento para notificar o Controller quando o utilizador clica em "Start Narration"
        public event EventHandler OnStartNarration;

        // Evento para notificar a MODEL com a informação do PDF
        public event SyncAudioData_Handler OnSyncAudioData;
        public delegate void SyncAudioData_Handler();

        // Evento para notificar o Controller quando o utilizador clica em "Stop Narration"
        public event EventHandler OnStopNarration;

        // Evento para fechar aplicação
        public event Action OnExitApp;

        //===============================
        public View(Controller c, Model m)
        {
            // Inicializa o Controller e o Model
            controller = c;
            model = m;
            
            // Inicializa o SpeechSynthesizer
            synthesizer = new SpeechSynthesizer();
            ConfigSynthesizer(synthesizer);

            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            // Associação de eventos  
            controller.OnSuccessMessage += ShowSuccessMessage;
            controller.OnFailedMessage += ShowErrorMessage;
            controller.OnAudioInfoStatus += AudioDataStatus;
            controller.OnStoppedNarration += Narration_Stopped;

            model.OnSendPDFData += ReceivePDFData;
            model.OnAudioData += SyncAudioData;
            model.OnExitApp += ExitApp;

            FormClosing += (sender, e) => OnExitApp?.Invoke();
        }

        private void ConfigSynthesizer(SpeechSynthesizer synthesizer)
        {
            // Configurações do SpeechSynthesizer
            synthesizer.Rate = 0; // Velocidade normal
            synthesizer.Volume = 100; // Volume máximo
            synthesizer.SelectVoice("Microsoft Zira Desktop"); // Seleciona a voz padrão
        }

        public void CreateInterface()
        {
            // Creates and Displays Interface to user
            Application.Run(this);
        }

        public void AnimateButtons()
        {
            // Displays Buttons Animation to User
            // Método vazio
        }

        //////////////////////////////////////////
        private void Click_LoadPDF(object sender, EventArgs e)
        {
            // Test if "OnLoadPDF" event as any subscribers
            if (OnLoadPDF == null)
            {
                Test_Write_to_TextBox("Program is processing the PDF.");
                return;
            }

            // Activate Pop-Up Window to ask for PDF path
            string data = ShowPopUpPromptToLoadPathWherePDF();

            // Check if its a valid path
            if (data != "")
            {
                OnLoadPDF(data);
            }
            else
            {
                Test_Write_to_TextBox("No PDF file selected.");
            }

        }

        public string ShowPopUpPromptToLoadPathWherePDF()
        {
            // Criar um OpenFileDialog para selecionar o PDF
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "Select a PDF File";
            // Inicializa o diretório inicial
            openFileDialog.InitialDirectory = LoadPDF_path;
            // Filtra apenas ficheiros PDF
            openFileDialog.Filter = "PDF Files|*.pdf";

            // Mostrar o pop-up e verificar se o utilizador selecionou um ficheiro
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                // O utilizador cancelou a seleção
                return "";
            }

            // Obter o caminho do ficheiro selecionado
            LoadPDF_path = Path.GetDirectoryName(openFileDialog.FileName);
            return openFileDialog.FileName;
        }

        public void ShowSuccessMessage()
        {
            MessageBox.Show("PDF loaded successfully!", "\"Load PDF\" Click Action - Success");
            OnGetPDFData?.Invoke(null);
        }

        public void ShowErrorMessage(string errorMessage)
        {
            MessageBox.Show($"PDF Failed to be Loadead!\n\n{errorMessage}", "\"Load PDF\" Click Action - Fail");
        }

        public void ReceivePDFData(string extractedText)
        {
            txtOutput.Text = extractedText;
        }

        //////////////////////////////////
        private void Click_StartNarrattion(object sender, EventArgs e)
        {
            OnStartNarration?.Invoke(this, EventArgs.Empty);
        }

        public void AudioDataStatus(bool isAudioDataAvailable)
        {
            if (isAudioDataAvailable)
            {
                btnStartNarration.ForeColor = System.Drawing.Color.Green;
                OnSyncAudioData?.Invoke();
            }
            else
                MessageBox.Show("No audio data available to play.", "Audio Data Info");
        }

        private void SyncAudioData(string audio_data)
        {
            PlayAudio(audio_data);
            //OnSyncAudioData?.Invoke();
        }

        public void PlayAudio(string audio_data)
        {
            synthesizer.SpeakAsync(audio_data);
        }

        //////////////////////////////////
        private void Click_StopNarration(object sender, EventArgs e)
        {
            // Change Color on the button "Stop Narration"
            btnStopNarration.ForeColor = System.Drawing.Color.Red;
            // Kills the sound effect
            synthesizer.SpeakAsyncCancelAll();
            // Change Color on the button "Start Narration"
            btnStartNarration.ForeColor = System.Drawing.Color.Black;
            // Notifies subscription of "Stop Narration" Event
            OnStopNarration?.Invoke(this, EventArgs.Empty);
        }

        public void Narration_Stopped()
        {
            btnStopNarration.ForeColor = System.Drawing.Color.Black;
            MessageBox.Show("Narration stopped.", "Narration Stopped");
        }

        //////////////////////////////////
        public void ExitApp()
        {
            OnLoadPDF = null;
            OnGetPDFData = null;
            OnStartNarration = null;
            OnSyncAudioData = null;
            OnStopNarration = null;

            // Close the application
            Application.Exit();
        }

        // Test Programs

        public void Test_Write_to_TextBox(string data)
        {
            txtOutput.Text = data;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
        }

        
    }
}