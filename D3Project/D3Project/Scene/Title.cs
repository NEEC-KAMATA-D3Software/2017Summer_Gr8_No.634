using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using D3Project.Character;
using D3Project.Device;
using D3Project.Utility;
using D3Project.UI;

namespace D3Project.Scene
{
    class Title : IScene //シーンインタフェースを継承
    {
        private InputState input;
        private Sound sound;
        private float alpha;              //透過率
        private int flashCount;           //点滅回数
        private Timer flashTimer;         //点滅用時間
        private Timer sceneNextTimer;     //シーン遷移するまでの時間
        private bool isSceneNext;         //シーン遷移時ボタン押下確認フラグ
        private bool isEnd;               //シーン終了フラグ

        private List<string> menuButton;  //メニューボタン管理
        private List<string> menuButton2; //メニューボタン管理2
        private List<Vector2> positions;  //メニューボタン位置管理
        private int currentButton;        //現在のボタン
        private Slide slide;              //スライド管理


        private CharacterControl characterControl; //キャラ管理

        public Title(GameDevice gameDevice)
        {
            input = gameDevice.GetInputState();
            sound = Sound.GetInstance();
        }

        public void Initialize()
        {
            //各種値初期化
            isEnd = false;
            isSceneNext = false;
            alpha = 1.0f;
            flashCount = 0;
            flashTimer = new Timer(0);
            sceneNextTimer = new Timer(1.0f);

            //ボタン関連初期化
            menuButton = new List<string>()
            {
                "GAMESTART_button",
                "HOWTOPLAY_button",
            };
            menuButton2 = new List<string>()
            {
                "GAMESTART2_button",
                "HOWTOPLAY2_button",
            };
            positions = new List<Vector2>()
            {
                new Vector2(200.0f, 350.0f),
                new Vector2(200.0f, 450.0f)
            };
            currentButton = 0;

            //スライド関連初期化
            slide = new Slide(input);
            slide.Initialize();

            //キャラ管理初期化
            characterControl = new CharacterControl();
            characterControl.Initialize();
            characterControl.AddFirst(new titlePlayer(new titleAI(), new Vector2(70.0f, 534.0f), characterControl));
            characterControl.AddFirst(new titleEnemy(new titleAI2(), new Vector2(70.0f, 624.0f), characterControl));
            characterControl.AddFirst(new titleEnemy(new titleAI3(), new Vector2(70.0f, 674.0f), characterControl));
            characterControl.AddFirst(new Spotlight(new titleAI(),  new Vector2(70.0f, 524.0f), characterControl));
            characterControl.AddFirst(new Spotlight(new titleAI2(), new Vector2(70.0f, 624.0f), characterControl));
            characterControl.AddFirst(new Spotlight(new titleAI3(), new Vector2(70.0f, 674.0f), characterControl));
        }


        public void Update(GameTime gameTime)
        {
            sound.ChangeBGMLoopFlag(true);
            sound.PlayBGM("titleBGM");

            characterControl.Update(gameTime);

            //ボタンフェード処理
            if(isSceneNext && flashCount < 2)flashTimer.addUpdate();
            if (0 < flashTimer.Now() && flashTimer.Now() < 15.0f)
            {
                alpha -= 0.04f;
                if (alpha < 0.0f) alpha = 0.0f;
            }

            if (15.0f <= flashTimer.Now() && flashTimer.Now() < 30.0f)
            {
                alpha += 0.04f;
                if (alpha > 1.0f) alpha = 1.0f;
            }

            if (30.0f <= flashTimer.Now()) { flashTimer.Initialize(); flashCount++; }

            //メニュー関連
            slide.Update();

            //スライドが表示されていない&次のシーンへ遷移させていなかったら
            if (!slide.IsShow() && !isSceneNext)
            {
                //下へ移動
                if ((input.GetButtonTrigger(Buttons.LeftThumbstickDown) || input.GetKeyTrigger(Keys.Down)))
                {
                    if (currentButton < menuButton.Count - 1) currentButton++;
                    else currentButton = 0;
                }

                //上へ移動
                if ((input.GetButtonTrigger(Buttons.LeftThumbstickUp) || input.GetKeyTrigger(Keys.Up)))
                {
                    if (currentButton > 0) currentButton--;
                    else currentButton = menuButton.Count - 1;
                }
            }

            //Bボタンまたはスペースキーを押したときにボタン実行
            if ((input.GetButtonTrigger(Buttons.B) || input.GetKeyTrigger(Keys.Space)) && isSceneNext == false)
            {
                if(currentButton == 0)
                {
                    sound.PlaySE("button");
                    isSceneNext = true;
                }

                if(currentButton == 1 && !slide.IsShow())
                {
                    sound.PlaySE("SlideShow");
                    slide.Show();
                }
            }

            //AボタンまたはBackSpaceを押したときにスライドを戻す
            if ((input.GetButtonTrigger(Buttons.A) || input.GetKeyTrigger(Keys.Back)))
            {
                if (currentButton == 1 && slide.IsShow())
                {
                    sound.PlaySE("SlideEnd");
                    slide.End();
                }
            }


            if (isSceneNext)
            {
                sceneNextTimer.Update();
                if(sceneNextTimer.IsTime()) isEnd = true;
            }
        }


        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("title_background", Vector2.Zero);
            characterControl.Draw(renderer);
            renderer.DrawTexture("title_logo", new Vector2(135.0f, 65.0f));

            //ボタン表示
            for(int i = 0; i < menuButton.Count; i++)
            {
                if (i == currentButton) renderer.DrawTexture(menuButton2[i], positions[i], alpha);
                else renderer.DrawTexture(menuButton[i], positions[i]);
            }

            //スライド表示
            slide.Draw(renderer);
            renderer.End();
        }


        public void Shutdown()
        {
            sound.StopBGM();
        }


        public Scene Next()
        {
            return Scene.GamePlay;
        }

        public bool IsEnd() { return isEnd; }
    }
}
