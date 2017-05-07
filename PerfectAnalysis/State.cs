using System.Collections.Generic;
using System.Linq;
using Sammoku;

namespace PerfectAnalysis
{
    /// <summary>
    /// 状態を表すクラス
    /// </summary>
    public class State
    {
        /// <summary>
        /// 状態のインスタンスを初期化
        /// </summary>
        /// <param name="turn">手数</param>
        /// <param name="board">盤</param>
        public State(int turn, SammokuBoard board)
        {
            Turn = turn;
            Board = board;
            Winner = board.JudgeWinner();
            Children = new List<State>();
        }
        /// <summary>
        /// 手数
        /// </summary>
        private int Turn { get; }
        /// <summary>
        /// 盤
        /// </summary>
        public SammokuBoard Board { get;}
        /// <summary>
        /// 結果
        /// </summary>
        public MatchResult Winner { get; set; } = MatchResult.NotYet;
        /// <summary>
        /// 次に遷移しうる状態のリスト
        /// </summary>
        public List<State> Children { get; }
        /// <summary>
        /// 次の状態を探索(再帰的に最後まで探索するので、呼び出すのは1回でよい)
        /// </summary>
        public void SearchChildren()
        {
            if (Board.JudgeWinner() != MatchResult.NotYet)
            {
                return;
            }
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Board.GetState(row, col) == StoneType.None)
                    {
                        var nextSengo = (Turn+1) % 2 == 1 ? StoneType.Sente : StoneType.Gote;
                        var childBoard = Board.Add(row, col, nextSengo);
                        var childState = new State(Turn + 1, childBoard);
                        childState.SearchChildren(); //再帰的に呼び出す
                        Children.Add(childState);
                    }
                }
            }
        }
        /// <summary>
        /// WinnerがNotYetの場合、次の状態を見てWinnerを更新
        /// </summary>
        public void ResetWinner()
        {
            if (Turn%2 == 1)
            {
                if (Children.Any(x => x.Winner == MatchResult.Gote))
                {
                    Winner = MatchResult.Gote;
                }
                else if (Children.All(x => x.Winner == MatchResult.Sente))
                {
                    Winner = MatchResult.Sente;
                }
                else
                {
                    Winner = MatchResult.Draw;
                }
            }
            else
            {
                if (Children.Any(x => x.Winner == MatchResult.Sente))
                {
                    Winner = MatchResult.Sente;
                }
                else if (Children.All(x => x.Winner == MatchResult.Gote))
                {
                    Winner = MatchResult.Gote;
                }
                else
                {
                    Winner = MatchResult.Draw;
                }
            }
        }
        /// <summary>
        /// 出力用の文字列を作成
        /// </summary>
        /// <returns>出力用の文字列</returns>
        public override string ToString()
        {
            return string.Format("{0},{1},{2}\n", Turn, Board.ToStateString(), Winner);
        }
    }
}
