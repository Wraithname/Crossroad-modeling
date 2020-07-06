using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace PracticaSummer
{
    public static class Engine
    {
        public static List<Cars> Car { get; set; }
        public static bool IsReady { get; set; }
        public static bool Clear { get; set; }
        public static List<BaseClass> Deleter { get; set; }
        public static Random R { get; set; }
        public static Road CurrentRoad { get; set; }
        public static Timer GenCarTimer { get; set; }
        public static Timer MoveTimer { get; set; }
        public static Timer WorkTimer { get; set; }
        public static int LightsInterval1 { get; set; }
        public static int LightsInterval2 { get; set; }
        public static Timer LightsTimer { get; set; }
        public static Panel UserPanel { get; set; }
        public static RightTurn RightTurn { get; set; }
        public static LeftTurn LeftTurn { get; set; }
        public static TextBox CarCount { get; set; }
        public static TextBox CurrentlyCarCount { get; set; }
        public static TextBox WorkTime { get; set; }
        public static TextBox Cpm { get; set; }
        public static int WorkTm { get; set; }
        public static TrafficLight[] TrafficLights { get; set; }
        public static RoadTransit CurrentRoadTransit { get; set; }
        public static int speedest=100;
        public static void Start()
        {
            MoveTimer.Start();
            GenCarTimer.Start();
            WorkTimer.Start();
            LightsTimer.Start();
            CurrentRoad = new Road(1, 1, 1, 1);
            CurrentRoadTransit = new RoadTransit(false, false, false, false, 1, 1, 1, 1);
            TrafficLight.CreateLight();
            IsReady = true;
        }

        public static void Pause()
        {
            MoveTimer.Stop();
            GenCarTimer.Stop();
            WorkTimer.Stop();
            LightsTimer.Stop();
        }

        public static void Stop()
        {
            Car.Clear();
            Deleter.Clear();
            MoveTimer.Stop();
            LightsTimer.Stop();
            GenCarTimer.Stop();
            WorkTimer.Stop();
            CarCount.Text = "0";
            CurrentlyCarCount.Text = "0";
            WorkTime.Text = "0 c";
            Cpm.Text = "0";
            WorkTm = 0;
            UserPanel.ResetBackColor();
            UserPanel.Invalidate();
        }

        public static void Initialization()
        {
            Car = new List<Cars>();
            Deleter = new List<BaseClass>();
            R = new Random(DateTime.Now.Millisecond);
            TrafficLights = new TrafficLight[4];
            RightTurn = new RightTurn();
            LeftTurn = new LeftTurn();
            MoveTimer = new Timer { Interval = 10 };
            MoveTimer.Tick += (sender, e) => Update();
            GenCarTimer = new Timer { Interval = speedest*1000 };
            GenCarTimer.Tick += (sender, e) => GenerateCar_Tick();
            WorkTimer = new Timer { Interval = 1000 };
            WorkTimer.Tick += (sender, e) => Work_tick();
            LightsTimer = new Timer { Interval = LightsInterval1 };
            LightsTimer.Tick += (sender, e) => TrafficLight.SwitchLight();
        }

        public static void Work_tick()
        {
            WorkTm++;
            WorkTime.Text = WorkTm + " c";
            Cpm.Text = Math.Round(Convert.ToDouble(CarCount.Text) * (60 / (double)WorkTm)).ToString(CultureInfo.InvariantCulture);
        }

        public static void Update()
        {
            CurrentlyCarCount.Text = Car.Count.ToString();
            //Логика работы
            foreach (var c in Car)
            {
                if (Cars.Check(c))
                {
                    c.Speed = 0;
                }
                else
                {
                    c.Speed = Cars.CanMove(c);
                    if (c.Turn == CTurn.Right)
                    {
                        RightTurn.StartTurn(c);
                    }
                    else if (c.Turn == CTurn.Left)
                    {
                        LeftTurn.StartTurn(c);
                    }
                }
                Move(c);
                if (c.X < -50 || c.X > UserPanel.Width + 50 || c.Y < -50 || c.Y > UserPanel.Height + 50)
                {
                    Deleter.Add(c);
                }
            }
            if (Deleter.Count != 0)
            {
                foreach (var d in Deleter)
                {
                    if (d is Cars)
                    {
                        Car.Remove((Cars)d);
                    }
                }
            }
            UserPanel.Invalidate();
        }

        public static void Move(BaseClass mm)
        {
            mm.Y += (int)mm.Direct.Y * mm.Speed;
            mm.X += (int)mm.Direct.X * mm.Speed;
        }

        public static void RenderMap(object sender, PaintEventArgs e)
        {
            if (IsReady)
            {
                CurrentRoad.RenderRoad(UserPanel, e);
                CurrentRoadTransit.RenderRoadTransit(UserPanel, e);
                foreach (var light in TrafficLights)
                {
                    TrafficLight.RenderLight(light, UserPanel, e);
                }
                foreach (var c in Car)
                {
                    e.Graphics.DrawImage(c.Sprite, new Point(c.X, c.Y));
                }
            }
            else if (Clear)
            {
                e.Graphics.Clear(Color.AliceBlue);
                Clear = false;
            }
        }

        public static void GenerateCar_Tick()
        {
            CarCount.Text = (Convert.ToInt32(CarCount.Text) + 1).ToString();
            switch (R.Next(1, 5))
            {
                case 1:
                    GenerateMembers.VerticalLeftCar();
                    break;
                case 2:
                    GenerateMembers.VerticalRightCar();
                    break;
                case 3:
                    GenerateMembers.HorizontalUpCar();
                    break;
                case 4:
                    GenerateMembers.HorizontalDownCar();
                    break;
            }
        }
    }
}
