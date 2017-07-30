using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Def;

namespace D3Project.Character
{
    abstract class Character
    {
        protected string name;                           //アセット名
        protected string type;                           //タイプ名
        protected Vector2 position;                      //位置
        protected float radius;                          //半径
        protected Vector2 textureCenter = Vector2.Zero;  //テクスチャの中心座標
        protected float angle;                           //向き
        protected bool isDead = false;                   //死亡フラグ
        protected ICharacterMediator mediator;           //仲介人

        public virtual void Update(GameTime gameTime)
        {
            //画面外に出たら死亡フラグをtrueに
            if (position.X < 0.0f - radius || (Screen.Width + radius) < position.X || position.Y < 0.0f - radius || (Screen.Height + radius) < position.Y) isDead = true;
        }
        public abstract void Hit(string type);

        public Character(string name, string type, Vector2 position, float radius, ICharacterMediator mediator)
        {
            this.name = name;
            this.type = type;
            this.position = position;
            this.radius = radius;
            this.textureCenter = new Vector2(radius, radius);
            this.mediator = mediator;
        }

        public Character(string name, string type, float radius, ICharacterMediator mediator)
        {
            this.name = name;
            this.type = type;
            position = new Vector2(-24.0f, -24.0f);
            this.radius = radius;
            this.textureCenter = new Vector2(radius, radius);
            this.mediator = mediator;
        }



        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position - textureCenter);
        }

        public bool IsDead()
        {
            return isDead;
        }

        //衝突判定
        public bool Collision(Character character)
        {
            if (Vector2.Distance(this.position, character.position) <= (this.radius + character.radius))
            {
                return true;
            }
            return false;
        }

        //位置の取得
        public Vector2 GetPosition()
        {
            return position;
        }

        //向きの取得
        public float GetAngle()
        {
            return angle;
        }

        //位置の受け渡し(引数で渡された、変数に自分の位置を渡す)
        public void SetPosition(ref Vector2 other)
        {
            other = position;
        }

        //タイプ名の取得
        public string typeName()
        {
            return type;
        }

        //タイプ名の変更
        public void ChangeType(string type)
        {
            this.type = type;
        }
    }
}
