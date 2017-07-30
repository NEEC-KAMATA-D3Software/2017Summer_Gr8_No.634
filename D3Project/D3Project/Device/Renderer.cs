using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics; //Assert用

namespace D3Project.Device
{
    class Renderer
    {
        private GraphicsDevice graphicsDevice;                                                  //グラフィック機器管理者
        private SpriteBatch spriteBatch;                                                        //描画オブジェクト
        private ContentManager contentManager;                                                  //コンテンツ管理者
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();   //画像管理用ディクショナリ

        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void LoadTexture(string name, string filepath = "./")
        {
            //ガード節
            //Dictionaryへの２重登録を回避
            if (textures.ContainsKey(name))
            {
#if DEBUG //DEBUGモードの時のみ有効
                System.Console.WriteLine("この" + name + "はKeyで、すでに登録してます");
#endif
                //処理終了
                return;
            }
            //画像の読み込みとDictionaryにアセット名と画像を追加
            textures.Add(name, contentManager.Load<Texture2D>(filepath + name));
        }

        public void LoadTexture(string name, Texture2D texture)
        {
            //ガード節
            //Dictionaryへの２重登録を回避
            if (textures.ContainsKey(name))
            {
#if DEBUG //DEBUGモードの時のみ有効
                System.Console.WriteLine("この" + name + "はKeyで、すでに登録してます");
#endif
                //処理終了
                return;
            }
            textures.Add(name, texture);
        }

        public void Unload()
        {
            textures.Clear();
        }

        public void Begin()
        {
            spriteBatch.Begin();
        }

        public void End()
        {
            spriteBatch.End();
        }

        //画像の描画
        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }

        //画像の描画（色指定）
        public void DrawTexture(string name, Vector2 position, Color color, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(textures[name], position, color * alpha);
        }

        //画像の描画（指定範囲）
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(
                textures[name],        //画像
                position,              //位置
                rect,                  //矩形の範囲（左上の座標x, y, 幅, 高さ）
                Color.White * alpha    //透過
                );
        }


        //画像の描画（指定範囲&色指定）
        public void DrawTexture(string name, Vector2 position, Rectangle rect, Color color, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(
                textures[name],        //画像
                position,              //位置
                rect,                  //矩形の範囲（左上の座標x, y, 幅, 高さ）
                color * alpha          //透過
                );
        }


        //画像の描画（拡大縮小対応版）
        public void DrawTexture(string name, Vector2 position, Vector2 scale,float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(
                textures[name],        //画像
                position,              //位置
                null,                  //切り取り範囲
                Color.White * alpha,   //透過
                0.0f,                  //回転
                Vector2.Zero,          //回転軸の位置
                scale,                 //拡大縮小
                SpriteEffects.None,    //表示反転効果
                0.0f                   //スプライト表示深度
                );
        }

        //画像の描画（拡大縮小&回転軸指定対応版）
        public void DrawTexture(string name, Vector2 position, Vector2 origin, Vector2 scale, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(
                textures[name],        //画像
                position,              //位置
                null,                  //切り取り範囲
                Color.White * alpha,   //透過
                0.0f,                  //回転
                origin,                //回転軸の位置
                scale,                 //拡大縮小
                SpriteEffects.None,    //表示反転効果
                0.0f                   //スプライト表示深度
                );
        }

        //画像の描画（回転対応版）
        public void DrawTexture(string name, Vector2 position, float rotation, Vector2 origin, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(
                textures[name],        //画像
                position,              //位置
                null,                  //切り取り範囲
                Color.White * alpha,   //透過
                rotation,              //回転
                origin,                //回転軸の位置
                1.0f,                  //拡大縮小
                SpriteEffects.None,    //表示反転効果
                0.0f                   //スプライト表示深度
                );
        }

        //画像の描画（回転対応&色指定版）
        public void DrawTexture(string name, Vector2 position, Color color, float rotation, Vector2 origin, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(
                textures[name],        //画像
                position,              //位置
                null,                  //切り取り範囲
                color * alpha,         //透過
                rotation,              //回転
                origin,                //回転軸の位置
                1.0f,                  //拡大縮小
                SpriteEffects.None,    //表示反転効果
                0.0f                   //スプライト表示深度
                );
        }

        //画像の描画（ゲージの描画用）
        public void DrawTexture(string name, Rectangle rect, Color color, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");
            spriteBatch.Draw(
                textures[name],        //画像
                rect,                  //矩形の範囲（左上の座標x, y, 幅, 高さ）
                color * alpha    //透過
                );
        }

        //画像の描画（振動用）
        public void DrawEffect(Texture2D gameContent, Vector2 position, Color color, Vector2 origin, float scale, float alpha = 1.0f)
        {
            spriteBatch.Draw(
                gameContent,
                position,
                null,
                color * alpha,
                0.0f,
                origin,
                scale,
                SpriteEffects.None,
                1.0f);
        }



        //数字の描画（簡易版）
        public void DrawNumber(string name, Vector2 position, int number, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            number = Math.Max(number, 0);
            foreach (var n in number.ToString())
            {
                spriteBatch.Draw(
                    textures[name],
                    position,
                    new Rectangle((n - '0') * 32, 0, 32, 64),
                    Color.White * alpha
                    );
                position.X += 32;
            }
        }

        //数字の描画（簡易版）
        public void DrawNumber(string name, Vector2 position, int number, Color color, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            number = Math.Max(number, 0);
            foreach (var n in number.ToString())
            {
                spriteBatch.Draw(
                    textures[name],
                    position,
                    new Rectangle((n - '0') * 32, 0, 32, 64),
                    color * alpha
                    );
                position.X += 32;
            }
        }


        //数字の描画（詳細版）
        public void DrawNumber(string name, Vector2 position, string number, int digit, float alpha = 1.0f)
        {
            //登録されているキーがなければエラー表示
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違ってませんか？\n" +
                "LoadTextureで読み込んでますか？\n" +
                "プログラムを確認してください");

            for(int i = 0; i < digit; i++)
            {
                if(number[i] == '.')
                {
                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle(10 * 32, 0, 32, 64),
                        Color.White * alpha
                        );
                }
                else
                {
                    char n = number[i];

                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle((n - '0') * 32, 0, 32, 64),
                        Color.White * alpha
                        );
                }
            position.X += 32;
            }
        }
    }
}