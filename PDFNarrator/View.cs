using System;
using System.Windows.Forms;

namespace PDFNarrator
{
    public partial class View : Form
    {
        private Controller controller;
        private Model model;
        private Button btnLoadPDF;
        private Button btnStartNarration;
        private Button btnStopNarration;
        private TextBox txtOutput;

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
        }

        private void SetupControls()
        {
            btnLoadPDF = new Button { Text = "Load PDF", Location = new System.Drawing.Point(10, 10), Size = new System.Drawing.Size(100, 30) };
            btnStartNarration = new Button { Text = "Start Narration", Location = new System.Drawing.Point(120, 10), Size = new System.Drawing.Size(100, 30) };
            btnStopNarration = new Button { Text = "Stop Narration", Location = new System.Drawing.Point(230, 10), Size = new System.Drawing.Size(100, 30) };
            txtOutput = new TextBox { Location = new System.Drawing.Point(10, 50), Size = new System.Drawing.Size(320, 150), Multiline = true, ReadOnly = true };

            // Associar cliques aos eventos
            btnLoadPDF.Click += (s, e) => LoadPDFClicked?.Invoke(this, e);
            btnStartNarration.Click += (s, e) => StartNarrationClicked?.Invoke(this, e);
            btnStopNarration.Click += (s, e) => StopNarrationClicked?.Invoke(this, e);

            this.Controls.Add(btnLoadPDF);
            this.Controls.Add(btnStartNarration);
            this.Controls.Add(btnStopNarration);
            this.Controls.Add(txtOutput);
            this.Text = "PDF Narrator";
            this.Size = new System.Drawing.Size(360, 250);
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
    }
}