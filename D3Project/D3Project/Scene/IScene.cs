using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;

namespace D3Project.Scene
{
    interface IScene
    {
        void Initialize();                 //初期化
        void Update(GameTime gameTime);    //更新
        void Draw(Renderer renderer);      //描画
        void Shutdown();                   //終了

        bool IsEnd();                      //終了チェック
        Scene Next();                      //次のシーン番号
    }
}
