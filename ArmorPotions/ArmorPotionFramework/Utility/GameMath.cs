using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ArmorPotionFramework.Utility
{
    public static class GameMath
    {
        public static double Distance(Vector2 value1, Vector2 value2) {
            float x = (value2.X - value1.X);
            float y = (value2.Y - value1.Y);
            return Math.Sqrt((x * x) + (y * y));
        }

        public static double CalculateAngle(Vector2 value1, Vector2 value2)
        {
            float deltaX = value2.X - value1.X;
            float deltaY = value2.Y - value1.Y;

            return Math.Atan2(deltaY, deltaX);
        }
    }

    public static class RandomGenerator
    {
        private static Random _random = new Random();

        public static Random Random
        {
            get { return _random; }
        }
    }
}
