using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using D3Project.Device;
using D3Project.Utility;
using D3Project.Def;

namespace D3Project.UI
{
    class Slide
    {
        private InputState input;
        private float alpha;                 //透過率

        private List<string> slide;          //スライド管理
        private int currentSlide;            //現在のスライド

        private bool isShow;                 //表示させるかどうか

        public Slide(InputState input)
        {
            this.input = input;

            //透過率初期化
            alpha = 0;

            //スライド登録
            slide = new List<string>()
            {
                "HowToPlay1",
                "HowToPlay2"
            };

            //最初は非表示に
            isShow = false;
        }

        public void Initialize()
        {
            alpha = 0;
            isShow = false;
        }

        public void Update()
        {
            //表示させる場合
            if(isShow == true)
            {
                //フェードインして
                FadeIn();

                //スライド変更処理
                if(input.GetButtonTrigger(Buttons.LeftThumbstickRight) || input.GetKeyTrigger(Keys.Right))
                {
                    if(currentSlide == 0)
                    {
                        currentSlide = 1;
                    }
                }

                if (input.GetButtonTrigger(Buttons.LeftThumbstickLeft) || input.GetKeyTrigger(Keys.Left))
                {
                    if (currentSlide == 1)
                    {
                        currentSlide = 0;
                    }
                }
            }

            //表示させない場合
            if (isShow == false)
            {
                //フェードアウトさせる
                FadeOut();
            }

        }

        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture(slide[currentSlide], Vector2.Zero, alpha);
        }

        public void Show()
        {
            isShow = true;
        }

        public void End()
        {
            isShow = false;
        }

        public bool IsShow()
        {
            return isShow;
        }

        private void FadeIn()
        {
            alpha += 0.04f;
            if (alpha > 1.0f) alpha = 1.0f;
        }

        private void FadeOut()
        {
            alpha -= 0.04f;
            if (alpha < 0.0f) alpha = 0.0f;
        }
    }
}
