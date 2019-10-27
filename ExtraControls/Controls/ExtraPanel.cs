using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtraControls.Helpers;

namespace ExtraControls.Controls
{
    public partial class ExtraPanel : Panel
    {
        public ExtraPanel()
        {
            InitializeComponent();
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetOptions();
            e.Graphics.DrawRoundedRectangle(this.ClientRectangle, 11, 11, Color.Green, Color.White);
        }
    }
}
