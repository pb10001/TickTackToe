using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sammoku;

namespace PerfectAnalysis
{
    /// <summary>
    /// 三目並べを完全解析するクラス
    /// </summary>
    public class SammokuAnalyzer
    {
        /// <summary>
        /// インスタンスを初期化
        /// </summary>
        public SammokuAnalyzer()
        {
            var init = new SammokuBoard();
            init.Init();
            var initState = new State(0, init);
            stateList.Add(initState);
        }
        StoneType currentPlayer;
        /// <summary>
        /// 重複をチェックするためのハッシュセット
        /// </summary>
        HashSet<string> duplCheckList = new HashSet<string>();
        /// <summary>
        /// 探索に使うためのリスト
        /// </summary>
        List<State> stateList = new List<State>();
        /// <summary>
        /// 結果を返すためのリスト
        /// </summary>
        List<State> resultList = new List<State>();
        /// <summary>
        /// ある手数の探索が終わったことを表すデリゲート
        /// </summary>
        /// <param name="currentTurn">現在の手数</param>
        public delegate void TurnChangedEventHandler(int currentTurn);
        /// <summary>
        /// 手数が代わったときに発生するイベント
        /// </summary>
        public event TurnChangedEventHandler TurnChanged = delegate { };
        /// <summary>
        /// 探索を実行
        /// </summary>
        /// <returns>探索結果の文字列</returns>
        public string Execute()
        {
            for (int i = 0; i < 9; i++)
            {
                if (i % 2 == 0)
                {
                    currentPlayer = StoneType.Sente;
                }
                else
                {
                    currentPlayer = StoneType.Gote;
                }
                var tmp = new List<State>();
                foreach (var item in stateList.Where(x=>x.Turn == i))
                {
                    tmp.Add(item);
                }
                foreach (var item in tmp)
                {
                    switch (item.Board.JudgeWinner())
                    {
                        case MatchResult.Draw:
                            item.Winner = MatchResult.Draw;
                            break;
                        case MatchResult.Sente:
                            item.Winner = MatchResult.Sente;
                            break;
                        case MatchResult.Gote:
                            item.Winner = MatchResult.Gote;
                            break;
                        case MatchResult.NotYet:
                            searchChildren(item, currentPlayer);
                            break;
                        default:
                            break;
                    }
                }
                TurnChanged(i);
            }
            for (int i = 8; i >= 0; i--)
            {
                foreach (var item in stateList.Where(x=>x.Turn == i))
                {
                    if (i%2 == 1)
                    {
                        if (item.Children.Any(x => x.Winner == MatchResult.Gote))
                        {
                            item.Winner = MatchResult.Gote;
                        }
                        else if (item.Children.All(x=>x.Winner == MatchResult.Sente))
                        {
                            item.Winner = MatchResult.Sente;
                        }
                        else
                        {
                            item.Winner = MatchResult.Draw;
                        }
                    }
                    else
                    {
                        if (item.Children.Any(x => x.Winner == MatchResult.Sente))
                        {
                            item.Winner = MatchResult.Sente;
                        }
                        else if (item.Children.All(x => x.Winner == MatchResult.Gote))
                        {
                            item.Winner = MatchResult.Gote;
                        }
                        else
                        {
                            item.Winner = MatchResult.Draw;
                        }
                    }
                }
            }
            var res =resultList.Select(item => string.Format("{0},{1},{2}\n", item.Turn, item.Board.ToStateString(), item.Winner));
            var header = "Turn,11,12,13,21,22,23,31,32,33,Winner\n";
            return header+string.Join("",res);
        }
        private void searchChildren(State state,StoneType player)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (state.Board.GetState(row, col) == StoneType.None)
                    {
                        var newbrd = state.Board.Add(row, col, player);
                        var newst = new State(state.Turn + 1, newbrd)
                        {
                            Winner = newbrd.JudgeWinner()
                        };
                        state.Children.Add(newst);
                        //探索用リストにはそのまま追加
                        stateList.Add(newst);
                        if (duplCheckList.Add(newbrd.ToStateString()))
                        {
                            //ハッシュセットで重複を確認してから結果用リストに追加
                            resultList.Add(newst);
                        }
                    }
                }
            }
        }
    }
}
