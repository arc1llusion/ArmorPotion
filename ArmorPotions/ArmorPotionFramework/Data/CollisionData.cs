using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;

namespace ArmorPotionFramework.Data
{
    public struct CollisionData
    {
        public readonly bool IsXAxisColliding;
        public readonly bool IsYAxisColliding;
        public readonly bool IsColliding;
        public readonly List<Tile> TileData;
        public CollisionData(bool isColliding, bool xAxisCollision, bool yAxisCollision, List<Tile> tileData)
        {
            IsColliding = isColliding;
            IsXAxisColliding = xAxisCollision;
            IsYAxisColliding = yAxisCollision;
            TileData = tileData;
        }
    }
}
