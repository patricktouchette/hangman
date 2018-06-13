//Hangman Game
//@author: Patrick Touchette

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playing = true;
            while (playing)
            {
                playing = MainMenu();
                if (playing == false) { Environment.Exit(0);  };
                StartGame();
            }
        }

        public static string[] HangManPics()
        {
            string[] pics = new string[7];
            pics[0] = @"
                      +---+
                      |   |
                          |
                          |
                          |
                          |
                    =========";
            pics[1] = @"
                      +---+
                      |   |
                      O   |
                          |
                          |
                          |
                    =========";
            pics[2] = @"
                      +---+
                      |   |
                      O   |
                      |   |
                          |
                          |
                    =========";
            pics[3] = @"
                      +---+
                      |   |
                      O   |
                     /|   |
                          |
                          |
                    =========";
            pics[4] = @"
                      +---+
                      |   |
                      O   |
                     /|\  |
                          |
                          |
                    =========";
            pics[5] = @"
                      +---+
                      |   |
                      O   |
                     /|\  |
                     /    |
                          |
                    =========";
            pics[6] = @"
                      +---+
                      |   |
                      O   |
                     /|\  |
                     / \  |
                          |
                    =========";

            return pics;
        }

        public static bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Hangman\n");
            string[] pics = HangManPics();
            Console.WriteLine(pics[0]);
            Console.WriteLine("\nDo you want to play?");
            Console.WriteLine("1: Start game");
            Console.WriteLine("2: Quit");

            string input = Console.ReadLine();
            if (input == "1")
            {
                return true;
            }
            if (input == "2")
            {
                return false;
            }
            else
            {
                Console.WriteLine("I'm sorry, please type 1 or 2");
                return true;
            }

        }

        public static string RandomWord()
        {
            //Create Words List
            string[] words = "ant baboon badger bat bear beaver camel cat clam cobra cougar coyote crow deer dog donkey duck eagle ferret fox frog goat goose hawk lion lizard llama mole monkey moose mouse mule newt otter owl panda parrot pigeon python rabbit ram rat raven rhino salmon seal shark sheep skunk sloth snake spider stork swan tiger toad trout turkey turtle weasel whale wolf wombat zebra".Split(' ');
            List<string> wordsList = words.ToList();
            Random randomGenerator = new Random();
            int r = randomGenerator.Next(wordsList.Count);
            string secretWord = wordsList[r];
            return secretWord;
        }

        public static void StartGame()
        {
            string[] pics = HangManPics();
            string secretWord = RandomWord();
            string missedLetters = "";
            string correctLetters = "";
            string alreadyGuessed = "";
            bool notGuessed = true;

            while(notGuessed)
            {
                //Check if we lost
                if (missedLetters.Length >= pics.Length)
                {
                    Console.WriteLine("YOU LOSE!!!");
                    Console.WriteLine("The word was: " + secretWord + "\n");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    break;
                }
                
                //Print the Display
                Display(pics, secretWord, missedLetters, correctLetters);

                //Check if we won
                bool foundAllLetters = true;
                foreach (char s in secretWord)
                {
                    if (correctLetters.Contains(s) == false)
                    {
                        foundAllLetters = false;
                    }
                }
                
                if (foundAllLetters == true)
                {
                    notGuessed = false;
                    Console.WriteLine("Congrats!! You found the word!!");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                    break;
                }

                //Get players guess
                string guess = GetGuess(alreadyGuessed);
                if (secretWord.Contains(guess))
                {
                    correctLetters += guess;
                }
                else missedLetters += guess;
                alreadyGuessed = correctLetters + missedLetters;
            }

        }

        public static void Display(string[] pics, string secretWord, string missedLetter, string correctLetters)
        {
            Console.Clear();
            Console.WriteLine("Hangman! \n");
            Console.WriteLine(pics[missedLetter.Length]);
            //Console.WriteLine(secretWord); //show word for testing
            Console.Write("    ");
            foreach (char s in secretWord) 
            {
                if (correctLetters.Contains(s))
                {
                    Console.Write(s + " ");
                }
                else
                {
                    Console.Write("_ ");
                }

            }

            Console.Write("\n\n   Missed Letters: ");
            foreach (char s in missedLetter)
            {
                Console.Write(s + " ");
            }

            Console.WriteLine();
            Console.WriteLine("\n");

        }

        public static string GetGuess(string alreadyGuessed)
        {
            while (true)
            {
                Console.WriteLine("Pick a letter");
                string guess = Console.ReadLine();
                try
                {
                    char c = guess.ToCharArray()[0];
                }
                catch
                {
                    Console.WriteLine("ERROR - Please enter a letter");
                    continue;
                }

                if (guess.Length > 1)
                {
                    Console.WriteLine("ERROR - Please enter a single letter");
                }
                else if (char.IsLetter(guess.ToCharArray()[0]) == false)
                {
                    Console.WriteLine("ERROR - Please enter a letter");
                }
                else if (alreadyGuessed.Contains(guess))
                {
                    Console.WriteLine("ERROR - You already guessed that, try again");
                }
                //else if
                else return guess.ToLower();
            }
        }

    } // end of Program Class

} // End of Hangman
