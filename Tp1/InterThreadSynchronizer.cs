namespace Tp1
{
    public class InterThreadSynchronizer
    {
        private  Frame transmitterFrameToPS;
        private  Frame PSFrameToReceiver;
        private  bool readyToSendSource = true;
        private  bool receivedFromDestination = false;

        private Frame receiverFrameToPS;
        private Frame PSFrameToTransmitter;
        private bool readyToSendDestination= true;
        private bool receivedFromSource = false;

        public bool TransferTrameToSupportSource(Frame frame)
        {
            if (readyToSendSource){
                transmitterFrameToPS = frame;
                readyToSendSource = false;
                return true;
            }   else
            {
                return false;
            }
        }

        public bool TransferTrameToSupportDestination(Frame frame)
        {
            if (readyToSendDestination)
            {
                receiverFrameToPS = frame;
                readyToSendDestination = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TransferTrameToDestination(ref bool insertError)
        {
            if (!readyToSendSource && !receivedFromDestination)
            {
                Frame f = new Frame();
                f.Message = (char[])transmitterFrameToPS.Message.Clone();
                f.type = transmitterFrameToPS.type;
                f.FrameId = transmitterFrameToPS.FrameId;
                PSFrameToReceiver = f;
                if (insertError)
                {
                    Logger.WriteMessage("Trame sans erreur: ");
                    Logger.WriteMessage(PSFrameToReceiver.Message);
                    Logger.WriteMessage("");
                    int nbError = Logger.ReadInt("Combien d'erreurs souhaitez-vous insérer?");
                    if (nbError > 0)
                    {
                        bool rep = Logger.ReadStringChoice("Voulez-vous inserer manuellement?");

                        if (rep)
                        {
                            //Manual errors insertion
                            int max_pos = PSFrameToReceiver.Message.Length - 1;
                            for (int i = 0; i < nbError; ++i)
                            {
                                int pos = Logger.ReadIntInterval("A quel position voulez-vous inserer?", 0, max_pos);
                                Util.InjectErrorAtPosition(ref PSFrameToReceiver.Message, pos);
                            }
                        }
                        else
                        {
                            //Random errors insertion
                            for (int i = 0; i < nbError; ++i)
                                Util.InjectErrorRandom(ref PSFrameToReceiver.Message);
                        }
                    }                    
                    insertError = false;
                    Logger.WriteMessage("Trame avec erreur: ");
                    Logger.WriteMessage(PSFrameToReceiver.Message);
                    Logger.WriteMessage("");
                }
                receivedFromDestination = true;
                readyToSendSource = true;
            }
        }

        public void TransferTrameToSource()
        {
            if (!readyToSendDestination && !receivedFromSource)
            {
                PSFrameToTransmitter = receiverFrameToPS;
                receivedFromSource = true;
                readyToSendDestination = true;
            }
        }

        public bool ReadyToReadSourceMessage()
        {
            return receivedFromDestination;
        }

        public bool ReadyToReadDestinationMessage()
        {
            return receivedFromSource;
        }

        public Frame GetMessageFromSource()
        {
            receivedFromDestination = false;
            return PSFrameToReceiver;
        }

        public Frame GetMessageFromDestination()
        {
            receivedFromSource = false;
            return PSFrameToTransmitter;
        }
    }
}
