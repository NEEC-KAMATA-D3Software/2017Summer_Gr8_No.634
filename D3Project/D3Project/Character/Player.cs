using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using D3Project.Device;
using D3Project.Def;
using D3Project.Utility;
using D3Project.UI;

namespace D3Project.Character
{
    class Player : Character
    {
        private InputState input;                             
        private Sound sound;                                  
                                                              
        private Vector2 previousPosition;                     //1フレーム前の位置
        private Vector2 velocity = Vector2.Zero;              //プレイヤー移動量
        private float ACCELERATION = 0.5f;                    //加速度(移動処理)
        private float RESIST = 0.08f;                         //抵抗(移動処理)
        private float alpha = 0.0f;                           //透過(点滅用)
        private bool isDamaged = false;                       //ダメージ判定フラグ
        private bool isInvincible = false;                    //無敵判定フラグ
        private bool isBullet = false;

        private bool activBARRIER = false;
        private bool activINVINCIBLE = false;
        private bool First = false;

        Timer BulletTimer = new Timer(0.0f);                  //弾のクールタイム
        Timer timer = new Timer(2.0f);                        //無敵時間
        Timer SkillTimer = new Timer(0.0f);                   //スキル持続時間
        Timer DashTiemr = new Timer(2.0f);                    //ダッシュスキル連続使用制限
        Timer BarrierTimer = new Timer(10.0f);                //バリアスキル連続使用制限
        Timer InvincibleTiemr = new Timer(5.0f);              //無敵スキル連続使用制限
        Timer FirstTimer = new Timer(1.78f);                  //初期出現時間

        //攻撃スキル
        private enum BulletType { NORMAL, THREEWAY, HOMING, BOMB };    //弾の種類
        private BulletType bulletType = BulletType.NORMAL;             //初期弾

        //特殊スキル
        private enum PlayerState { NORMAL, BARRIER, INVINCIBLE};        //プレイヤーの状態
        private PlayerState playerState = PlayerState.NORMAL;           //初期状態

        public Player(InputState input, ICharacterMediator mediator) : base("player48", "Player", new Vector2(Screen.Width / 2, Screen.Height / 2), 24.0f, mediator)
        {
            this.input = input;
            sound = Sound.GetInstance();
        }

        public override void Update(GameTime gameTime)
        {
            //移動処理
            move();

            if (First == false)
            {
                FirstTimer.Update();
                if (FirstTimer.IsTime())
                {
                    alpha = 1.0f;
                    position = new Vector2(Screen.Width / 2, Screen.Height / 2);
                    FirstTimer.Initialize();
                    First = true;
                }
            }

            //攻撃を受けていたら一定時間点滅
            if (isDamaged == true)
            {
                timer.Update();
                ChangeType("damagedPlayer");
                if (timer.Now() % 10 == 0) alpha = 1.0f;
                else if (timer.Now() % 5 == 0) alpha = 0f;
            }

            //攻撃を受けていなかったら弾生成＆特殊スキル使える
            if (isDamaged == false)
            {
                ChangeType("Player");
                makeBullet();
                useSpecialSkill();
            }    
        }

        public override void Hit(string type)
        {
            if (type == "Enemy" || type == "EnemyBullet" || type == "EnemyBOMB")
            {
                if (isDamaged == false && isInvincible == false)
                {
                    mediator.AddFirstCharacter(new BurstEffect(position, mediator));

                    //残機１つ減らす
                    Heart.Damage();

                    if (Heart.GetHP() == 0) isDead = true;
                    else isDamaged = true;

                    //初期化処理
                    bulletType = BulletType.NORMAL;
                    position = new Vector2(Screen.Width / 2, Screen.Height / 2);
                    //ここまで
                }
            }

            if (timer.IsTime())
            {
                timer.Initialize();
                isDamaged = false;
            }

            //アイテム取得時に弾変更
            if (type == "THREEWAY_Item")
            {
                bulletType = BulletType.THREEWAY;
                sound.PlaySE("Item");
            }
            if (type == "HOMING_Item")
            {
                bulletType = BulletType.HOMING;
                sound.PlaySE("Item");
            }
            if (type == "BOMB_Item")
            {
                bulletType = BulletType.BOMB;
                sound.PlaySE("Item");
            }
            if (type == "Barrier") activBARRIER = true;
            else activBARRIER = false;
        }

