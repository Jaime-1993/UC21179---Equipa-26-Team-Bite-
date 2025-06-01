using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFNarrator.Interfaces
{
    // Delegates
    public delegate void SuccessMsg_Handler();
    public delegate void FailedMsg_Handler(string text);
    public interface IController
    {
        // Eventos
        event SuccessMsg_Handler OnSuccessMessage;
        event FailedMsg_Handler OnFailedMessage;
        event Action<bool> OnAudioInfoStatus;
        event Action OnStoppedNarration;

        // Metodos
        void LoadPDF(string path);
        void BeginNarration(object sender, EventArgs e);
        void EndNarration(object sender, EventArgs e);
        void ExitApp();
    }
}
