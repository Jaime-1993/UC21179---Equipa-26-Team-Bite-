using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFNarrator.Interfaces
{
    public interface IModel
    {
        // Eventos
        event Model.SendPDFdata_Handler OnSendPDFData;
        event Model.AudioData_Handler OnAudioData;
        event Action OnExitApp;

        // Metodos
        int LoadPDFFile(string path);
        int ExtractText(string path);
        int StartAudioSynthesis();
        void StopAudioSynthesis();
        void GetPDFData(string data);
        void CloseInterface();

        void setView(View v);
        void setupEvents();
    }
}
