using System;
using System.Collections;
using System.Collections.Generic;

namespace AiGuseeConsole
{
    class Program
    {
        static  int[] allCombination = getAllCombination();
        static int[] allGuess = new int[10];
        static Tuple<int, int>[] responseToGuess = new Tuple<int, int>[10];
        static void Main(string[] args)
        {

            int i;
            int index = 0;
            do
            {
                bool isFoundCombin = false;
                i = 0;
                for (; !isFoundCombin && i < allCombination.Length; i++)
                {
                    if (IsMatchCombination(allCombination[i]))
                    {
                        allGuess[index] = allCombination[i];
                        isFoundCombin = true;
                    }
                }

                if (!isFoundCombin)
                {
                    Console.WriteLine("Can't find match");
                    Console.WriteLine("Press enter to close...");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                Console.WriteLine("My " +( index + 1)  +" guess is: " + allCombination[i - 1]);

                Console.WriteLine("Correct location:(black)");
                int b = int.Parse(Console.ReadLine());
                Console.WriteLine("Right number in wrong location:(white)");
                int w = int.Parse(Console.ReadLine());


                responseToGuess[index] = new Tuple<int, int>(b, w);
            }
            while (responseToGuess[index++].Item1 != 4);

            Console.WriteLine("I managed to guess at " + (index) + " guesses");
            Console.WriteLine("Press enter to close...");
            Console.ReadKey();
            Environment.Exit(0);

        }
         static Tuple<int, int> compareTwoGuesses(int num1, int num2)
        {
            int correctLocationB = 0, notCorrectLocationW = 0;
            int[] arr1 = Int_to_array(num1);
            int[] arr2 = Int_to_array(num2);

            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr1[i] == arr2[i])
                    correctLocationB++;
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
            List<int> tempList = new List<int>();
            for (int i = 1234; i <= 6543; i++)
                if (existInGame(i))
                    tempList.Add(i);

            return  tempList.ToArray();
        }

        private static bool existInGame(int num)
        {
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

            for (int i = 0; allGuess[i] != 0 && i < allGuess.Length; i++)
            {
                if (!compareTwoGuesses(allGuess[i], currentComb).Equals(responseToGuess[i]))
                    return false;
            }

            return true;
        }                               
    }
}
