using System.Collections.Generic;

namespace Tp1
{
    /// <summary>
    /// Class that represents the reciever on a machine.
    /// </summary>
    class Receiver
    {
        InterThreadSynchronizer synchronizer;
        List<Frame> framesList;
        NetworkReceiver networkLayer;
        int lastAck = -1;
        int bufferSize;


        public Receiver(NetworkReceiver networkLayer, InterThreadSynchronizer synchronizer, int bufferSize)
        {
            this.networkLayer = networkLayer;
            this.synchronizer = synchronizer;
            this.bufferSize = bufferSize;

            framesList = new List<Frame>();
        }

        /// <summary>
        /// Delagate thread fonction that recieve the frame, if it's available, check for validity and send the message back. When the message is complete,
        /// writes the frame to the file.
        /// </summary>
        public void Receiving()
        {
            while (true)
            {
                waitForFrame();
                Frame frame = synchronizer.GetMessageFromSource();
                if (frame.type == Type.Fin)
                {
                    networkLayer.GiveNewFrame(frame);
                }
                else
                {
                    bool isValid = Hamming.Validate(ref frame.Message);

                    if (!isValid)
                    {
                        Logger.WriteMessage("NAK envoyé pour la trame " + frame.FrameId.ToString());
                        SendToSource(frame.FrameId, Type.Nak);
                    }
                    else
                    {
                        if (lastAck + 1 != frame.FrameId)
                        {
                            framesList.Add(frame);
                        }
                        else
                        {
                            networkLayer.GiveNewFrame(frame);

                            int size = framesList.Count;
                            while (framesList.Count != 0)
                            {
                                if (Hamming.Validate(ref framesList[0].Message))
                                {
                                    networkLayer.GiveNewFrame(framesList[0]);
                                    framesList.RemoveAt(0);
                                }
                                else
                                {
                                    Logger.WriteMessage("NAK envoyé pour la trame " + framesList[0].FrameId.ToString());
                                    SendToSource(framesList[0].FrameId, Type.Nak);
                                }
                            }
                            Logger.WriteMessage("ACK envoyé pour la trame " + frame.FrameId.ToString());
                            SendToSource(frame.FrameId + size - framesList.Count, Type.Ack);

                            lastAck = frame.FrameId + size - framesList.Count;
                        }
                    }
                }
            }
        }

        private void waitForFrame()
        {
            while (!synchronizer.ReadyToReadSourceMessage()) ;
        }

        private void SendToSource(int numSeq, Type type)
        {
            Frame frame = new Tp1.Frame();
            frame.FrameId = numSeq;
            frame.type = type;
            while (!synchronizer.TransferTrameToSupportDestination(frame)) ;
        }
    }
}