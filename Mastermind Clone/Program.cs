using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind_Clone
{
    class Program
    {
        static void Main(string[] args)
        {
            //Can't assume that the user knows how to play.
            Console.WriteLine("Welcome to MostorMond: A Mastermind Clone.\n\n" +
                "Your goal is to decipher a four digit code comprising of numbers 1-6.\n" +
                "For each round, you can enter four numbers.\n" +
                "Then, the game will let you know how close you are to guessing the code.\n" +
                "Digits that appear in the code in the correct location will return +.\n" +
                "Digits that appear in the code in the correct location will return -.\n\n" +
                "You have ten turns to try to guess the code.\n\n" +
                "You may exit the game at any time by entering \"Quit.\"\n\n" +
                "Press Enter when you are ready. Good luck.");

            while (Console.ReadKey().Key != ConsoleKey.Enter);

            Boolean inGame = true;

            while (inGame)
            {
                Console.Clear();
                playGame(ref inGame);
            }

            Console.Clear();
            Console.WriteLine("\nThanks for playing. Press Enter to quit.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;

        }

        private static void playGame(ref bool inGame)
        {
            char[] code = new char[4];

            //Generate the code to break this round.
            Random RNG = new Random();
            for (int i = 0; i < 4; i++)
            {
                //Making it a char array here means we can just work with the guess as a string, and it makes outputting the final code simple
                code[i] = RNG.Next(1, 7).ToString()[0];
            }

            //Flag here for returning either a winning or lose/quit message.
            bool win = false;

            for(int turn = 1; turn <= 10; turn++)
            {
                Console.WriteLine(String.Concat("\nTurn ", turn, " out of 10"));

                string guess = Console.ReadLine();

                if (guess == "Dev Cheat")
                {
                    Console.Write("The code: ");
                    Console.WriteLine(new string(code));
                }

                //Don't do anything if the user wants to quit.
                if (guess.ToLower() == "quit") { break; };
                    
                //Quite a bit going on here, but we're making guess only comprise of the
                //read characters ranging from 0 - 6, with 0 at the end in case this is too short.
                //Since the game will never generate a digit below 1, 0 is a safe placeholder.
                guess = String.Concat(
                    new String(
                                (from char c in guess
                                 where char.IsNumber(c) && char.GetNumericValue(c) <= 6
                                 select c).ToArray()
                                    ), "0000");

                Console.WriteLine(String.Concat("Testing against \n", guess.Substring(0, 4)));

                byte cor = 0;

                for (int i = 0; i < 4; i++)
                {
                    if(code.Contains(guess[i]))
                    {
                        if(code[i] == guess[i])
                        {
                            Console.Write("+");
                            cor++;
                        }
                        else
                        {
                            Console.Write("-");
                        }
                    }
                    else
                    {
                        Console.Write(" ");
                    }


                }
                Console.WriteLine(String.Concat("\n",cor, " matches found.\n"));

                if(cor == 4)
                {
                    win = true;
                    break;
                }
            }

            Console.WriteLine(String.Concat("The code: ", new String(code)));

            Console.WriteLine();

            //Can't really give more than a congrats here since what happens next is the same.
            Console.WriteLine(win ? "Congratulations!\nYou win all the money!" : "Round over!\nBetter luck next time..");

            //Give the user the option to play again. Only care about their input they enter Y/N.
            //We could easily change this to not use a loop and just default an outcome if the user tries to type randomly.
            Console.WriteLine("\nPlay Again? (y/n)\n\n");
            string choice = Console.ReadKey().Key.ToString().ToLower();
            while (choice != "y" && choice != "n") { choice = Console.ReadKey().Key.ToString().ToLower(); }

            //Set the flag based off of what the user picked. This will return back to the loop in the driver.
            inGame = choice == "y";
        }
    }
}
