using System;
using System.Collections;
using System.Collections.Generic;

namespace AiGuseeConsole
{
    class Program
    {
        // allCombination - Array taht include all the numbers in the game
        static int[] allCombination = getAllCombination();
        // allGuess - All the guesses that the computer guessed
        static int[] allGuess = new int[10];
        // responseToGuess - An array of pairs that contains all 
        //the user's responses according to the computer's guess
        static Tuple<int, int>[] responseToGuess = new Tuple<int, int>[10];
        
        static void Main(string[] args)
        {
            // i - The current index of number to check if 
            // the computer's guess is can be the next guess
            int i = 0;
            // index - The index of the current guess
            int index = 0;
            // This do while use to search number to computer's guess 
            do
            {
                bool isFoundCombin = false;
                
                // This for loop stops if he finds a suitable number to guess
                // and the next time he guesses he starts from the same place he stopped (from i)
                for (; i < allCombination.Length; i++)
                {
                    if (IsMatchCombination(allCombination[i]))
                    {
                        allGuess[index] = allCombination[i];
                        isFoundCombin = true;
                        break;
                    }
                }

                if (!isFoundCombin)
                {
                    Console.WriteLine("Can't find match");
                    Console.WriteLine("Press enter to close...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                Console.WriteLine("My " +( index + 1)  +" guess is: " + allGuess[index]);

                Console.WriteLine("Correct location:(black)");
                int b = int.Parse(Console.ReadLine());
                Console.WriteLine("Right number in wrong location:(white)");
                int w = int.Parse(Console.ReadLine());
                

                responseToGuess[index] = new Tuple<int, int>(b, w);
            }
            // Item1 is the correct location (black)
            // and if is not 4 (ie the computer not managed to guess) then continue to search
            while (responseToGuess[index++].Item1 != 4);

            Console.WriteLine("I managed to guess at " + (index) + " guesses");
            Console.WriteLine("Press enter to close...");
            Console.ReadKey();
            Environment.Exit(0);

        }
         static Tuple<int, int> compareTwoGuesses(int num1, int num2)
        {
            // Gets 2 numbers and returns a pair representing the number of correct positions 
            // between them (black) and those in the wrong places (white)
            
            int correctLocationB = 0, notCorrectLocationW = 0;
            int[] arr1 = Int_to_array(num1);
            int[] arr2 = Int_to_array(num2);

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == arr2[i])
                    correctLocationB++;
                // Array.Exists - Returns true if array contains one or more elements that match the 
                // conditions defined by the specified predicate; otherwise, false.
                else if (Array.Exists(arr2, element => element == arr1[i]))
                    notCorrectLocationW++;
                }

            return new Tuple<int,int>(correctLocationB,notCorrectLocationW);
        }  
        static int[] Int_to_array(int n)
        {
            int j = 0;
            int len = n.ToString().Length;
            int[] arr = new int[len];
            while (n != 0)
            {
                arr[len - j - 1] = n % 10;
                n /= 10;
                j++;
            }
            return arr;
        }

        static int[] getAllCombination() {
            //The function returns an array of all 4-digit 
            //numbers that include the digits between 1 and 6 without repeats
            List<int> tempList = new List<int>();
            for (int i = 1234; i <= 6543; i++)
                if (existInGame(i))
                    tempList.Add(i);

            return  tempList.ToArray();
        }

        private static bool existInGame(int num)
        {
            //The function receives a 4-digit number
            //and returns whether it is in the game, 
            //ie whether it is between the numbers 1 to 6 without repetitions.

            ArrayList listOfDigitExist = new ArrayList() {1,2,3,4,5,6};

            while (num > 0)
            {
                listOfDigitExist.Remove(num % 10);
                num/= 10;
            }

            return listOfDigitExist.Count == 2;
        }

        static bool IsMatchCombination(int currentComb)
        {
            // Gets a number and returns true whether it is possible to be the next guess. 
            // Check by comparison with all previous guesses
            for (int i = 0; allGuess[i] != 0 && i < allGuess.Length; i++)
            {
                if (!compareTwoGuesses(allGuess[i], currentComb).Equals(responseToGuess[i]))
                    return false;
            }

            return true;
        }                               
    }
}
