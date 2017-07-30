using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using D3Project.Character;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Scene
{
    class GameOver : IScene
    {
        private InputState input;
        private Sound sound;
        private Timer timer;           //点滅用時間
        private Timer sceneNextTimer;  //シーン遷移するまでの時間
        private float timerSpeed;      //点滅スピード
        private float alpha;           //透過率
        private bool isPressed;        //シーン遷移時ボタン押下確認フラグ
        private bool isEnd;            //シーン終了フラグ

        private CharacterControl characterControl; //キャラ管理

        public GameOver(GameDevice gameDevice)
        {
            input = gameDevice.GetInputState();
            sound = Sound.GetInstance();
        }

        public void Initialize()
        {
            isEnd = false;
            isPressed = false;
            alpha = 0.0f;
            timerSpeed = 30.0f;
            timer = new Timer(0);
            sceneNextTimer = new Timer(1.0f);

            //キャラ管理初期化
            characterControl = new CharacterControl();
            characterControl.Initialize();

            characterControl.AddFirst(new GameOverPlayer(new Vector2(400.0f, 550.0f), sound, characterControl));
            characterControl.AddFirst(new GameOverEnemy(new GameOverAI(), sound, new Vector2(924.0f, 550.0f), characterControl));
        }

        public void Update(GameTime gameTime)
        {
            sound.ChangeBGMLoopFlag(false);
            sound.PlayBGM("GameOverBGM");

            characterControl.Update(gameTime);

            //ボタンフェード処理
            timer.addUpdate();
            if (0 < timer.Now() && timer.Now() < timerSpeed)
            {
                alpha += 0.04f;
                if (alpha > 1.0f) alpha = 1.0f;
            }

            if (timerSpeed * 2 <= timer.Now() && timer.Now() < timerSpeed * 3)
            {
                alpha -= 0.04f;
                if (alpha < 0.0f) alpha = 0.0f;
            }

            if (timerSpeed * 3 <= timer.Now()) timer.Initialize();


            //スタートボタンまたはスペースキーを押したときに画面遷移
            if ((input.GetButtonTrigger(Buttons.B) || input.GetKeyTrigger(Keys.Space)) && isPressed == false)
            {
                sound.PlaySE("button");
                isPressed = true;
            }

            if (isPressed)
            {
                timerSpeed = 10.0f;
                sceneNextTimer.Update();
                if (sceneNextTimer.IsTime()) isEnd = true;
            }
        }

        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            renderer.DrawTexture("GAMEOVER", new Vector2(35.0f, 100.0f));
            renderer.DrawTexture("button", new Vector2(130.0f, 380.0f), alpha);
            characterControl.Draw(renderer);
            renderer.End();
        }

        public void Shutdown()
        {
            sound.StopBGM();
        }

        public Scene Next()
        {
            return Scene.Title;
        }

        public bool IsEnd() { return isEnd; }
    }
}
