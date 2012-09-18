using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmorPotionFramework.Data
{
    public struct CollisionData
    {
        public readonly bool IsXAxisColliding;
        public readonly bool IsYAxisColliding;
        public readonly bool IsColliding;
        public CollisionData(bool isColliding, bool xAxisCollision, bool yAxisCollision)
        {
            IsColliding = isColliding;
            IsXAxisColliding = xAxisCollision;
            IsYAxisColliding = yAxisCollision;
        }
    }
}
