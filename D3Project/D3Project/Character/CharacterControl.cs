using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.Character
{
    class CharacterControl : ICharacterMediator
    {
        private LinkedList<Character> characters;      //キャラ管理
        private LinkedList<Character> waveCharacters;  //Wave管理(確認用)
        private List<Character> newFirstCharacters;    //先頭に追加するキャラ管理
        private List<Character> newLastCharacters;     //末尾に追加するキャラ管理
        
        public CharacterControl() { }

        public void Initialize()
        {
            if (characters != null) characters.Clear();
            characters = new LinkedList<Character>();
            waveCharacters = new LinkedList<Character>();
            newFirstCharacters = new List<Character>();
            newLastCharacters = new List<Character>();
        }

        //キャラを最初に追加
        public bool AddFirst(Character character)
        {
            characters.AddFirst(character);
            return true;
        }

        //キャラを最後に追加
        public bool AddLast(Character character)
        {
            characters.AddLast(character);
            return true;
        }

        //Wave追加
        public bool AddWave(Character character)
        {
            characters.AddLast(character);
            waveCharacters.AddLast(character);
            return true;
        }

        public void Update(GameTime gameTime)
        {
            foreach (Character c in characters) c.Update(gameTime);

            New();
            Hit();
            Remove();
        }

        //新規にキャラクター生成
        private void New()
        {
            foreach (Character c in newFirstCharacters) AddFirst(c);
            newFirstCharacters.Clear();
            foreach (Character c in newLastCharacters) AddLast(c);
            newLastCharacters.Clear();
        }

        //キャラクター間の当たり判定
        private void Hit()
        {
            foreach(Character c1 in characters)
            {
                foreach(Character c2 in characters)
                {
                    if(c1.Collision(c2))
                    {
                        c1.Hit(c2.typeName());
                        c2.Hit(c1.typeName());
                    }
                }
            }
        }

        //死亡したキャラクターの削除
        private void Remove()
        {
            LinkedListNode<Character> node = characters.First;
            while (node != null)
            {
                LinkedListNode<Character> next = node.Next;
                if (node.Value.IsDead()) characters.Remove(node);
                node = next;
            }
        }

        //Waveが削除されたか確認
        public bool isWaveDead()
        {
            var wave = characters.Intersect(waveCharacters).ToList();
            return wave.Count() == 0;
        }

        //プレイヤーの死亡判定
        public bool IsPlayerDead()
        {
            foreach (Character c in characters)
            {
                if (c is Player) return false;
            }
            return true;
        }

        public void Draw(Renderer renderer)
        {
            foreach (Character c in characters) c.Draw(renderer);
        }

        //キャラを最初に追加
        public void AddFirstCharacter(Character character)
        {
            newFirstCharacters.Add(character);
        }

        //キャラを最後に追加
        public void AddLastCharacter(Character character)
        {
            newLastCharacters.Add(character);
        }

        //プレイヤー座標の取得
        public Vector2 GetPlayerPosition()
        {
            foreach(Character c in characters)
            {
                if (c is Player) return c.GetPosition();
            }
            return Vector2.Zero;
        }

        //エネミーの取得
        public Character GetEnemy()
        {
            foreach (Character c in characters)
            {
                if (c.typeName() == "Enemy") return c;
            }
            return null;
        }

        public float GetPlayerAngle()
        {
            foreach(Character c in characters)
            {
                if (c is Player) return c.GetAngle();
            }
            return 0.0f;
        }
    }
}
