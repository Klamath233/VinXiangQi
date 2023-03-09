using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using static VinXiangQi.Mainform;

namespace VinXiangQi {
    public class DisplayBoardViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        private Graphics _boardGDI;
        private DisplayBoardViewModelState _displayBoardState;
        private Bitmap _boardDisplayMap;

        public struct DisplayBoardViewModelState {
            public String[,] Board;
            public ChessMove BestMove;
            public ChessMove PonderMove;
            public ChessMove BackgroundAnalysisMove;

            public DisplayBoardViewModelState(String [,] board, ChessMove bestMove, ChessMove ponderMove, ChessMove backgroundAnalysisMove)
            {
                Board = board;
                BestMove = bestMove;
                PonderMove = ponderMove;
                BackgroundAnalysisMove = backgroundAnalysisMove;
            }
        }

        public DisplayBoardViewModelState DisplayBoardState
        {
            get => _displayBoardState;
            set
            {
                _displayBoardState = value;
                Program.ProgramMainform.Invoke(new Action(() => 
                {
                    redrawBoardImage(value);
                }));
            }
        }

        public Bitmap BoardDisplayBitmap
        {
            get => _boardDisplayMap;
            set
            {
                _boardDisplayMap = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BoardDisplayBitmap"));
            }
        }

        public DisplayBoardViewModel() : base()
        {
            _boardDisplayMap = new Bitmap(400, 440);
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;  

        private void redrawBoardImage(DisplayBoardViewModelState state) {
            String [,] board = state.Board;
            ChessMove bestMove = state.BestMove;
            ChessMove ponderMove = state.PonderMove;
            ChessMove backgroundAnalysisMove = state.BackgroundAnalysisMove;

            if (board == null) return;

            Bitmap newBoardDisplayBitmap = new Bitmap(400, 440);
            _boardGDI = Graphics.FromImage(newBoardDisplayBitmap);

            int width = 40;
            int height = 40;
            int xoffset = width / 2;
            int yoffset = height / 2;
            _boardGDI.Clear(Color.White);
            _boardGDI.DrawImage(Properties.Resources.board, 0, 0, width * 10, height * 11);
            // draw chess
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (CurrentBoard[x, y] != null && CurrentBoard[x, y] != "")
                    {
                        string name = CurrentBoard[x, y];
                        _boardGDI.DrawImage((Bitmap)Properties.Resources.ResourceManager.GetObject(CurrentBoard[x, y]), x * width + xoffset, y * height + yoffset, width, height);
                    }
                }
            }
            // draw hint
            Pen rp = new Pen(Color.FromArgb(180, Color.OrangeRed), 10);
            Pen bp = new Pen(Color.FromArgb(180, Color.Blue), 10);
            Pen rp_dim = new Pen(Color.FromArgb(180, Color.DarkOrange), 8);
            Pen bp_dim = new Pen(Color.FromArgb(180, Color.DarkGreen), 8);
            rp.EndCap = LineCap.ArrowAnchor;
            bp.EndCap = LineCap.ArrowAnchor;
            rp_dim.EndCap = LineCap.ArrowAnchor;
            bp_dim.EndCap = LineCap.ArrowAnchor;
            if (bestMove.From.X != -1)
            {
                Point bestFrom = new Point(bestMove.From.X * width + xoffset + width / 2, bestMove.From.Y * height + yoffset + height / 2);
                Point bestTo = new Point(bestMove.To.X * width + xoffset + width / 2, bestMove.To.Y * height + yoffset + height / 2);
                if (RedSide)
                {
                    _boardGDI.DrawLine(rp, bestFrom, bestTo);
                }
                else
                {
                    _boardGDI.DrawLine(bp, bestFrom, bestTo);
                }
            }
            if (ponderMove.From.X != -1)
            {
                Point ponderFrom = new Point(ponderMove.From.X * width + xoffset + width / 2, ponderMove.From.Y * height + yoffset + height / 2);
                Point ponderTo = new Point(ponderMove.To.X * width + xoffset + width / 2, ponderMove.To.Y * height + yoffset + height / 2);
                if (RedSide)
                {
                    _boardGDI.DrawLine(bp, ponderFrom, ponderTo);
                }
                else
                {
                    _boardGDI.DrawLine(rp, ponderFrom, ponderTo);
                }
            }
            if (backgroundAnalysisMove.From.X != -1)
            {
                Point bgFrom = new Point(backgroundAnalysisMove.From.X * width + xoffset + width / 2, backgroundAnalysisMove.From.Y * height + yoffset + height / 2);
                Point bgTo = new Point(backgroundAnalysisMove.To.X * width + xoffset + width / 2, backgroundAnalysisMove.To.Y * height + yoffset + height / 2);
                if (RedSide)
                {
                    _boardGDI.DrawLine(rp_dim, bgFrom, bgTo);
                }
                else
                {
                    _boardGDI.DrawLine(bp_dim, bgFrom, bgTo);
                }
            }
            BoardDisplayBitmap = newBoardDisplayBitmap;
        }

    }
}
