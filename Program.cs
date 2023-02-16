using DataStructures.Interfaces;
using DataStructures.QueueSamples;
using DataStructures.Sctructures;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;

namespace DataStructures
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dinamicArrays = new List<IArray<int>>() { new SingleArray<int>(), new VectorArray<int>(100) , new FactorArray<int>(2) , new MatrixArray<int>(100) };

            foreach(var array in dinamicArrays)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                for (int i = 0; i < 1_000_000; i++)
                {
                    array.Add(i);
                }
                stopwatch.Stop();
                Console.WriteLine($" Добавление в массив типа {array.GetType().Name} 1_000_000 элементов заняло {stopwatch.ElapsedMilliseconds} мс");
            }
        }
    }
}
