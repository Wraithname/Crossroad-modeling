using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaSummer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Sprite.LoadCarSpriteLib();
            user_panel.Paint += Engine.RenderMap;
            Engine.UserPanel = user_panel;
            Engine.CarCount = textBox2;
            Engine.CurrentlyCarCount = textBox1;
            Engine.WorkTime = textBox3;
            Engine.Cpm = textBox4;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Engine.LightsInterval1 = (int)numericUpDown1.Value * 1000;
            Engine.LightsInterval2 = (int)numericUpDown2.Value * 1000;
            Engine.speedest = (int)numericUpDown3.Value;
            Engine.Initialization();
            Engine.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Engine.Pause();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Engine.Stop();
            user_panel.Controls.Clear();
            Engine.IsReady = false;
            Engine.Clear = true;
            user_panel.Invalidate();
        }
    }
}
