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
        private GraphicsDeviceManager graphicsDeviceManager;  //�O���t�B�b�N�Ǘ�
        private GameDevice gameDevice;                        //�f�o�C�X�Ǘ�
        private Renderer renderer;                            //�`��Ǘ�
        private InputState input;                             //���͊Ǘ�
        private Sound sound;                                  //�T�E���h�Ǘ�
        private SceneManager sceneManager;                    //�V�[���Ǘ�



        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = Screen.Width;   //��ʉ���
            graphicsDeviceManager.PreferredBackBufferHeight = Screen.Height; //��ʏc��
            graphicsDeviceManager.IsFullScreen = true;                       //�t���X�N���[��
            Content.RootDirectory = "Content";
            Window.Title = "No.634";
        }


        ////////////////////////����������////////////////////////
        protected override void Initialize()
        {
            gameDevice = new GameDevice(Content, GraphicsDevice);  //�f�o�C�X�Ǘ��̎��̐���
            renderer = gameDevice.GetRenderer();                   //�`��Ǘ��I�u�W�F�N�g�̎擾
            input = gameDevice.GetInputState();                    //���͊Ǘ��I�u�W�F�N�g�̎擾
            sound = Sound.GetInstance();                           //�T�E���h�Ǘ��I�u�W�F�N�g�̎擾
            sound.Initialize(Content);                             //�T�E���h�Ǘ��I�u�W�F�N�g�̏�����
            sceneManager = new SceneManager();                     //�V�[���Ǘ��̎��̐���

            //�V�[���̒ǉ�
            sceneManager.Add(Scene.Scene.Title, new SceneFader(new Title(gameDevice)));
            sceneManager.Add(Scene.Scene.GamePlay, new GamePlay(gameDevice));
            sceneManager.Add(Scene.Scene.GameClear, new SceneFader(new GameClear(gameDevice)));
            sceneManager.Add(Scene.Scene.GameOver, new SceneFader(new GameOver(gameDevice)));

            //�ŏ��̃V�[�����^�C�g���V�[����
            sceneManager.Change(Scene.Scene.Title);

            base.Initialize(); //�K�{
        }


        ////////////////////////���\�[�X�̓Ǎ�////////////////////////
        protected override void LoadContent()
        {
            //�摜�̓ǂݍ���
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

            #region �v���C���[�ƃG�l�~�[
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

            #region �A�C�e��
            renderer.LoadTexture("THREEWAY_Item32", "./Characters/Items/");
            renderer.LoadTexture("LAZER_Item32", "./Characters/Items/");
            renderer.LoadTexture("HOMING_Item32", "./Characters/Items/");
            renderer.LoadTexture("BOMB_Item32", "./Characters/Items/");
            renderer.LoadTexture("SKILL_Item16", "./Characters/Items/");
            #endregion

            #region �X�L��
            renderer.LoadTexture("Barrier1", "./Characters/Skills/");
            renderer.LoadTexture("Barrier2", "./Characters/Skills/");
            renderer.LoadTexture("Barrier3", "./Characters/Skills/");
            #endregion

            #region �e
            renderer.LoadTexture("bullet16", "./Characters/Bullets/");
            renderer.LoadTexture("HOMING_Bullet16", "./Characters/Bullets/");
            renderer.LoadTexture("LAZER_Bullet16", "./Characters/Bullets/");
            renderer.LoadTexture("BOMB_Bullet16", "./Characters/Bullets/");
            renderer.LoadTexture("enemyBullet16", "./Characters/Bullets/");
            renderer.LoadTexture("LAZER_enemyBullet32", "./Characters/Bullets/");
            #endregion

            #region �G�t�F�N�g
            renderer.LoadTexture("BOMB_Effect", "./Effect/");
            renderer.LoadTexture("Burst_Effect", "./Effect/");
            renderer.LoadTexture("Burst_Ring", "./Effect/");
            renderer.LoadTexture("BOSS_Burst_Effect", "./Effect/");
            renderer.LoadTexture("Invincible_Effect", "./Effect/");
            renderer.LoadTexture("door", "./Effect/");
            #endregion

            //�T�E���h�̓ǂݍ���

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

            //�P�s�N�Z���摜�̐���
            Texture2D fade = new Texture2D(GraphicsDevice, 1, 1);
            Color[] data = new Color[1 * 1];
            data[0] = new Color(0, 0, 0);
            fade.SetData(data);
            renderer.LoadTexture("fade", fade);
        }


        ////////////////////////���\�[�X�̉��////////////////////////
        protected override void UnloadContent()
        {
            renderer.Unload();
        }


        ////////////////////////�X�V����////////////////////////
        protected override void Update(GameTime gameTime)
        {
            // �I������
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || (Keyboard.GetState().IsKeyDown(Keys.Escape))) this.Exit();

            gameDevice.Update(gameTime);    //�f�o�C�X�Ǘ��̍X�V
            sceneManager.Update(gameTime);  //�V�[���Ǘ��̍X�V

            base.Update(gameTime); //�K�{
        }


        ////////////////////////�`�揈��////////////////////////
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);  //�w�i�F

            sceneManager.Draw(renderer);  �@�@�@//�V�[���̕`��

            base.Draw(gameTime); //�K�{
        }
    }
}
