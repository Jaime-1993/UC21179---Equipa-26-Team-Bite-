using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFNarrator.Interfaces
{
    // Delegates
    public delegate void LoadPDFpath_Handler(string path);
    public delegate void GetPDFdata_Handler(string data);
    public delegate void SyncAudioData_Handler();

    public interface IView
    {
        // Eventos
        event LoadPDFpath_Handler OnLoadPDF;
        event EventHandler OnStartNarration;
        event EventHandler OnStopNarration;
        event Action OnExitApp;
        event GetPDFdata_Handler OnGetPDFData;
        event SyncAudioData_Handler OnSyncAudioData;

        // Metodos
        void ReceivePDFData(string extractedText);
        void AudioDataStatus(bool isAudioDataAvailable);
        void Narration_Stopped();
        void CreateInterface();
        void AnimateButtons();
    }
}
