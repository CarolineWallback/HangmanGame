﻿using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace HangmanGame
{
    internal class Program
    {
        static Random random = new Random();
        static string[] words = new string[10] { "bubblegum", "kiwifruit", "keyhole", "espionage", "microwave", "peekaboo", "nightclub", "unknown", "jukebox", "mystery" };
        static int guesses = 10;

        static void Main(string[] args)
        {
            string chosenWord = words[random.Next(0, words.Length)];
            char [] word = new char[chosenWord.Length];
            StringBuilder guessedLetters = new StringBuilder();

            

            for (int i = 0; i < word.Length; i++)
            {
                word[i] = '_';
            }

            Console.WriteLine("HANGMAN GAME\n");
            Console.WriteLine("I'm thinking of a word, you have 10 guesses to figure out which word.\n");

            foreach(var c in word)
                Console.Write(c + " ");

            Console.WriteLine("\n");
            while(word.Contains('_') && guesses > 0)
            {
                Console.WriteLine("Guess any letter or guess the whole word");

                string letter = "";
                while (!Regex.IsMatch(letter, "[A-Za-z]"))
                {
                    letter = (Console.ReadLine().ToLower());
                    if (!Regex.IsMatch(letter, "[a-z]"))
                        Console.WriteLine("You can only use letters A-Z");
                }

                if(letter.Length > 1)
                {
                    if(letter == chosenWord)
                    {
                        Console.WriteLine($"\nYou guessed the word! It was {chosenWord}. You won!");
                        Console.WriteLine("\nPress any key to quit.");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("You guessed wrong.\n");
                        guesses--;
                    }
                    
                }
                else
                {
                    if (guessedLetters.ToString().Contains(letter))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You have already guessed on this letter, try again");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    else
                    {
                        guessedLetters.Append(letter + " ");
                        if (chosenWord.Contains(letter))
                        {
                            var indexes = Regex.Matches(chosenWord, letter).Cast<Match>().Select(m => m.Index).ToList();
                            foreach (var i in indexes)
                            {
                                word[i] = char.Parse(letter);
                            }

                            Console.WriteLine("You guessed right.");
                        }
                        else
                        {
                            Console.WriteLine("You guessed wrong.");
                            guesses--;
                        }

                        foreach (var c in word)
                            Console.Write(c + " ");

                    }
                
                    Console.WriteLine("\n\nYour guessed letters are: ");
                    Console.WriteLine(guessedLetters);
                    Console.WriteLine($"\nTries left: {guesses}");
                    Console.WriteLine("--------------------\n");
                }
            }

            if (!word.Contains('_'))
                Console.WriteLine($"\nYou guessed the word! It was {chosenWord}. You won!");

            else if(guesses == 0)
            {
                Console.WriteLine("\nYou have consumed your 10 guesses. You lose!");
                Console.WriteLine($"The word was: {chosenWord}");
            }

            Console.WriteLine("\nPress any key to quit.");
            Console.ReadKey();
            Environment.Exit(0);


        }
    }
}