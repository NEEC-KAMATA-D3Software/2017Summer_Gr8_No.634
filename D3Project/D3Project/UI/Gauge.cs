using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using D3Project.Device;

namespace D3Project.UI
{
    static class Gauge
    {
        private static string backgroundTexture;    //外枠の画像
        private static string pixel;                //内側の画像
        private static float maxValue;              //最大値
        private static float currentValue;          //現在の値
        private static float width;                 //横幅
        private static Rectangle bounds;
        private static Color color;

        static Gauge()
        {
            backgroundTexture = "gauge";
            pixel = "pixel";
            width = 180.0f;
            bounds = new Rectangle(17, 0, 250, 75);
            maxValue = 100;
            currentValue = 50;
            color = Color.Aqua;
        }
        
        public static void Draw(Renderer renderer)
        {
            //ゲージの量を計算
            int Width = (int)((currentValue / maxValue) * width);

            //ゲージの中身を描画
            renderer.DrawTexture(pixel, new Rectangle(bounds.X + 67, bounds.Y + 18, Width, bounds.Height - 40), color);

            //四角い背景描画
            renderer.DrawTexture(backgroundTexture, bounds, Color.White);
        }

        //値の設定
        public static void SetValue(float value)
        {
            currentValue = value;
        }


        //値の加算
        public static void AddValue(float value)
        {
            currentValue += value;
            if (maxValue <= currentValue) currentValue = maxValue;
        }

        //値の減算
        public static void SubValue(float value)
        {
            currentValue -= value;
            if (currentValue <= 0) currentValue = 0;
        }

        //値の確認
        public static bool hasValue(float value)
        {
            return value <= currentValue;
        }
    }
}