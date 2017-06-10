using System;
namespace Tp1
{
    public class InterThreadSynchronizer
    {
        private  Frame envoieSource;
        private  Frame receptionDestionation;
        private  bool pretEmmetreSource = true;
        private  bool recuDestination = false;

        private Frame envoieDestination;
        private Frame receptionSource;
        private bool pretEmmetreDestination= true;
        private bool recuSource = false;

        ///<Summary> 
        ///Transfer the current Frame to the support.
        ///</Summary>
        ///<returns> False if the support is not ready and the Frame wasn't transfered and True if the Frame was transfered.</returns>
        public bool TransferTrameToSupportSource(Frame trame)
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
        ///Transfer the current Frame to the support.
        ///</Summary>
        ///<returns> False if the support is not ready and the Frame wasn't transfered and True if the Frame was transfered.</returns>
        public bool TransferTrameToSupportDestination(Frame trame)
        {
            if (pretEmmetreDestination)
            {
                envoieDestination = trame;
                pretEmmetreDestination = false;
                return true;
            }
            else
            {
                return false;
            }
        }
        ///<Summary> 
        ///Transfer the current Frame from one machine to the other.
        ///</Summary>
        ///<returns> False if the transfer wasn't reay and the Frame wasn't transfered and True if the Frame was transfered.</returns>
        public void TransferTrameToDestination(bool insertError)
        {
            if (!pretEmmetreSource && !recuDestination)
            {
                if (insertError)
                {
                    Console.WriteLine("Tramme actuelle: ");
                    Console.WriteLine(envoieSource.Message);
                    Console.WriteLine("Combien d'erreur souhaitez-vous insérer? ");
                    int nbError = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Voulez-vous inserer aléatoirement ou manuellement? (y/n)");
                    string r = Console.ReadLine();

                    while (r != "y" && r != "n")
                        r = Console.ReadLine();

                    if (r == "y")
                    {
                        for (int i = 0; i < nbError; ++i)
                        {
                            Util.InjectErrorRandom(ref envoieSource.Message);
                        }
                    }
                    else
                    {

                    }                   
                    


                }
                    
                receptionDestionation = envoieSource;
                recuDestination = true;
                pretEmmetreSource = true;
            }
        }

        ///<Summary> 
        ///Transfer the current Frame from one machine to the other.
        ///</Summary>
        ///<returns> False if the transfer wasn't reay and the Frame wasn't transfered and True if the Frame was transfered.</returns>
        public void TransferTrameToSource()
        {
            if (!pretEmmetreDestination && !recuSource)
            {
                receptionSource = envoieDestination;
                recuSource = true;
                pretEmmetreDestination = true;
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
        ///Check to see if there is something to be recieved.
        ///</Summary>
        ///<returns> False there is nothing and True if we can read something.</returns>
        public bool ReadyToReadDestinationMessage()
        {
            return recuSource;
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

        ///<Summary> 
        ///Reads the Frame that was recieved.
        ///</Summary>
        ///<returns> The Frame that was recieved.</returns>
        public Frame GetMessageFromDestination()
        {
            recuSource = false;
            return receptionSource;
        }
    }
}
