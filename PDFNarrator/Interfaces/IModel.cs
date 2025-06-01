using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFNarrator.Interfaces
{
    // Delegates
    public delegate void SendPDFdata_Handler(string data);
    public delegate void AudioData_Handler(string audio_data);
    public interface IModel
    {
        // Eventos
        event SendPDFdata_Handler OnSendPDFData;
        event AudioData_Handler OnAudioData;
        event Action OnExitApp;

        // Metodos
        int LoadPDFFile(string path);
        int ExtractText(string path);
        int StartAudioSynthesis();
        void StopAudioSynthesis();
        void GetPDFData(string data);
        void CloseInterface();

        void setView(IView v);
        void setupEvents();
    }
}
