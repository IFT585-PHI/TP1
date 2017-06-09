namespace Tp1
{
    public class InterThreadSynchronizer
    {
        private  Frame envoieSource;
        private  Frame receptionDestionation;
        private  bool pretEmmetreSource = true;
        private  bool recuDestination = false;

        private string envoieDestination;
        private bool isCodeNew;

        ///<Summary> 
        ///Transfer the current Frame to the support.
        ///</Summary>
        ///<returns> False if the support is not ready and the Frame wasn't transfered and True if the Weft was transfered.</returns>
        public  bool TransferTrameToSupport(Frame trame)
        {
            if (pretEmmetreSource){
                envoieSource = trame;
                pretEmmetreSource = false;
                return true;
            }   else
            {
                return false;
            }
        }

        ///<Summary> 
        ///Transfer the current Frame from one machine to the other.
        ///</Summary>
        ///<returns> False if the transfer wasn't reay and the Frame wasn't transfered and True if the Weft was transfered.</returns>
        public void TransferTrameToDestination()
        {
            if (!pretEmmetreSource && !recuDestination)
            {
                receptionDestionation = envoieSource;
                recuDestination = true;
                pretEmmetreSource = true;
            }
        }

        ///<Summary>
        ///Check to see if there is something to be recieved.
        ///</Summary>
        ///<returns> False there is nothing and True if we can read something.</returns>
        public bool ReadyToReadSourceMessage()
        {
            return recuDestination;
        }

        ///<Summary> 
        ///Reads the Frame that was recieved.
        ///</Summary>
        ///<returns> The Frame that was recieved.</returns>
        public Frame GetMessageFromSource()
        {
            recuDestination = false;
            return receptionDestionation;
        }



    }
}
