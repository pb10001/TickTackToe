using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Children = new List<State>();
        }
        /// <summary>
        /// 手数
        /// </summary>
        public int Turn { get; set; }
        /// <summary>
        /// 盤
        /// </summary>
        public SammokuBoard Board { get; set; }
        /// <summary>
        /// 結果
        /// </summary>
        public MatchResult Winner { get; set; } = MatchResult.NotYet;
        /// <summary>
        /// 次に遷移しうる状態のリスト
        /// </summary>
        public List<State> Children { get; set; }
    }
}
