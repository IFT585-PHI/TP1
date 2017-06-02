namespace Tp1
{
    public class InterThreadSynchronizer
    {
        private  Weft envoieSource;
        private  Weft receptionDestionation;
        private  bool pretEmmetreSource = true;
        private  bool recuDestination = false;

        
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

        public bool ReadyToReadSourceMessage()
        {
            return recuDestination;
        }

        public Weft GetMessageFromSource()
        {
            recuDestination = false;
            return receptionDestionation;
        }



    }
}
