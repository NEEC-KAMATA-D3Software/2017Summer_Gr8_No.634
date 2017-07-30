using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;
using D3Project.Def;
using D3Project.UI;

namespace D3Project.Character
{
    //ボス
    class Boss : Character
    {
        private List<Vector2> normalPositions;        //NORMAL用位置リスト
        private List<Vector2> starPositions;          //STAR用位置リスト
        private int current;                          //現在の番号（リスト用)

        private Vector2 previousPosition;             //直前の座標
        private Vector2 difference;                   //座標の差

        private float rot;                            //円の角度（SPIRAL用）
        private float Radius;                         //円の半径（SPIRAL用）
        private Timer appearTimer;                    //出現用
        private Timer starTimer;                      //STAR用
        private Timer infinityTimer;                  //INFINITY用
        private Timer spiralTimer;                    //SPIRAL用
        private Timer bulletTimer;                    //攻撃用
        private Timer isDeadTimer;                    //死亡時用

        private bool isOnce;                          //一回処理用フラグ（CIRCLE用）
        private bool isOnce2;                         //一回処理用フラグ（SPIRAL用）
        private bool canAttack;                       //攻撃開始フラグ

        private Sound sound;

        private Color color;                          //ヒット時の色

        //行動タイプ
        private enum MoveType {APPEAR, NORMAL, CIRCLE, INFINITY, STAR, SPIRAL, DEAD };          //行動の種類
        private MoveType moveType = MoveType.APPEAR;                     　　　　　             //初期行動

        public Boss(Vector2 position, ICharacterMediator mediator) : base("enemy128", "", position, 64.0f, mediator)
        {
            #region NORMAL
            normalPositions = new List<Vector2>()
            {
                new Vector2(400.0f, 100.0f), new Vector2(736.0f, 100.0f)
            };
            current = 0;
            #endregion

            #region STAR
            starPositions = new List<Vector2>()
            {
                new Vector2(64.0f, 300.0f), new Vector2(736.0f, 300.0f), new Vector2(200.0f, 536.0f), new Vector2(400.0f, 64.0f), new Vector2(600.0f, 536.0f),
            };
            current = 0;
            #endregion

            sound = Sound.GetInstance();

            //各種値初期化
            rot = 90;
            Radius = 0;

            //タイマー初期化
            appearTimer = new Timer(2.0f);
            starTimer = new Timer(0.5f);
            infinityTimer = new Timer(1.0f);
            spiralTimer = new Timer(0);
            bulletTimer = new Timer(0);
            isDeadTimer = new Timer(0);

            //各種フラグ初期化
            isOnce = true;
            isOnce2 = true;
            canAttack = false;
        }

