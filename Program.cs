using System;
using System.IO;
using AeroCalcCore;

namespace CLtool
{


    class Program { 


        static void Main(string[] args) {
        
            AeroCalcCommandProcessor CommandProcessor = new AeroCalcCommandProcessor("config" + Path.DirectorySeparatorChar + "configuration.xml");
            AeroCalcCommand Command;
            bool run = true;

            // Initialisation & Accueil
            Command = CommandProcessor.process(AeroCalcCommand.CMD_WORD_INIT_INTERPRETER);
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
                Console.Write("<>");

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
