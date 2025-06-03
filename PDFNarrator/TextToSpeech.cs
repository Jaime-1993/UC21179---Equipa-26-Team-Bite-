using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace PDFNarrator
{
    public interface ITextToSpeech
    {
        void Play(string text);
        void Stop();
        void Configure(int rate, int volume, string voice);
    }

    public class TextToSpeech : ITextToSpeech
    {
        private SpeechSynthesizer synthesizer;
        public TextToSpeech()
        {
            synthesizer = new SpeechSynthesizer();
            Configure(  rate: 0,
                        volume: 100,
                        voice: "Microsoft Zira Desktop");
        }
        public void Configure(int rate, int volume, string voice)
        {
            synthesizer.Rate = rate;                               // Velocidade normal
            synthesizer.Volume = volume;                           // Volume máximo
            synthesizer.SelectVoice(voice);                        // Seleciona a voz padrão
        }
        public void Play(string text)
        {
            synthesizer.SpeakAsync(text);
        }
        
        public void Stop()
        {
            synthesizer.SpeakAsyncCancelAll();
        }
        public void Dispose()
        {
            synthesizer.Dispose();
        }
    }
}
