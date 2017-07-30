using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Def;
using D3Project.Device;
using D3Project.Utility;
using D3Project.Character;
using D3Project.UI;

namespace D3Project.Scene
{
    class GamePlay : IScene
    {
        private bool isGameClear;                  //ゲームクリアフラグ
        private bool isEnd;                        //シーン終了フラグ
        private bool isGameOver;                   //ゲームオーバーフラグ
                                                   
        private GameDevice gameDevice;             //デバイス管理
        private InputState input;                  //操作
        private Sound sound;                       //音
        private Timer timer;
        private Timer gameClearTimer;              //ゲームクリア遷移用タイマー
        private Timer gameOverTimer;               //ゲームオーバー遷移用タイマー

        private CharacterControl characterControl; //キャラ管理
        private WaveManager waveManager;           //Wave管理
        private ItemManager itemManager;           //アイテム管理

        public GamePlay(GameDevice gameDevice)
        {
            this.gameDevice = gameDevice;
            this.input = gameDevice.GetInputState();
            this.sound = Sound.GetInstance();
        }

        public void Initialize()
        {
            //各種フラグ初期化
            isEnd = false;
            isGameClear = false;
            isGameOver = false;

            //各種オブジェクト初期化
            BOSSHP.Initialize();
            Heart.Initialize();
            Score.Initialize();

            //キャラ管理初期化
            characterControl = new CharacterControl();
            characterControl.Initialize();
            timer = new Timer(0);

            //Wave管理初期化
            waveManager = new WaveManager();
            waveManager.Initialize();

            //アイテム管理初期化
            itemManager = new ItemManager();
            itemManager.Initialize();

            //プレイヤー追加
            characterControl.AddFirst(new Player(input, characterControl));

            //シーン遷移用タイマー
            gameClearTimer = new Timer(1.0f);
            gameOverTimer = new Timer(1.0f);
        }


        public void Update(GameTime gameTime)
        {

            characterControl.Update(gameTime);
            waveManager.Update(characterControl);
            itemManager.Update(characterControl);
            timer.addUpdate();

            //ゲームオーバー確認
            if (Heart.GetHP() <= 0) isGameOver = true;

            //ゲームクリア確認
            if (waveManager.IsDead()) isGameClear = true;

            //ゲームオーバーまたはゲームクリアフラグがtrueだったら
            //１秒後にシーン遷移。
            if (isGameClear == true) gameClearTimer.Update();
            if (isGameOver == true) gameOverTimer.Update();
            if (gameClearTimer.IsTime() || gameOverTimer.IsTime()) isEnd = true;
        }


        public void Draw(Renderer renderer)
        {
            renderer.Begin();
            if(!waveManager.IsAppearBoss()) renderer.DrawTexture("background", Vector2.Zero);
            if (waveManager.IsAppearBoss()) renderer.DrawTexture("background(noDoor)", Vector2.Zero);
            characterControl.Draw(renderer);
            Gauge.Draw(renderer);
            renderer.DrawTexture("Score", new Vector2(260.0f, 525.0f), Color.Gainsboro);
            Score.Draw(renderer, new Vector2(440.0f, 522.0f), Color.Gainsboro);
            Heart.Draw(renderer, new Vector2(620.0f, 15.0f));
            renderer.End();
        }


        public void Shutdown()
        {
            sound.StopBGM();
        }

        public Scene Next()
        {
            if (isGameClear)return Scene.GameClear;
            if (isGameOver) return Scene.GameOver;
            return Scene.GamePlay;  //ダミー
        }

        public bool IsEnd() { return isEnd; }
    }
}
