using System.Collections.Generic;
using System.Linq;
using Sammoku;

namespace PerfectAnalysis
{
    /// <summary>
    /// 三目並べを完全解析するクラス
    /// </summary>
    public class SammokuAnalyzer
    {
        /// <summary>
        /// 探索を実行
        /// </summary>
        /// <returns>探索結果の文字列</returns>
        public string Execute()
        {
            // 重複をチェックするためのハッシュセット
            HashSet<string> duplCheckList = new HashSet<string>();
            // 探索に使うためのリスト
            List<State>[] stateList = new List<State>[10];
            // 結果を返すためのリスト
            List<State> resultList = new List<State>();
            var initBoard = new SammokuBoard();
            initBoard.Init();
            var initState = new State(0, initBoard);
            initState.SearchChildren();

            //往路(リストに状態インスタンスを詰めていく)
            stateList[0] = new List<State>() { initState };
            for (int i = 1; i < 10; i++)
            {
                stateList[i] = new List<State>();
                stateList[i].AddRange(stateList[i - 1].SelectMany(x => x.Children));
                //重複チェック
                var newStates = stateList[i].Where(x => duplCheckList.Add(x.Board.ToStateString()));
                resultList.AddRange(newStates);
            }
            //復路(勝敗を更新する)
            for (int i = 8; i >= 0; i--)
            {
                foreach (var item in stateList[i].Where(x=>x.Winner == MatchResult.NotYet))
                {
                    item.ResetWinner();
                }
            }
            var header = "Turn,11,12,13,21,22,23,31,32,33,Winner\n";
            return header+string.Join("",resultList);
        }
    }
}
