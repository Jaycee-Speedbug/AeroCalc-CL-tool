using System;
using System.IO;
using AeroCalcCore;

namespace CLtool
{


    class Program { 


        static void Main(string[] args) {
        
            AeroCalcCommandProcessor CommandProcessor = new AeroCalcCommandProcessor();
            AeroCalcCommand Command;
            bool run = true;
            string cmdLineOptions = " ";

            // TODO: Provisoire, améliorer la prise en charge des options de ligne de commande
            if  (args.Length == 0) {
                // Default configuration file path
                cmdLineOptions = "config" + Path.DirectorySeparatorChar + "config.xml";
            }
            else {
                foreach (string str in args)
                {
                    cmdLineOptions += str + " ";
                }
            }

            // Initialisation & Accueil
            Command = CommandProcessor.process(AeroCalcCommand.CMD_WORD_INIT_INTERPRETER + cmdLineOptions);
            if (Command.eventCode == AeroCalcCommand.EVENTCODE_INIT_SUCCESSFULL) {
                // Initialisation réussie
                Console.WriteLine(Command.txtResult);
            }
            else {
                // Echec de l'initialisation
                Console.WriteLine("Exiting AirCalc...");
                DateTime instant = DateTime.Now;
                instant = instant.AddSeconds(2.5);
                while (DateTime.Now.Second < instant.Second) { }
                run = false;
            }

            // Boucle principale de l'interpréteur de commandes en ligne
            while (run) {

                // Prompt
                Console.Write("--o-Ô-o--");

                // Lecture de la console et transfert vers le processeur de commande
                Command = CommandProcessor.process(Console.ReadLine());

                // Traitement particulier de la commande EXIT
                if (Command.action == AeroCalcCommand.ACTION_EXIT)
                {
                    Console.WriteLine("Exiting AirCalc...");
                    DateTime instant = DateTime.Now;
                    instant = instant.AddSeconds(2.5);
                    while (DateTime.Now.Second < instant.Second) { }
                    break;
                }

                // Affichage du résultat des commandes
                Console.WriteLine(Command.txtResult);
                if (Command.eventCode <= AeroCalcCommand.EVENTCODE_INIT_VALUE)
                {
                    // Echec du traitement de la commande, ajout du commentaire en seconde ligne
                    Console.WriteLine(Command.txtComment);
                }

            }

        } // Main()
    
    }
    
}
