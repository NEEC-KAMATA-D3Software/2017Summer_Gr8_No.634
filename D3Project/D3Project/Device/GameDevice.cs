using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace D3Project.Device
{
    class GameDevice
    {
        private Renderer renderer;  //描画管理
        private InputState input;   //入力管理

        public GameDevice(ContentManager contentManager, GraphicsDevice graphics)
        {
            renderer = new Renderer(contentManager, graphics);  //描画管理の実体生成
            input = new InputState();                           //入力管理の実体生成
        }

        public void Initialize()
        {

        }

        public void Update(GameTime gameTime)
        {
            input.Update();
        }

        public Renderer GetRenderer()
        {
            return renderer;
        }

        public InputState GetInputState()
        {
            return input;
        }
    }
}
