using System;
using System.IO;
using AeroCalcCore;

namespace CLtool
{



    class Program
    {



        static void Main(string[] args)
        {
            AeroCalcCommandProcessor CommandProcessor = new AeroCalcCommandProcessor();
            AeroCalcCommand Command;
            bool run = true;
            string cmdLineOptions = " ";

            // TODO: Provisoire, améliorer la prise en charge des options de ligne de commande
            if (args.Length == 0)
            {
                // Default configuration file path
                cmdLineOptions += "config" + Path.DirectorySeparatorChar + "config.xml";
            }
            else
            {
                foreach (string str in args)
                {
                    cmdLineOptions += str + " ";
                }
            }

            // Première commande : Initialisation & Accueil
            Command = CommandProcessor.process(AeroCalcCommand.CMD_WORD_INIT_INTERPRETER + cmdLineOptions);

            // Boucle principale de l'interpréteur de commandes en ligne
            while (run)
            {
                // Affichage de la réponse du processeur
                Console.WriteLine(Command.txtResult);
                // Traitement particulier de la commande EXIT (User ou System)
                if (Command.isExit())
                {
                    DateTime instant = DateTime.Now;
                    instant = instant.AddSeconds(2.5);
                    while (DateTime.Now.Second < instant.Second) { }
                    break;
                }
                // Prompt & Lecture
                Console.Write("--o-Ô-o-- ");
                Command = CommandProcessor.process(Console.ReadLine());



                // * DEBUG
                /*
                Command = CommandProcessor.process("VERBOSE");
                Console.WriteLine(Command.txtResult);
                Command = CommandProcessor.process("LOAD MODEL *");
                Console.WriteLine(Command.txtResult);
                run = false;
                */
            }

        } // Main()

    }

}
