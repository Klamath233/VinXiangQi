using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace VinXiangQi {
    class DisplayBoardView : System.Windows.Forms.PictureBox {
        public readonly DisplayBoardViewModel ViewModel;

        public DisplayBoardView() : base()
        {
            this.ViewModel = new DisplayBoardViewModel();
            this.DataBindings.Clear();
            this.DataBindings.Add(new Binding("Image", this.ViewModel, "BoardDisplayBitmap"));
        }
    }
}