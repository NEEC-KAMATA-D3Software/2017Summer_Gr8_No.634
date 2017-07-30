using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace D3Project.Utility
{
    static class Calculate
    {
        //角度から向きを計算
        public static Vector2 direction(float angle)
        {
            Vector2 upVector = new Vector2(0.0f, -1.0f);
            Matrix rotation = Matrix.CreateRotationZ(angle);
            Vector2 direction = Vector2.Transform(upVector, rotation);

            if (direction.Length() != 0.0f) direction.Normalize();

            return direction;
        }

        //度数→ラジアン変換
        public static float radian(float degree)
        {
            return degree * ((float)Math.PI / 180.0f);
        }

        //ラジアン→度数変換
        public static float degree(float radian)
        {
            return radian * (180.0f / (float)Math.PI);
        }
    }
}
