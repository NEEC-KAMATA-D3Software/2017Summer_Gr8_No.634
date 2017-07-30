using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using D3Project.Def;
using D3Project.Device;
using D3Project.Scene;
using D3Project.Character;

namespace D3Project
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphicsDeviceManager;  //グラフィック管理
        private GameDevice gameDevice;                        //デバイス管理
        private Renderer renderer;                            //描画管理
        private InputState input;                             //入力管理
        private Sound sound;                                  //サウンド管理
        private SceneManager sceneManager;                    //シーン管理



        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;   //画面横幅
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height; //画面縦幅
            graphicsDeviceManager.IsFullScreen = true;                       //フルスクリーン
            Content.RootDirectory = "Content";
            Window.Title = "No.634";
        }


        ////////////////////////初期化処理////////////////////////
        protected override void Initialize()
        {
            gameDevice = new GameDevice(Content, GraphicsDevice);  //デバイス管理の実体生成
            renderer = gameDevice.GetRenderer();                   //描画管理オブジェクトの取得
            input = gameDevice.GetInputState();                    //入力管理オブジェクトの取得
            sound = Sound.GetInstance();                           //サウンド管理オブジェクトの取得
            sound.Initialize(Content);                             //サウンド管理オブジェクトの初期化
            sceneManager = new SceneManager();                     //シーン管理の実体生成

            //シーンの追加
            sceneManager.Add(Scene.Scene.Title, new SceneFader(new Title(gameDevice)));
            sceneManager.Add(Scene.Scene.GamePlay, new GamePlay(gameDevice));
            sceneManager.Add(Scene.Scene.GameClear, new SceneFader(new GameClear(gameDevice)));
            sceneManager.Add(Scene.Scene.GameOver, new SceneFader(new GameOver(gameDevice)));

            //最初のシーンをタイトルシーンに
            sceneManager.Change(Scene.Scene.Title);

            base.Initialize(); //必須
        }


        ////////////////////////リソースの読込////////////////////////
        protected override void LoadContent()
        {
            //画像の読み込み
            renderer.LoadTexture("background");
            renderer.LoadTexture("background(noDoor)");

            #region UI
            renderer.LoadTexture("gauge", "./UI/");
            renderer.LoadTexture("pixel", "./UI/");
            renderer.LoadTexture("heart", "./UI/");
            renderer.LoadTexture("LINE1", "./UI/");
            renderer.LoadTexture("LINE2", "./UI/");
            renderer.LoadTexture("WARNING", "./UI/");
            renderer.LoadTexture("number", "./UI/");
            renderer.LoadTexture("title_background", "./UI/");
            renderer.LoadTexture("Spotlight", "./UI/");
            renderer.LoadTexture("title_logo", "./UI/");
            renderer.LoadTexture("GAMESTART_button", "./UI/");
            renderer.LoadTexture("GAMESTART2_button", "./UI/");
            renderer.LoadTexture("HOWTOPLAY_button", "./UI/");
            renderer.LoadTexture("HOWTOPLAY2_button", "./UI/");
            renderer.LoadTexture("HowToPlay1", "./UI/");
            renderer.LoadTexture("HowToPlay2", "./UI/");
            renderer.LoadTexture("button", "./UI/");
            renderer.LoadTexture("CLEAR", "./UI/");
            renderer.LoadTexture("bar", "./UI/");
            renderer.LoadTexture("HP", "./UI/");
            renderer.LoadTexture("Score", "./UI/");
            renderer.LoadTexture("GameClear_background", "./UI/");
            renderer.LoadTexture("GAMEOVER", "./UI/");
            #endregion

            #region プレイヤーとエネミー
            renderer.LoadTexture("player48", "./Characters/");
            renderer.LoadTexture("enemy128", "./Characters/Enemies/");
            renderer.LoadTexture("Scout", "./Characters/Enemies/");
            renderer.LoadTexture("CircleCrashes", "./Characters/Enemies/");
            renderer.LoadTexture("ShootingScout", "./Characters/Enemies/");
            renderer.LoadTexture("Attack", "./Characters/Enemies/");
            renderer.LoadTexture("BossBound", "./Characters/Enemies/");
            renderer.LoadTexture("BossCircle1", "./Characters/Enemies/");
            renderer.LoadTexture("BossCircle2", "./Characters/Enemies/");
            renderer.LoadTexture("BossInfinite", "./Characters/Enemies/");
            renderer.LoadTexture("BossSpiral", "./Characters/Enemies/");
            renderer.LoadTexture("BossStar", "./Characters/Enemies/");
            renderer.LoadTexture("Lazer", "./Characters/Enemies/");
            renderer.LoadTexture("BombBattery", "./Characters/Enemies/");
            #endregion

            #region アイテム
            renderer.LoadTexture("THREEWAY_Item32", "./Characters/Items/");
            renderer.LoadTexture("LAZER_Item32", "./Characters/Items/");
            renderer.LoadTexture("HOMING_Item32", "./Characters/Items/");
            renderer.LoadTexture("BOMB_Item32", "./Characters/Items/");
            renderer.LoadTexture("SKILL_Item16", "./Characters/Items/");
            #endregion

            #region スキル
            renderer.LoadTexture("Barrier1", "./Characters/Skills/");
            renderer.LoadTexture("Barrier2", "./Characters/Skills/");
            renderer.LoadTexture("Barrier3", "./Characters/Skills/");
            #endregion

            #region 弾
            renderer.LoadTexture("bullet16", "./Characters/Bullets/");
            renderer.LoadTexture("HOMING_Bullet16", "./Characters/Bullets/");
            renderer.LoadTexture("LAZER_Bullet16", "./Characters/Bullets/");
            renderer.LoadTexture("BOMB_Bullet16", "./Characters/Bullets/");
            renderer.LoadTexture("enemyBullet16", "./Characters/Bullets/");
            renderer.LoadTexture("LAZER_enemyBullet32", "./Characters/Bullets/");
            #endregion

            #region エフェクト
            renderer.LoadTexture("BOMB_Effect", "./Effect/");
            renderer.LoadTexture("Burst_Effect", "./Effect/");
            renderer.LoadTexture("Burst_Ring", "./Effect/");
            renderer.LoadTexture("BOSS_Burst_Effect", "./Effect/");
            renderer.LoadTexture("Invincible_Effect", "./Effect/");
            renderer.LoadTexture("door", "./Effect/");
            #endregion

            //サウンドの読み込み

            #region BGM
            sound.LoadBGM("titleBGM");
            sound.LoadBGM("GamePlayBGM");
            sound.LoadBGM("BossBGM");
            sound.LoadBGM("GameOverBGM");
            sound.LoadBGM("GameClearBGM");
            #endregion

            #region SE
            sound.LoadSE("player_shot");
            sound.LoadSE("Burst");
            sound.LoadSE("Bomb");
            sound.LoadSE("SkillPoint");
            sound.LoadSE("Item");
            sound.LoadSE("Barrier");
            sound.LoadSE("BarrierDamage");
            sound.LoadSE("Invincible");
            sound.LoadSE("Warning");
            sound.LoadSE("button");
            sound.LoadSE("SlideShow");
            sound.LoadSE("SlideEnd");
            sound.LoadSE("Damege");
            #endregion

            //１ピクセル画像の生成
            Texture2D fade = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1 * 1];
            data[0] = new Color(0, 0, 0);
            fade.SetData(data);
            renderer.LoadTexture("fade", fade);
        }


        ////////////////////////リソースの解放////////////////////////
        protected override void UnloadContent()
        {
            renderer.Unload();
        }


        ////////////////////////更新処理////////////////////////
        protected override void Update(GameTime gameTime)
        {
            // 終了処理
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || (Keyboard.GetState().IsKeyDown(Keys.Escape))) this.Exit();

            gameDevice.Update(gameTime);    //デバイス管理の更新
            sceneManager.Update(gameTime);  //シーン管理の更新

            base.Update(gameTime); //必須
        }


        ////////////////////////描画処理////////////////////////
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);  //背景色

            sceneManager.Draw(renderer);  　　　//シーンの描画

            base.Draw(gameTime); //必須
        }
    }
}
