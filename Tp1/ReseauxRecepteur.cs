using System;
using System.Collections.Generic;
using System.IO;

namespace Tp1
{
    class ReseauxRecepteur
    {
        List<Frame> message;
        string outputPath;

        public ReseauxRecepteur(string outputPath)
        {
            message = new List<Frame>();
            this.outputPath = outputPath;
        }

        public void GiveNewFrame(Frame newFrame)
        {
            if(newFrame.type == Type.Fin)
            {
                SignalEndOfTransmission();
            } else {
                Console.WriteLine("Transmission de la trame " + newFrame.FrameId + " à la couche réseau.");
                message.Add(newFrame);
            }
        }

        private void SignalEndOfTransmission()
        {
            Console.WriteLine("Fin de la transmition.");
            string result = string.Empty;
            foreach (Frame entry in message)
            {
                result += Hamming.Decode(entry.Message);
            }
            Console.WriteLine("Écriture au fichier de sortie.");
            File.WriteAllText(outputPath, result);
        }
    }
}
