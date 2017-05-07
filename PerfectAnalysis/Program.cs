using System;
using System.IO;
using System.Diagnostics;

namespace PerfectAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("三目並べ完全解析");
            var watch = new Stopwatch();
            var analyser = new SammokuAnalyzer();
            watch.Start();
            var res = analyser.Execute();
            Console.WriteLine(string.Format("所要時間: {0}ms",watch.ElapsedMilliseconds));
            File.WriteAllText("perfectsammoku.csv", res);
            Console.WriteLine("何かキーを押すと終了します");
            Console.ReadKey();
        }
    }
}
