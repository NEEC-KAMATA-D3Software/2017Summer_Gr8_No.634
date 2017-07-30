using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;
using D3Project.Device;

namespace D3Project.Character
{
    class WaveManager
    {
        private Sound sound;                       //サウンド
        private int WaveNumber;                    //Waveナンバー
        private Timer timer;                       //タイマー
        private Random rand;                       //乱数
        private bool isDead;                       //Wave削除確認フラグ
        private bool isAppearBoss;                 //Boss出現フラグ

        public WaveManager() { }

        public void Initialize()
        {
            sound = Sound.GetInstance();
            sound.ChangeBGMLoopFlag(true);
            sound.PlayBGM("GamePlayBGM");

            //Waveナンバー初期化
            WaveNumber = 0;

            //その他初期化
            timer = new Timer(0);
            rand = new Random();
            isDead = false;
            isAppearBoss = false;

        }

        public void Update(CharacterControl characterControl)
        {
            timer.addUpdate();

            //敵生成処理
            int randPos;
            if (WaveNumber == 0)
            {
                if (timer.Now(1)) characterControl.AddWave(new FakePlayer(new Vector2(824.0f, 300.0f), Calculate.radian(270), characterControl));
            }

            if (WaveNumber == 0 && timer.Now() >= 100.0f) { WaveNumber = 1; timer.Initialize(); }

            #region 斥候
            #region 直線移動(右→左)
            if (WaveNumber == 1)
            {
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 300.0f), new Vector2(-24.0f, 300.0f)), Calculate.radian(90), characterControl));
                if (timer.Now(140)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 200.0f), new Vector2(-24.0f, 200.0f)), Calculate.radian(90), characterControl));
                if (timer.Now(140)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 400.0f), new Vector2(-24.0f, 400.0f)), Calculate.radian(90), characterControl));
                if (timer.Now(160)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 100.0f), new Vector2(-24.0f, 100.0f)), Calculate.radian(90), characterControl));
                if (timer.Now(160)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 500.0f), new Vector2(-24.0f, 500.0f)), Calculate.radian(90), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 1 && timer.Now() > 160) { WaveNumber = 2; timer.Initialize(); }
            else if (WaveNumber == 1 && 240 < timer.Now()) { WaveNumber = 2; timer.Initialize(); }

            #region 直線移動(左→右)
            if (WaveNumber == 2)
            {
                if (timer.Now(0)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 300.0f), new Vector2(824.0f, 300.0f)), Calculate.radian(-90), characterControl));
                if (timer.Now(20)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 200.0f), new Vector2(824.0f, 200.0f)), Calculate.radian(-90), characterControl));
                if (timer.Now(20)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 400.0f), new Vector2(824.0f, 400.0f)), Calculate.radian(-90), characterControl));
                if (timer.Now(40)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 100.0f), new Vector2(824.0f, 100.0f)), Calculate.radian(-90), characterControl));
                if (timer.Now(40)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 500.0f), new Vector2(824.0f, 500.0f)), Calculate.radian(-90), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 2 && timer.Now() > 40) { WaveNumber = 3; timer.Initialize(); }
            else if (WaveNumber == 2 && 120 < timer.Now()) { WaveNumber = 3; timer.Initialize(); }

            #region 直線移動(上→下)
            if (WaveNumber == 3)
            {
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(400.0f, -24.0f), new Vector2(400.0f, 624.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(80)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(300.0f, -24.0f), new Vector2(300.0f, 624.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(80)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(500.0f, -24.0f), new Vector2(500.0f, 624.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(100)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(200.0f, -24.0f), new Vector2(200.0f, 624.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(100)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(600.0f, -24.0f), new Vector2(600.0f, 624.0f)), Calculate.radian(0), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 3 && timer.Now() > 100) { WaveNumber = 4; timer.Initialize(); }
            else if (WaveNumber == 3 && 120 < timer.Now()) { WaveNumber = 4; timer.Initialize(); }

            #region 直線移動(下→上)
            if (WaveNumber == 4)
            {
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(400.0f, 624.0f), new Vector2(400.0f, -24.0f)), Calculate.radian(180), characterControl));
                if (timer.Now(80)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(300.0f, 624.0f), new Vector2(300.0f, -24.0f)), Calculate.radian(180), characterControl));
                if (timer.Now(80)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(500.0f, 624.0f), new Vector2(500.0f, -24.0f)), Calculate.radian(180), characterControl));
                if (timer.Now(100)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(200.0f, 624.0f), new Vector2(200.0f, -24.0f)), Calculate.radian(180), characterControl));
                if (timer.Now(100)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(600.0f, 624.0f), new Vector2(600.0f, -24.0f)), Calculate.radian(180), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 4 && timer.Now() > 100) { WaveNumber = 5; timer.Initialize(); }
            else if (WaveNumber == 4 && 120 < timer.Now()) { WaveNumber = 5; timer.Initialize(); }

            #region 直線移動(左上から右下)
            if (WaveNumber == 5)
            {
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 500.0f), new Vector2(824.0f, 300.0f)), Calculate.radian(-90), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 300.0f), new Vector2(824.0f, 100.0f)), Calculate.radian(-90), characterControl));
                if (timer.Now(90)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 400.0f), new Vector2(824.0f, 200.0f)), Calculate.radian(-90), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 500.0f), new Vector2(824.0f, 300.0f)), Calculate.radian(-90), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(-24.0f, 300.0f), new Vector2(824.0f, 100.0f)), Calculate.radian(-90), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 5 && timer.Now() > 120) { WaveNumber = 6; timer.Initialize(); }
            else if (WaveNumber == 5 && 160 < timer.Now()) { WaveNumber = 6; timer.Initialize(); }

            #region 直線移動(右上から左下)
            if (WaveNumber == 6)
            {
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 500.0f), new Vector2(-24.0f, 300.0f)), Calculate.radian(90), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 300.0f), new Vector2(-24.0f, 100.0f)), Calculate.radian(90), characterControl));
                if (timer.Now(90)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 400.0f), new Vector2(-24.0f, 200.0f)), Calculate.radian(90), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 500.0f), new Vector2(-24.0f, 300.0f)), Calculate.radian(90), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(824.0f, 300.0f), new Vector2(-24.0f, 100.0f)), Calculate.radian(90), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 6 && timer.Now() > 120) { WaveNumber = 7; timer.Initialize(); }
            else if (WaveNumber == 6 && 160 < timer.Now()) { WaveNumber = 7; timer.Initialize(); }

            #region 直線移動(上右から下左)
            if (WaveNumber == 7)
            {
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(300.0f, -24.0f), new Vector2(700.0f, 624.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(100.0f, -24.0f), new Vector2(500.0f, 624.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(90)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(200.0f, -24.0f), new Vector2(600.0f, 624.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(300.0f, -24.0f), new Vector2(700.0f, 624.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(100.0f, -24.0f), new Vector2(500.0f, 624.0f)), Calculate.radian(0), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 7 && timer.Now() > 120) { WaveNumber = 8; timer.Initialize(); }
            else if (WaveNumber == 7 && 160 < timer.Now()) { WaveNumber = 8; timer.Initialize(); }

            #region 直線移動(上左から下右)
            if (WaveNumber == 8)
            {
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(300.0f, 624.0f), new Vector2(700.0f, -24.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(100.0f, 624.0f), new Vector2(500.0f, -24.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(90)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(200.0f, 624.0f), new Vector2(600.0f, -24.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(300.0f, 624.0f), new Vector2(700.0f, -24.0f)), Calculate.radian(0), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new Scouts(new StraightAI(new Vector2(100.0f, 624.0f), new Vector2(500.0f, -24.0f)), Calculate.radian(0), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 8 && timer.Now() > 120) { WaveNumber = 10; timer.Initialize(); }
            else if (WaveNumber == 8 && 160 < timer.Now()) { WaveNumber = 10; timer.Initialize(); }

            //#region 放物線(下→右→上→左)
            //if (WaveNumber == 9)
            //{
            //    if (timer.Now(60)) characterControl.AddWave(new Scouts(new VerticalParabolaAI(new Vector2(200.0f, 624.0f), 3.5f, -16.0f, 0.7f), Calculate.radian(180), characterControl));
            //    if (timer.Now(120)) characterControl.AddWave(new Scouts(new HorizontalParabolaAI(new Vector2(824.0f, 450.0f), -18.0f, -2.5f, 0.7f), Calculate.radian(90), characterControl));
            //    if (timer.Now(180)) characterControl.AddWave(new Scouts(new VerticalParabolaAI(new Vector2(600.0f, -24.0f), -3.5f, 16.0f, -0.7f), Calculate.radian(0), characterControl));
            //    if (timer.Now(240)) characterControl.AddWave(new Scouts(new HorizontalParabolaAI(new Vector2(-24.0f, 150.0f), 18.0f, 2.5f, -0.7f), Calculate.radian(-90), characterControl));

            //    if (timer.Now(300)) characterControl.AddWave(new Scouts(new VerticalParabolaAI(new Vector2(600.0f, -24.0f), -3.5f, 16.0f, -0.3f), Calculate.radian(0), characterControl));
            //    if (timer.Now(360)) characterControl.AddWave(new Scouts(new HorizontalParabolaAI(new Vector2(824.0f, 450.0f), -18.0f, -2.5f, 0.3f), Calculate.radian(90), characterControl));
            //    if (timer.Now(420)) characterControl.AddWave(new Scouts(new VerticalParabolaAI(new Vector2(200.0f, 624.0f), 3.5f, -16.0f, 0.3f), Calculate.radian(180), characterControl));
            //    if (timer.Now(480)) characterControl.AddWave(new Scouts(new HorizontalParabolaAI(new Vector2(-24.0f, 150.0f), 18.0f, 2.5f, -0.3f), Calculate.radian(-90), characterControl));
            //}
            //#endregion

            //if (characterControl.isWaveDead() && WaveNumber == 9 && timer.Now() > 480) { WaveNumber = 10; timer.Initialize(); }

            #region サインカーブ(左→右)
            if (WaveNumber == 10)
            {
                if (timer.Now(60)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(-24.0f, 140.0f), 2.0f), Calculate.radian(-90), characterControl));
                if (timer.Now(130)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(-24.0f, 220.0f), 2.0f), Calculate.radian(-90), characterControl));
                if (timer.Now(200)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(-24.0f, 300.0f), 2.0f), Calculate.radian(-90), characterControl));
                if (timer.Now(270)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(-24.0f, 380.0f), 2.0f), Calculate.radian(-90), characterControl));
                if (timer.Now(340)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(-24.0f, 460.0f), 2.0f), Calculate.radian(-90), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 10 && timer.Now() > 340) { WaveNumber = 11; timer.Initialize(); }
            else if (WaveNumber == 10 && 440 < timer.Now()) { WaveNumber = 11; timer.Initialize(); }

            #region サインカーブ(上→下)
            if (WaveNumber == 11)
            {
                if (timer.Now(60)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(200.0f, -24.0f), 2.0f), Calculate.radian(0), characterControl));
                if (timer.Now(130)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(300.0f, -24.0f), 2.0f), Calculate.radian(0), characterControl));
                if (timer.Now(200)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(400.0f, -24.0f), 2.0f), Calculate.radian(0), characterControl));
                if (timer.Now(270)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(500.0f, -24.0f), 2.0f), Calculate.radian(0), characterControl));
                if (timer.Now(340)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(600.0f, -24.0f), 2.0f), Calculate.radian(0), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 11 && timer.Now() > 340) { WaveNumber = 12; timer.Initialize(); }
            else if (WaveNumber == 11 && 440 < timer.Now()) { WaveNumber = 12; timer.Initialize(); }

            #region サインカーブ(右→左)
            if (WaveNumber == 12)
            {
                if (timer.Now(60)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(824.0f, 460.0f), -2.0f), Calculate.radian(90), characterControl));
                if (timer.Now(130)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(824.0f, 380.0f), -2.0f), Calculate.radian(90), characterControl));
                if (timer.Now(200)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(824.0f, 300.0f), -2.0f), Calculate.radian(90), characterControl));
                if (timer.Now(270)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(824.0f, 220.0f), -2.0f), Calculate.radian(90), characterControl));
                if (timer.Now(340)) characterControl.AddWave(new CircleCrashes(new HorizontalSincurveAI(new Vector2(824.0f, 140.0f), -2.0f), Calculate.radian(90), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 12 && timer.Now() > 340) { WaveNumber = 13; timer.Initialize(); }
            else if (WaveNumber == 12 && 440 < timer.Now()) { WaveNumber = 13; timer.Initialize(); }

            #region サインカーブ(下→上)
            if (WaveNumber == 13)
            {
                if (timer.Now(60)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(200.0f, 624.0f), -2.0f), Calculate.radian(180), characterControl));
                if (timer.Now(130)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(300.0f, 624.0f), -2.0f), Calculate.radian(180), characterControl));
                if (timer.Now(200)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(400.0f, 624.0f), -2.0f), Calculate.radian(180), characterControl));
                if (timer.Now(270)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(500.0f, 624.0f), -2.0f), Calculate.radian(180), characterControl));
                if (timer.Now(340)) characterControl.AddWave(new CircleCrashes(new VerticalSincurveAI(new Vector2(600.0f, 624.0f), -2.0f), Calculate.radian(180), characterControl));
            }
            #endregion

            if (characterControl.isWaveDead() && WaveNumber == 13 && timer.Now() > 340) { WaveNumber = 14; timer.Initialize(); }

            #endregion

            #region ザコ敵
            if (WaveNumber == 14)
            {
                #region 上側から
                if (timer.Now(60)) characterControl.AddWave(new ShootingScouts(new Vector2(randPos = rand.Next(150, 250), -24.0f), new Vector2(randPos, 80.0f), Calculate.radian(0), characterControl));
                if (timer.Now(80)) characterControl.AddWave(new ShootingScouts(new Vector2(randPos = rand.Next(350, 450), -24.0f), new Vector2(randPos, 80.0f), Calculate.radian(0), characterControl));
                if (timer.Now(100)) characterControl.AddWave(new ShootingScouts(new Vector2(randPos = rand.Next(550, 650), -24.0f), new Vector2(randPos, 80.0f), Calculate.radian(0), characterControl));
                #endregion
                #region 右側から
                if (timer.Now(120)) characterControl.AddWave(new ShootingScouts(new Vector2(824.0f, randPos = rand.Next(100, 200)), new Vector2(720.0f, randPos), Calculate.radian(90), characterControl));
                if (timer.Now(140)) characterControl.AddWave(new ShootingScouts(new Vector2(824.0f, randPos = rand.Next(250, 350)), new Vector2(720.0f, randPos), Calculate.radian(90), characterControl));
                if (timer.Now(160)) characterControl.AddWave(new ShootingScouts(new Vector2(824.0f, randPos = rand.Next(400, 500)), new Vector2(720.0f, randPos), Calculate.radian(90), characterControl));
                #endregion
                #region 下側から
                if (timer.Now(180)) characterControl.AddWave(new ShootingScouts(new Vector2(randPos = rand.Next(550, 650), 624.0f), new Vector2(randPos, 520.0f), Calculate.radian(180), characterControl));
                if (timer.Now(200)) characterControl.AddWave(new ShootingScouts(new Vector2(randPos = rand.Next(350, 450), 624.0f), new Vector2(randPos, 520.0f), Calculate.radian(180), characterControl));
                if (timer.Now(220)) characterControl.AddWave(new ShootingScouts(new Vector2(randPos = rand.Next(150, 250), 624.0f), new Vector2(randPos, 520.0f), Calculate.radian(180), characterControl));
                #endregion
                #region 左側から
                if (timer.Now(240)) characterControl.AddWave(new ShootingScouts(new Vector2(-24.0f, randPos = rand.Next(400, 450)), new Vector2(80.0f, randPos), Calculate.radian(-90), characterControl));
                if (timer.Now(260)) characterControl.AddWave(new ShootingScouts(new Vector2(-24.0f, randPos = rand.Next(300, 350)), new Vector2(80.0f, randPos), Calculate.radian(-90), characterControl));
                if (timer.Now(280)) characterControl.AddWave(new ShootingScouts(new Vector2(-24.0f, randPos = rand.Next(100, 200)), new Vector2(80.0f, randPos), Calculate.radian(-90), characterControl));
                #endregion
            }

            if (characterControl.isWaveDead() && WaveNumber == 14 && timer.Now() > 280) { WaveNumber = 15; timer.Initialize(); }

            #endregion

            #region 突撃兵
            if (WaveNumber == 15)
            {
                if (timer.Now(60)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(150.0f, 150.0f), characterControl), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(400.0f, 150.0f), characterControl), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(650.0f, 150.0f), characterControl), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 15 && timer.Now() > 60) { WaveNumber = 16; timer.Initialize(); }

            if (WaveNumber == 16)
            {
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(150.0f, 450.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(400.0f, 450.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(650.0f, 450.0f), characterControl), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 16 && timer.Now() > 10) { WaveNumber = 17; timer.Initialize(); }

            if (WaveNumber == 17)
            {
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(150.0f, 150.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(400.0f, 150.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(650.0f, 450.0f), characterControl), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 17 && timer.Now() > 10) { WaveNumber = 18; timer.Initialize(); }

            if (WaveNumber == 18)
            {
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(150.0f, 450.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(400.0f, 450.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new OnceAttackAI(new Vector2(650.0f, 150.0f), characterControl), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 18 && timer.Now() > 10) { WaveNumber = 19; timer.Initialize(); }
            #endregion

            #region 突撃兵2
            if (WaveNumber == 19)
            {
                if (timer.Now(60)) characterControl.AddWave(new Attacks(new TwiceAttackAI(new Vector2(150.0f, 150.0f), characterControl), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Attacks(new TwiceAttackAI(new Vector2(650.0f, 150.0f), characterControl), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 19 && timer.Now() > 60) { WaveNumber = 20; timer.Initialize(); }

            if (WaveNumber == 20)
            {
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new TwiceAttackAI(new Vector2(150.0f, 450.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new TwiceAttackAI(new Vector2(650.0f, 450.0f), characterControl), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 20 && timer.Now() > 10) { WaveNumber = 21; timer.Initialize(); }

            if (WaveNumber == 21)
            {
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new TwiceAttackAI(new Vector2(150.0f, 150.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new TwiceAttackAI(new Vector2(650.0f, 450.0f), characterControl), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 21 && timer.Now() > 10) { WaveNumber = 22; timer.Initialize(); }

            if (WaveNumber == 22)
            {
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new TwiceAttackAI(new Vector2(150.0f, 450.0f), characterControl), characterControl));
                if (timer.Now(10)) characterControl.AddWave(new Attacks(new TwiceAttackAI(new Vector2(650.0f, 150.0f), characterControl), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 22 && timer.Now() > 10) { WaveNumber = 23; timer.Initialize(); }
            #endregion

            #region 砲台
            if (WaveNumber == 23)
            {
                if (timer.Now(60)) characterControl.AddWave(new LazerBatteries(new U_BatteryAI(), new Vector2(450.0f, -24.0f), Calculate.radian(0), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new LazerBatteries(new D_BatteryAI(), new Vector2(350.0f, 624.0f), Calculate.radian(180), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new LazerBatteries(new R_BatteryAI(), new Vector2(824.0f, 250.0f), Calculate.radian(90), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new LazerBatteries(new L_BatteryAI(), new Vector2(-24.0f, 350.0f), Calculate.radian(-90), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 23 && timer.Now() > 60) { WaveNumber = 24; timer.Initialize(); }

            #endregion

            #region 砲台2
            if (WaveNumber == 24)
            {
                if (timer.Now(60)) characterControl.AddWave(new BombBatteries(new U_LeftBatteryAI(), new Vector2(-24.0f, -24.0f), Calculate.radian(-45), characterControl));
                if (timer.Now(80)) characterControl.AddWave(new BombBatteries(new U_RightBatteryAI(), new Vector2(824.0f, -24.0f), Calculate.radian(45), characterControl));
                if (timer.Now(100)) characterControl.AddWave(new BombBatteries(new D_LeftBatteryAI(), new Vector2(-24.0f, 624.0f), Calculate.radian(-135), characterControl));
                if (timer.Now(120)) characterControl.AddWave(new BombBatteries(new D_RightBatteryAI(), new Vector2(824.0f, 624.0f), Calculate.radian(135), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 24 && timer.Now() > 120) { WaveNumber = 25; timer.Initialize(); sound.StopBGM(); }

            #endregion

            #region WARNING
            if (WaveNumber == 25)
            {
                if (timer.Now(60)) sound.PlaySE("Warning");
                if (timer.Now(60)) characterControl.AddWave(new Warning(new Vector2(80f, 100f), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Line(8, new Vector2(-1000f, 240f), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Line(-8, new Vector2(800f, 340f), characterControl));

                if (timer.Now(60)) characterControl.AddWave(new Line2(-12, new Vector2(800f, -125f), characterControl));
                if (timer.Now(60)) characterControl.AddWave(new Line2(12, new Vector2(-2020f, 525f), characterControl));
            }

            if (characterControl.isWaveDead() && WaveNumber == 25 && timer.Now() >= 60) { WaveNumber = 26; timer.Initialize(); }
            #endregion

            #region ボス
            if (WaveNumber == 26)
            {
                if (timer.Now(30)) sound.PlayBGM("BossBGM");
                if (timer.Now(30)) characterControl.AddWave(new Boss(new Vector2(400.0f, -24.0f), characterControl));
                if (timer.Now(30)) characterControl.AddWave(new Door(new Vector2(300.0f, 200.0f), characterControl));
                if (timer.Now(30)) isAppearBoss = true;
            }

            if (characterControl.isWaveDead() && WaveNumber == 26 && timer.Now() > 30) { isDead = true; }
            #endregion
        }

        public bool IsDead()
        {
            return isDead;
        }

        public bool IsAppearBoss()
        {
            return isAppearBoss;
        }
    }
}
