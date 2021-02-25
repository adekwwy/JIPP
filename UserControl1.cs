using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class UserControl1 : UserControl
    {
        private int rateCount;
        public int RateCount
        {
            get { return rateCount; }
            set 
            { 
                rateCount = value;
                Refresh();
            }
        }

        public UserControl1()
        {
            InitializeComponent();
            rateLabel.BackColor = System.Drawing.Color.Transparent;
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float w, h, x, y;

            w = this.Width;
            h = this.Height;

            x = 0;
            y = 0;


            for (int i =0; i<rateCount; i++)
            {
                if(rateCount<34)
                    e.Graphics.FillRectangle(Brushes.Red, x, 0, w * 0.01f, h);
                if(rateCount>=34 && rateCount<67)
                    e.Graphics.FillRectangle(Brushes.Yellow, x, 0, w * 0.01f, h);
                if(rateCount>=67)
                    e.Graphics.FillRectangle(Brushes.Green, x, 0, w * 0.01f, h);

                x += 0.01f * w;
            }
            

            base.OnPaint(e);
        }

        private void UserControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                RateCount = 1+(100 * e.X) / this.Width;
                rateLabel.Text = rateCount + "/100";
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            rateLabel.Text = rateCount + "/100";
        }
    }
}
