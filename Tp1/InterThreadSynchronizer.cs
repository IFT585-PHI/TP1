namespace Tp1
{
    public class InterThreadSynchronizer
    {
        private  Weft envoieSource;
        private  Weft receptionDestionation;
        private  bool pretEmmetreSource = true;
        private  bool recuDestination = false;

        private string envoieDestination;
        private bool isCodeNew;

        ///<Summary> 
        ///Transfer the current Weft to the support.
        ///</Summary>
        ///<returns> False if the support is not ready and the Weft wasn't transfered and True if the Weft was transfered.</returns>
        public  bool TransferTrameToSupport(Weft trame)
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
        ///Transfer the current Weft from one machine to the other.
        ///</Summary>
        ///<returns> False if the transfer wasn't reay and the Weft wasn't transfered and True if the Weft was transfered.</returns>
        public bool TransferTrameToDestination()
        {
            if (!pretEmmetreSource && !recuDestination)
            {
                receptionDestionation = envoieSource;
                recuDestination = true;
                pretEmmetreSource = true;
                return true;
            }
            else
            {
                return false;
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
        ///Reads the Weft that was recieved.
        ///</Summary>
        ///<returns> The Weft that was recieved.</returns>
        public Weft GetMessageFromSource()
        {
            recuDestination = false;
            return receptionDestionation;
        }



    }
}
