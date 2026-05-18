using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace ChatbotGUI
{
    internal class GreetingVoiceSpeech
    {
        SpeechSynthesizer speech = new SpeechSynthesizer();

        public void Speak(string message)
        {
            speech.SpeakAsync(message);
        }

    }
}
