using System;
using System.IO;
using System.Windows.Forms;

namespace PDFNarrator
{
    public partial class View : Form
    {
        private Controller controller;
        private Model model;

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

        // Evento para notificar o Controller quando o utilizador clica em "Stop Narration"
        public event EventHandler StopNarrationClicked;
        // Evento para notificar o Controller ao fechar a aplicação
        public event EventHandler ExitAppRequested;

        //===============================
        public View(Controller c, Model m)
        {
            controller = c;
            model = m;
            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            // Associação de eventos
            controller.OnSuccessMessage += ShowSuccessMessage;
            controller.OnFailedMessage += ShowErrorMessage;
            model.OnSendPDFData += ReceivePDFData;

            btnStopNarration.Click += (s, e) => StopNarrationClicked?.Invoke(this, e);

            this.FormClosing += (s, e) => ExitAppRequested?.Invoke(this, e);
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
            OnStartNarration?.Invoke(this, e);
            btnStartNarration.ForeColor = System.Drawing.Color.Green;
        }

        public void PlayAudio()
        {
            // Método vazio
        }

        public void AudioStopped()
        {
            // Método vazio
        }

        public void ShowConfirmationMessage()
        {
            // Método vazio
        }

        public void InterfaceClosed()
        {
            // Método vazio
        }

        

                        
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