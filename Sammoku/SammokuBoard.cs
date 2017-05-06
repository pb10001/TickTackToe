using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sammoku
{
    /// <summary>
    /// 盤上の石の状態
    /// </summary>
    public enum StoneType
    {
        /// <summary>
        /// 石なし
        /// </summary>
        None = 0,
        /// <summary>
        /// 先手
        /// </summary>
        Sente = 1,
        /// <summary>
        /// 後手
        /// </summary>
        Gote = 2
    }
    /// <summary>
    /// 対局結果
    /// </summary>
    public enum MatchResult
    {
        /// <summary>
        /// 引き分け
        /// </summary>
        Draw = 0,
        /// <summary>
        /// 先手の勝ち
        /// </summary>
        Sente = 1,
        /// <summary>
        /// 後手の勝ち
        /// </summary>
        Gote = 2,
        /// <summary>
        /// まだ終わっていない
        /// </summary>
        NotYet = 3
    }
    /// <summary>
    /// 盤を表すクラス
    /// </summary>
    public class SammokuBoard
    {
        /// <summary>
        /// 盤のインスタンスを初期化
        /// </summary>
        public SammokuBoard() { }
        /// <summary>
        /// 状態を元に盤のインスタンスを初期化
        /// </summary>
        /// <param name="stateString">状態の文字列</param>
        public SammokuBoard(string stateString)
        {
            var array = stateString.Split(',');
            board[0, 0] = (StoneType) int.Parse(array[0]);
            board[0, 1] = (StoneType) int.Parse(array[1]);
            board[0, 2] = (StoneType) int.Parse(array[2]);
            board[1, 0] = (StoneType) int.Parse(array[3]);
            board[1, 1] = (StoneType) int.Parse(array[4]);
            board[1, 2] = (StoneType) int.Parse(array[5]);
            board[2, 0] = (StoneType) int.Parse(array[6]);
            board[2, 1] = (StoneType) int.Parse(array[7]);
            board[2, 2] = (StoneType)int.Parse(array[8]);
        }
        StoneType[,] board = new StoneType[3, 3];
        /// <summary>
        /// 盤の各マスにデフォルトの状態(None)を代入
        /// </summary>
        public void Init()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = StoneType.None;
                }
            }
        }
        /// <summary>
        /// 盤に石を打つ
        /// </summary>
        /// <param name="row">行番号(0,1,2)</param>
        /// <param name="col">列番号(0,1,2)</param>
        /// <param name="player">先後</param>
        /// <returns>石を打った後の盤</returns>
        public SammokuBoard Add(int row,int col,StoneType player)
        {
            return new SammokuBoard(ToStateString()).add(row,col,player);
        }
        /// <summary>
        /// マスを指定して状態を取得
        /// </summary>
        /// <param name="row">行番号(0,1,2)</param>
        /// <param name="col">列番号(0,1,2)</param>
        /// <returns>マスの状態</returns>
        public StoneType GetState(int row,int col)
        {
            return board[row, col];
        }
        /// <summary>
        /// 盤の状態を文字列で取得
        /// </summary>
        /// <returns>盤の状態を表す文字列</returns>
        public string ToStateString()
        {
            var text = "";
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    text += ((int)board[row, col]).ToString();
                    text += ",";
                }
            }
            return text.TrimEnd(',');
        }
        /// <summary>
        /// 勝敗を判定
        /// </summary>
        /// <returns>勝ち、負け、引き分け、まだ終わっていないのいずれか</returns>
        public MatchResult JudgeWinner()
        {
            bool drawGame = true;
            foreach (var item in board)
            {
                if (item == StoneType.None)
                {
                    drawGame = false;
                }
            }
            if (
                  board[0, 0] == StoneType.Sente && board[0, 1] == StoneType.Sente && board[0, 2] == StoneType.Sente
                || board[1, 0] == StoneType.Sente && board[1, 1] == StoneType.Sente && board[1, 2] == StoneType.Sente
                || board[2, 0] == StoneType.Sente && board[2, 1] == StoneType.Sente && board[2, 2] == StoneType.Sente
                || board[0, 0] == StoneType.Sente && board[1, 0] == StoneType.Sente && board[2, 0] == StoneType.Sente
                || board[0, 1] == StoneType.Sente && board[1, 1] == StoneType.Sente && board[2, 1] == StoneType.Sente
                || board[0, 2] == StoneType.Sente && board[1, 2] == StoneType.Sente && board[2, 2] == StoneType.Sente
                || board[0, 0] == StoneType.Sente && board[1, 1] == StoneType.Sente && board[2, 2] == StoneType.Sente
                || board[0, 2] == StoneType.Sente && board[1, 1] == StoneType.Sente && board[2, 0] == StoneType.Sente
                )
            {
                return MatchResult.Sente;
            }
            else if (
                board[0, 0] == StoneType.Gote && board[0, 1] == StoneType.Gote && board[0, 2] == StoneType.Gote
                || board[1, 0] == StoneType.Gote && board[1, 1] == StoneType.Gote && board[1, 2] == StoneType.Gote
                || board[2, 0] == StoneType.Gote && board[2, 1] == StoneType.Gote && board[2, 2] == StoneType.Gote
                || board[0, 0] == StoneType.Gote && board[1, 0] == StoneType.Gote && board[2, 0] == StoneType.Gote
                || board[0, 1] == StoneType.Gote && board[1, 1] == StoneType.Gote && board[2, 1] == StoneType.Gote
                || board[0, 2] == StoneType.Gote && board[1, 2] == StoneType.Gote && board[2, 2] == StoneType.Gote
                || board[0, 0] == StoneType.Gote && board[1, 1] == StoneType.Gote && board[2, 2] == StoneType.Gote
                || board[0, 2] == StoneType.Gote && board[1, 1] == StoneType.Gote && board[2, 0] == StoneType.Gote
                )
            {
                return MatchResult.Gote;
            }
            else if (drawGame)
            {
                return MatchResult.Draw;
            }
            else
            {
                return MatchResult.NotYet;
            }
        }

        private SammokuBoard add(int row, int col, StoneType player)
        {
            if (player == StoneType.None)
            {
                throw new ArgumentException("Noneは打てません");
            }
            board[row, col] = player;
            return this;
        }
    }
}