        public override void Update(GameTime gameTime)
        {
            //通常時の色
            color = Color.White;

            appearTimer.Update();
            bulletTimer.addUpdate();

            //行動切り替え処理
            if (appearTimer.IsTime()) { moveType = MoveType.NORMAL; }
            if (BOSSHP.GetHP() <= 80) { moveType = MoveType.CIRCLE;   }
            if (BOSSHP.GetHP() <= 60) { moveType = MoveType.INFINITY; }
            if (BOSSHP.GetHP() <= 40) { moveType = MoveType.STAR;     }
            if (BOSSHP.GetHP() <= 20) { moveType = MoveType.SPIRAL;   }
            if (BOSSHP.GetHP() <= 0)  { moveType = MoveType.DEAD; }

            //行動タイプ
            switch (moveType)
            {
                #region APPEAR
                case MoveType.APPEAR:
                    position = new Vector2(400.0f, 300.0f);
                    //次のMoveType行動用
                    previousPosition = position;
                    break;
                #endregion

                #region NORMAL
                case MoveType.NORMAL:

                    //移動処理
                    Vector2 normalVelocity = Vector2.Normalize(normalPositions[current] - position);
                    position += normalVelocity * 5.0f;
                    if ((normalPositions[current] - position).Length() < 5.0f) current = (current + 1) % normalPositions.Count;
                    angle = Calculate.radian(0);

                    //移動位置を変更
                    if (current == 1)
                    {
                        name = "BossBound";
                        normalPositions[0] = new Vector2(64.0f, 100.0f);
                        canAttack = true;
                    }

                    //攻撃処理
                    if(canAttack && bulletTimer.Now() % 20 == 0) mediator.AddFirstCharacter(new EnemyBullet(position + new Vector2(0.0f, 50.0f), angle + Calculate.radian(180), 7.0f, mediator));

                    //次のMoveType行動用
                    previousPosition = position;

                    break;
                #endregion

                #region CIRCLE
                case MoveType.CIRCLE:
                    //移動遷移処理
                    if (isOnce)
                    {
                        Vector2 starVelocity = Vector2.Normalize(new Vector2(Screen.Width / 2, Screen.Height / 2) - previousPosition);
                        if ((new Vector2(Screen.Width / 2, Screen.Height / 2) - position).Length() > 5.0f) position += starVelocity * 3.0f;
                        else isOnce = false;
                    }



                    //姿を変化
                    if (isOnce == false)
                    {
                        name = "BossCircle2";
                        //移動処理
                        angle += Calculate.radian(2);
                    }

                        //攻撃処理
                        if (isOnce == false && bulletTimer.Now() % 15 == 0)
                    {
                        mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(0),   3.0f, mediator));
                        mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(90),  3.0f, mediator));
                        mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(-90), 3.0f, mediator));
                        mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(180), 3.0f, mediator));
                    }


                    break;
                #endregion

                #region INFINITY
                case MoveType.INFINITY:
                    //姿を変化
                    name = "BossInfinite";

                    infinityTimer.Update();

                    if (infinityTimer.IsTime())
                    {
                        //移動処理
                        double rad3 = Calculate.radian(rot);
                        double x3 = Screen.Width / 2 + 300 * Math.Cos(rad3);
                        double y3 = Screen.Height / 2 + 150 * Math.Sin(2 * rad3);
                        rot += 1.0f;
                        position.X = (float)x3;
                        position.Y = (float)y3;

                        //攻撃処理
                        if (bulletTimer.Now() % 20 == 0) mediator.AddFirstCharacter(new BombEnemyBullet(position, angle + Calculate.radian(180), 3.0f, mediator));
                    }
                        angle = Calculate.radian(0);



                    break;
                #endregion

                #region STAR
                case MoveType.STAR:
                    //姿を変化
                    name = "BossStar";

                    //移動処理
                    Vector2 velocity3 = starPositions[current] - position;
                    if (velocity3.Length() != 0.0f) velocity3.Normalize();

                    if ((starPositions[current] - position).Length() <= 5.0f)
                    {
                        //攻撃処理
                        if (bulletTimer.Now() % 5 == 0)
                        {
                            mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(180), 10.0f, mediator));
                            mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(180) + Calculate.radian(15.0f), 10.0f, mediator));
                            mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(180) - Calculate.radian(15.0f), 10.0f, mediator));
                            mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(180) + Calculate.radian(30.0f), 10.0f, mediator));
                            mediator.AddFirstCharacter(new EnemyBullet(position, angle + Calculate.radian(180) - Calculate.radian(30.0f), 10.0f, mediator));
                        }

                        starTimer.Update();
                        if (starTimer.IsTime())
                        {
                            current = (current + 1) % starPositions.Count;
                            starTimer.Initialize();
                        }

                    }
                    else
                    {
                        position += velocity3 * 5.0f;
                    }

                    if (current == 0) angle = Calculate.radian(-90);
                    if (current == 1) angle = Calculate.radian(90);
                    if (current == 2) angle = Calculate.radian(180);
                    if (current == 3) angle = Calculate.radian(0);
                    if (current == 4) angle = Calculate.radian(180);



                    previousPosition = position;
                    break;
                #endregion

                #region SPIRAL
                case MoveType.SPIRAL:
                    //姿を変化
                    name = "BossSpiral";

                    //移動処理
                    if (isOnce2)
                    {
                        Vector2 velocity4 = Vector2.Normalize(new Vector2(Screen.Width / 2, Screen.Height / 2) - previousPosition);
                        if ((new Vector2(Screen.Width / 2, Screen.Height / 2) - position).Length() > 5.0f) position += velocity4 * 3.0f;
                        else isOnce2 = false;
                    }

                    if (!isOnce2)
                    {
                        spiralTimer.addUpdate();
                        if (0 < spiralTimer.Now() && spiralTimer.Now() <= 240)
                        {
                            rot -= 4.5f;
                            Radius += 1;
                            double rad2 = Calculate.radian(rot);

                            double x2 = Screen.Width / 2 + Math.Cos(rad2) * Radius;
                            double y2 = Screen.Height / 2 + Math.Sin(rad2) * Radius;

                            position.X = (float)x2;
                            position.Y = (float)y2;
                        }

                        else if (spiralTimer.Now() <= 480)
                        {
                            rot -= 4.5f;
                            Radius -= 1;
                            double rad2 = Calculate.radian(rot);

                            double x2 = Screen.Width / 2 + Math.Cos(rad2) * Radius;
                            double y2 = Screen.Height / 2 + Math.Sin(rad2) * Radius;

                            position.X = (float)x2;
                            position.Y = (float)y2;
                        }

                        if (spiralTimer.Now() > 480)
                        {
                            spiralTimer.Initialize();
                        }



                        //攻撃処理
                        if (bulletTimer.Now() % 5 == 0) mediator.AddFirstCharacter(new EnemyBullet(position + new Vector2(0.0f, 50.0f), angle + Calculate.radian(180), 3.0f, mediator));
                    }
                        difference = new Vector2(Screen.Width / 2, Screen.Height / 2) - position;
                        angle = (float)Math.Atan2(difference.X, -difference.Y);
                        if (Radius == 0) angle = Calculate.radian(0);

                    previousPosition = position;

                    break;
                #endregion

                #region DEAD
                case MoveType.DEAD:

                    position = previousPosition;

                    break;
                    #endregion
            }

            if (BOSSHP.GetHP() <= 0)
            {
                isDeadTimer.addUpdate();
            }

            if (isDeadTimer.Now(1))
            {
                mediator.AddFirstCharacter(new BossBurstEffect(position, mediator));
                Score.add(300);
            }

            if (isDeadTimer.Now(120)) isDead = true;
        }

        public override void Hit(string type)
        {
            //ボスが攻撃可能な場合のみ、攻撃を受ける。
            if (!canAttack) ChangeType("");
            if (canAttack)
            {
                ChangeType("Enemy");
                if (type == "Bullet" || type == "Player" || type == "Barrier")
                {
                    BOSSHP.SubValue(5);
                    sound.PlaySE("Damege");
                    color = Color.SkyBlue;
                }

                if (type == "BOMB")
                {
                    BOSSHP.SubValue(0.2f);
                    color = Color.SkyBlue;
                }
            }
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, color, angle, textureCenter);
            if (moveType == MoveType.CIRCLE && isOnce == false) renderer.DrawTexture("BossCircle1", position - new Vector2(64, 64));
        }
    }
}
