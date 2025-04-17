using System;
using System.Windows.Forms;

namespace PDFNarrator
{
    public partial class View : Form
    {
        private Controller controller;
        private Model model;

        // Evento para notificar o Controller quando o utilizador clica em "Load PDF"
        public event EventHandler LoadPDFClicked;
        // Evento para notificar o Controller quando o utilizador clica em "Start Narration"
        public event EventHandler StartNarrationClicked;
        // Evento para notificar o Controller quando o utilizador clica em "Stop Narration"
        public event EventHandler StopNarrationClicked;
        // Evento para notificar o Controller ao fechar a aplicação
        public event EventHandler ExitAppRequested;

        public View(Controller c, Model m)
        {
            controller = c;
            model = m;
            InitializeComponent();
            SetupControls();
            
            //Test
        }

        private void SetupControls()
        {
            // Associar cliques aos eventos
            btnLoadPDF.Click += (s, e) => LoadPDFClicked?.Invoke(this, e);
            btnStartNarration.Click += (s, e) => StartNarrationClicked?.Invoke(this, e);
            btnStopNarration.Click += (s, e) => StopNarrationClicked?.Invoke(this, e);

            this.FormClosing += (s, e) => ExitAppRequested?.Invoke(this, e);
        }

        public void DisplayInterface()
        {
            // Método vazio
        }

        public void DisplayInterfaceWithPDFContent()
        {
            // Método vazio
        }

        public void ShowPopUpPromptToLoadPathWherePDF()
        {
            // Método vazio
        }

        public void ShowSuccessMessage()
        {
            // Método vazio
        }

        public void ShowErrorMessage()
        {
            // Método vazio
        }

        public void ShowErrorMessageOnFileLoad()
        {
            // Método vazio
        }

        public void StartNarrationButtonAnimation()
        {
            // Método vazio
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

        public void DisplayButtonAnimation()
        {
            // Método vazio
        }

        public void AnimateButtons()
        {
            // Método vazio
        }

        public void Test_Write_to_TextBox(String data)
        {
            txtOutput.Text = data;
        }

        //private void btn_dummy_Click(object sender, EventArgs e)
        //{
        //    controller.TEST_READPDF();
        //}

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
        }
    }
}