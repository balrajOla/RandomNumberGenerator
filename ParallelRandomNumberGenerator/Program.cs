﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ParallelRandomNumberGenerator
{
    class Program
    {
        /// <summary>
        /// Main program execution starts here
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            #region Variables
            // Fetch User Inputs for 
            //N - Number of Threads
            int numberOfThreads;

            //M - Maximum Random Number Generated
            int maxNumberOfValuesGenerated;

            //X - Lowest Number Generated by Random
            int lowestNumberGenerated;

            //Y - Highest Number Generated by Random
            int highestNumberGenerated;
            #endregion Variables 

            #region Fetch Data From User
            Console.WriteLine("Enter the number of threads:");
            int.TryParse(Console.ReadLine(), out numberOfThreads);

            Console.WriteLine("Enter maximum number of Random generated:");
            int.TryParse(Console.ReadLine(), out maxNumberOfValuesGenerated);

            Console.WriteLine("Enter Lowest possible number:");
            int.TryParse(Console.ReadLine(), out lowestNumberGenerated);

            Console.WriteLine("Enter maximum possible number:");
            int.TryParse(Console.ReadLine(), out highestNumberGenerated);
            #endregion Fetch Data From User

            #region Perform Task generate Random Numbers and Display Output
            // Parallel for loop will initiate tasks to generate random numbers 
            // based on No. of cores and Max number of threads aviable for the tasks
            // will assign operation intelligently to each task
            Parallel.For(1, maxNumberOfValuesGenerated, new ParallelOptions() { MaxDegreeOfParallelism = numberOfThreads }, value => 
            {
                // generate a number
                int number = ThreadSafeRandomGenerator.GenerateWithInBounds(lowestNumberGenerated, highestNumberGenerated);
                int threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    
                // dispatch on UI thread to display
                PrintValues(number, threadId);
            });
            #endregion Perform Task generate Random Numbers and Display Output

            Console.ReadKey();
        }

        /// <summary>
        /// Prints the result and threadId
        /// </summary>
        /// <param name="result">result obtained</param>
        /// <param name="threadId">Thread Id the result is obtained on</param>
        static private void PrintValues(int result, int threadId)
        {
            Console.WriteLine("The value generated: {0} , on ThreadId: {1} ", result, threadId);
        }
    }
}