        private void move()
        {
            //移動処理
            previousPosition = position;
            velocity += input.Velocity() * ACCELERATION - velocity * RESIST;
            position += velocity;

            //画面外に出ないように
            var min = new Vector2(radius, radius);
            var max = new Vector2(Screen.Width - radius, Screen.Height - radius);
            position = Vector2.Clamp(position, min, max);

            //移動キーが押されている時に角度計算
            if (input.LeftStickDown() || input.GetKeyState(Keys.Up) || input.GetKeyState(Keys.Down) || input.GetKeyState(Keys.Right) || input.GetKeyState(Keys.Left))
            {
                angle = (float)Math.Atan2(position.X - previousPosition.X, -(position.Y - previousPosition.Y));
            }
        }

        private void makeBullet()
        {

            if (isBullet == true) BulletTimer.addUpdate();

            if (BulletTimer.Now() >= 50)
            {
                BulletTimer.Initialize();
                isBullet = false;
            }

                //弾生成
                if (input.IsButtonDown(Buttons.B) && First == true 
                || input.IsKeyDown(Keys.Space) && First == true)
            {
                

                //弾の種類取得と生成
                switch (bulletType)
                {
                    case BulletType.NORMAL:
                        sound.PlaySE("player_shot");
                        mediator.AddFirstCharacter(new Bullet(position, angle, 10.0f, mediator));
                        break;
                    case BulletType.THREEWAY:
                        sound.PlaySE("player_shot");
                        mediator.AddFirstCharacter(new Bullet(position, angle, 10.0f, mediator));
                        mediator.AddFirstCharacter(new Bullet(position, angle + Calculate.radian(15.0f), 10.0f, mediator));
                        mediator.AddFirstCharacter(new Bullet(position, angle - Calculate.radian(15.0f), 10.0f, mediator));
                        break;
                    case BulletType.HOMING:
                        if (isBullet == false)
                        {
                            sound.PlaySE("player_shot");
                            mediator.AddFirstCharacter(new HomingBullet(position, angle, 7.0f, mediator));
                            isBullet = true;
                        }
                        break;
                    case BulletType.BOMB:
                        if (isBullet == false)
                        {
                            sound.PlaySE("player_shot");
                            mediator.AddFirstCharacter(new BombBullet(position, angle, 10.0f, mediator));
                            isBullet = true;
                        }
                        break;
                }
            }
        }

        private void useSpecialSkill()
        {
            SkillTimer.Update();
            //バリアスキル
            if (input.IsButtonDown(Buttons.A) || input.IsKeyDown(Keys.A))
            {
                playerState = PlayerState.BARRIER;
            }

            //無敵スキル
            if (input.IsButtonDown(Buttons.Y) || input.IsKeyDown(Keys.Y))
            {
                playerState = PlayerState.INVINCIBLE;
            }

            switch (playerState)
            {
                case PlayerState.BARRIER:
                    //50以上ポイントがあれば実行
                    if (Gauge.hasValue(50) && activBARRIER == false && First == true )
                    {
                        Gauge.SubValue(50);
                        mediator.AddLastCharacter(new Barrier(position, mediator));
                        playerState = PlayerState.NORMAL;
                    }
                    break;
                case PlayerState.INVINCIBLE:
                    //100以上ポイントがあれば実行(=満タン時)
                    if (Gauge.hasValue(100) && activINVINCIBLE == false)
                    {
                        SkillTimer = new Timer(5.0f); //スキル持続時間
                        Gauge.SubValue(100);
                        mediator.AddLastCharacter(new InvincibleEffect(position, mediator));
                        activINVINCIBLE = true;
                        isInvincible = true;          //無敵
                        playerState = PlayerState.NORMAL;
                    }
                    break;
            }

            //スキル持続時間が終わったら元に戻す
            if (SkillTimer.IsTime())
            {
                activBARRIER = false;
                activINVINCIBLE = false;
                isInvincible = false;

            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, angle, textureCenter, alpha);
        }
    }
}
