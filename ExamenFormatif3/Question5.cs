using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenFormatif3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] position = new bool[100];
            Random random = new Random();
            byte playerPosition = 0;
            int essai = 0;
            string command = "";
        
            position[0] = true;
            for (byte t = 1; t <= 98; t++)
            {
                if (random.Next(1, 3) == 1)
                    position[t] = false;
                else
                    position[t] = true;
            }
            position[99] = true;

            while (playerPosition != 99)
            {
                Console.WriteLine("Position présente : " + playerPosition + " / 99 \n");
                Console.WriteLine("Écrivez «A» pour reculer de 3 cases");
                Console.WriteLine("Écrivez «S» pour reculer de 2 cases");
                Console.WriteLine("Écrivez «D» pour reculer de 1 cases");
                Console.WriteLine("Écrivez «G» pour avancer de 2 cases");
                Console.WriteLine("Écrivez «H» pour avancer de 4 cases \n");

                Console.WriteLine("Écrivez «Q» pour quitter le jeu");
                Console.WriteLine("Écrivez «Y» pour voir tout le tableau");
                Console.WriteLine("Écrivez «P» pour voir les 10 prochaines cases");

                Console.Write("Insérez la commande ici : ");
                command = Console.ReadLine();
                Console.Clear();

                if (command.ToUpper() == "A")
                    playerPosition -= 3;
                else if (command.ToUpper() == "S")
                    playerPosition -= 2;
                else if (command.ToUpper() == "D")
                    playerPosition -= 1;
                else if (command.ToUpper() == "G")
                    playerPosition += 2;
                else if (command.ToUpper() == "H")
                    playerPosition += 4;
                #region AffichageEntier()
                else if (command.ToUpper() == "Y")
                {
                    for (byte t = 0; t < 100; t++)
                    {
                        if (playerPosition == t)
                            Console.Write("==> ");
                        else
                            Console.Write("    ");

                        Console.WriteLine(t + "- " + position[t]);
                    }
                    Console.WriteLine();
                }
                #endregion
                #region Affichage10()
                else if (command.ToUpper() == "P")
                {
                    for (byte t = 1; t <= 10; t++)
                    {
                        if (playerPosition + t < 100)
                            Console.WriteLine((playerPosition + t) + "- " + position[playerPosition + t]);
                    }
                    Console.WriteLine();
                }
                #endregion
                else if (command.ToUpper() == "Q")
                    playerPosition = 99;
                if (playerPosition > 99)
                    playerPosition = 99;
                #region GameOver
                if (position[playerPosition] == false)
                {
                    essai += 1;
                    playerPosition = 0;
                    for (byte t = 1; t <= 98; t++)
                    {
                        if (random.Next(1, 3) == 1)
                            position[t] = false;
                        else
                            position[t] = true;
                    }
                }

                if (playerPosition < 95)
                    if (position[playerPosition + 1] == false && position[playerPosition + 2] == false && position[playerPosition + 3] == false && position[playerPosition + 4] == false)
                        for (byte t = 1; t <= 98; t++)
                        {
                            if (random.Next(1, 3) == 1)
                                position[t] = false;
                            else
                                position[t] = true;
                        }
                #endregion
            }

            if (command.ToUpper() != "Q")
            {
                Console.WriteLine("Vous avez essayé " + essai + " fois avant d'atteindre la case 99. \n");
                Console.Write("Appuyez sur ENTER pour quitter.");
            }
        }
    }
}
