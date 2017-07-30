using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.Scene
{
    class SceneManager
    {
        private Dictionary<Scene, IScene> scenes = new Dictionary<Scene, IScene>();    //シーン管理用ディクショナリ
        private IScene currentScene = null;                                            //現在のシーン

        public SceneManager() { }

        public void Add(Scene name, IScene scene)
        {
            //多重登録防止
            if (scenes.ContainsKey(name)) return;

            //シーンを追加
            scenes.Add(name, scene);
        }

        public void Change(Scene name)
        {
            //何らかのシーンが登録されていたら現在のシーンの終了処理を行う
            if (currentScene != null) currentScene.Shutdown();

            //ディクショナリから次のシーンを取り出し、現在のシーンに設定と初期化
            currentScene = scenes[name];
            currentScene.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            //現在のシーンがまだ登録されていなければ更新しない
            if (currentScene == null) return;

            //現在のシーンを更新
            currentScene.Update(gameTime);
            //現在のシーンが終了していたら、次のシーンを取得して、シーン切り替え
            if (currentScene.IsEnd()) Change(currentScene.Next());
        }

        public void Draw(Renderer renderer)
        {
            //現在のシーンがまだ登録されていなければ描画しない
            if (currentScene == null) return;

            //現在のシーンを描画
            currentScene.Draw(renderer);
        }
    }
}
