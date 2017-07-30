using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Utility;
using D3Project.Device;
using D3Project.Def;

namespace D3Project.Scene
{
    class SceneFader : IScene
    {
        //フェードシーン状態の列挙型
        private enum SceneFadeState
        {
            In,
            Out,
            None
        };

        private Timer timer;                     //フェード時間
        private static float FADE_TIMER = 2.0f;
        private SceneFadeState state;
        private IScene scene;                    //現在のシーン
        private bool isEnd = false;              //終了フラグ

        public SceneFader(IScene scene)
        {
            this.scene = scene;
        }

        public void Initialize()
        {
            scene.Initialize();
            state = SceneFadeState.In;
            timer = new Timer(FADE_TIMER);
            isEnd = false;
        }

        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case SceneFadeState.In:
                    UpdateFadeIn(gameTime);
                    break;
                case SceneFadeState.Out:
                    UpdateFadeOut(gameTime);
                    break;
                case SceneFadeState.None:
                    UpdateFadeNone(gameTime);
                    break;
            }
        }

        public void Draw(Renderer renderer)
        {
            switch (state)
            {
                case SceneFadeState.In:
                    DrawFadeIn(renderer);
                    break;
                case SceneFadeState.Out:
                    DrawFadeOut(renderer);
                    break;
                case SceneFadeState.None:
                    DrawFadeNone(renderer);
                    break;
            }
        }

        private void UpdateFadeIn(GameTime gameTime)
        {
            scene.Update(gameTime);
            if (scene.IsEnd()) state = SceneFadeState.Out;

            timer.Update();
            if (timer.IsTime()) state = SceneFadeState.None;
        }

        private void UpdateFadeOut(GameTime gameTime)
        {
            scene.Update(gameTime);
            if (scene.IsEnd()) state = SceneFadeState.Out;

            timer.Update();
            if (timer.IsTime()) isEnd = true;
        }

        private void UpdateFadeNone(GameTime gameTime)
        {
            scene.Update(gameTime);
            if (scene.IsEnd())
            {
                state = SceneFadeState.Out;
                timer.Initialize();
            }
        }

        private void DrawFadeIn(Renderer renderer)
        {
            scene.Draw(renderer);
            DrawEffect(renderer, timer.Rate());
        }

        private void DrawFadeOut(Renderer renderer)
        {
            scene.Draw(renderer);
            DrawEffect(renderer, 1.0f - timer.Rate());
        }

        private void DrawFadeNone(Renderer renderer)
        {
            scene.Draw(renderer);
        }

        private void DrawEffect(Renderer renderer, float alpha)
        {
            renderer.Begin();
            renderer.DrawTexture(
                "fade",
                Vector2.Zero,
                new Vector2(Screen.Width, Screen.Height),
                alpha);
            renderer.End();
        }

        public bool IsEnd()
        {
            return isEnd;
        }

        public Scene Next()
        {
            return scene.Next();
        }

        public void Shutdown()
        {
            scene.Shutdown();
        }
    }
}
