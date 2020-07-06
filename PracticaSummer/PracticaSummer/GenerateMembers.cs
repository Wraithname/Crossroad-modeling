using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaSummer
{
    public static class GenerateMembers
    {
        public static void VerticalRightCar()
        {
            int polosa = Engine.R.Next(0, Engine.CurrentRoad.VerticalRoadRight);
            int sprite = Engine.R.Next(0, 42);
            if (
                !Engine.Car.Exists(
                    c =>
                        c.X >= Engine.UserPanel.Width / 2 + 50 * (polosa - 1) &&
                        c.X <= Engine.UserPanel.Width / 2 + 50 * polosa &&
                        c.Y + 70 >= Engine.UserPanel.Height))
            {
                Engine.Car.Add(new Cars(Engine.UserPanel.Width / 2 - 8 + polosa * 40, Engine.UserPanel.Height, 3,
                    Sprite.SpriteLibUp[sprite], sprite,
                    new Vector(0, -1), polosa + 1, Engine.CurrentRoad.VerticalRoadRight,
                    Engine.CurrentRoad.HorizontRoadUp, Side.Right, RoadPass.WhatIsTurn(polosa + 1, Engine.CurrentRoad.VerticalRoadRight)));
            }
        }

        public static void VerticalLeftCar()
        {
            int polosa = Engine.R.Next(0, Engine.CurrentRoad.VerticalRoadLeft);
            int sprite = Engine.R.Next(0, 42);
            if (
                !Engine.Car.Exists(
                    c =>
                        c.X >= Engine.UserPanel.Width / 2 - 50 * (polosa + 1) && c.X <= Engine.UserPanel.Width / 2 - 50 * polosa &&
                        c.Y - 70 <= 0))
            {
                Engine.Car.Add(new Cars(Engine.UserPanel.Width / 2 - 50 - polosa * 40, 0, 3, Sprite.SpriteLibDown[sprite], sprite,
                    new Vector(0, 1), polosa + 1,
                    Engine.CurrentRoad.VerticalRoadLeft, Engine.CurrentRoad.HorizontRoadDown, Side.Left, RoadPass.WhatIsTurn(polosa + 1, Engine.CurrentRoad.VerticalRoadLeft)));
            }
        }

        public static void HorizontalUpCar()
        {
            int polosa = Engine.R.Next(0, Engine.CurrentRoad.HorizontRoadUp);
            int sprite = Engine.R.Next(0, 42);
            if (
                !Engine.Car.Exists(
                    c =>
                        c.Y >= Engine.UserPanel.Height / 2 - 50 * (polosa + 1) &&
                        c.Y <= Engine.UserPanel.Height / 2 - 50 * polosa &&
                        c.X + 70 >= Engine.UserPanel.Width))
            {
                Engine.Car.Add(new Cars(Engine.UserPanel.Width, Engine.UserPanel.Height / 2 - 50 - polosa * 40, 3,
                    Sprite.SpriteLibLeft[sprite], sprite,
                    new Vector(-1, 0), polosa + 1, Engine.CurrentRoad.HorizontRoadUp, Engine.CurrentRoad.VerticalRoadLeft, Side.Up, RoadPass.WhatIsTurn(polosa + 1, Engine.CurrentRoad.HorizontRoadUp)));
            }
        }

        public static void HorizontalDownCar()
        {
            int polosa = Engine.R.Next(0, Engine.CurrentRoad.HorizontRoadDown);
            int sprite = Engine.R.Next(0, 42);
            if (!Engine.Car.Exists(
                c =>
                    c.Y >= Engine.UserPanel.Height / 2 + 50 * (polosa - 1) && c.Y <= Engine.UserPanel.Height / 2 + 50 * polosa &&
                    c.X - 70 <= 0))
            {
                Engine.Car.Add(new Cars(0, Engine.UserPanel.Height / 2 - 8 + polosa * 40, 3, Sprite.SpriteLibRight[sprite], sprite,
                    new Vector(1, 0), polosa + 1,
                    Engine.CurrentRoad.HorizontRoadDown, Engine.CurrentRoad.VerticalRoadRight, Side.Down, RoadPass.WhatIsTurn(polosa + 1, Engine.CurrentRoad.HorizontRoadDown)));
            }
        }
    }
}
