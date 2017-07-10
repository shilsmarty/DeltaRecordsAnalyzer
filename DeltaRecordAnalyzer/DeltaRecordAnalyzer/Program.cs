using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DeltaRecordAnalyzer
{
    /// <summary>
    /// Delta Records Analyzer using Hashset O(1) performance + object hashing GetHashCode() magic.
    /// </summary>
    public class Program
    {
        static readonly Random _random = new Random();

        public static void Main(string[] args)
        {
            int times = 10000000;

            HashSet<ItemInventory> hashsetA = new HashSet<ItemInventory>();
            HashSet<ItemInventory> hashsetB;
            string[] itemNameList =
            {
                "Paper", "Bag", "Pen", "Sharpner", "Pencil", "Rubber", "Book", "Uniform",
                "TiffinBox"
            };

            Console.WriteLine(" Starting the delta records analyzer test tool. ");

            Console.WriteLine(" Total load of records in this test is : "+ times);

            Stopwatch timer = new Stopwatch();
            timer.Start();
            for (int i = 0; i < times; i++)
            {
                hashsetA.Add(new ItemInventory()
                {
                    ItemCode = string.Concat(GetLetter(), _random.Next(times)),
                    ItemName = itemNameList[_random.Next(itemNameList.Length)],
                    ItemCost = _random.Next(),
                });

                if (i == 1000)
                    hashsetA.Add(new ItemInventory()
                    {
                        ItemCode = "HashsetA - TestCode",
                        ItemName = "HashsetA - TestName",
                        ItemCost = _random.Next(),
                    });
            }

            hashsetB =  new HashSet<ItemInventory>(hashsetA);

            var itemInventories = hashsetB.ToList();

            //Updation of an existing element in hashsetB.
            //------------------------------------------------------
            var deltaIndex = _random.Next(times);
            var addingDeltaChanges = new ItemInventory()
            {
                ItemCode = "HashSetB - ChangedCode",
                ItemName = "HashSetB - ChangedName",
                ItemCost = 50,
            };
            itemInventories[deltaIndex] = addingDeltaChanges;

            var addingChanges = new ItemInventory()
            {
                ItemCode = "HashsetB - TestCode",
                ItemName = "HashsetB - TestName",
                ItemCost = deltaIndex,
            };
            itemInventories[1001] = addingChanges;

            //Adding new element in HashSetB
            //-----------------------------------------------------
            itemInventories.Add(new ItemInventory()
            {
                ItemCode = "HashSetB - NewFirstCode",
                ItemName = "HashSetB - NewFirstName",
                ItemCost = 100
            });

            itemInventories.Add(new ItemInventory()
            {
                ItemCode = "HashSetB - NewSecondCode",
                ItemName = "HashSetB - NewSecondName",
                ItemCost = 100
            });

            //Adding new element in HashSetA
            //-----------------------------------------------------
            hashsetA.Add(new ItemInventory()
            {
                ItemCode = "HashSetA - NewCode",
                ItemName = "HashSetA - NewName",
                ItemCost = 100
            });

            hashsetB = new HashSet<ItemInventory>(itemInventories);

            //Magic is happening here.
            hashsetA.SymmetricExceptWith(hashsetB);

            timer.Stop();

            Console.WriteLine(" Identified records which are delta in the 2 hashset : " + hashsetA.Count + Environment.NewLine);

            //Display the delta records.
            Console.WriteLine("Different Records : " + Environment.NewLine);

            foreach (var deltaRow in hashsetA)
            {
                Console.WriteLine(deltaRow.ItemCode + " , " + deltaRow.ItemName + " , " + deltaRow.ItemCost + " , " + Environment.NewLine);
            }

            Console.WriteLine(timer.Elapsed.Seconds  + " total time elapsed in seconds.");

            Console.ReadLine();
        }

        public static char GetLetter()
        {
            // This method returns a random lowercase letter.
            // ... Between 'a' and 'z' inclusize.
            int num = _random.Next(0, 26); // Zero to 25
            return (char) ('A' + num);
        }
    }
}
