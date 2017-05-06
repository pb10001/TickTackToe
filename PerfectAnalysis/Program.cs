using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace PerfectAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("三目並べ完全解析");
            watch = new Stopwatch();
            var analyser = new SammokuAnalyzer();
            analyser.TurnChanged += Analyser_TurnChanged;
            watch.Start();
            var res = analyser.Execute();
            File.WriteAllText("perfectsammoku.csv", res);
        }
        static Stopwatch watch;
        private static void Analyser_TurnChanged(int currentTurn)
        {
            Console.WriteLine(string.Format("{0}手目終了: {1}",currentTurn,watch.Elapsed.Seconds));
        }
    }
}
