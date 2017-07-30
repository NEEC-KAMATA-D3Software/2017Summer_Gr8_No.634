using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace D3Project.Device
{
    class InputState
    {
        private KeyboardState currentKey;        //現在のキー
        private KeyboardState previousKey;       //１フレーム前のキー
        private GamePadState currentButton;      //現在のボタン
        private GamePadState previousButton;     //1フレーム前のボタン

        private Vector2 velocity = Vector2.Zero; //移動速度
        private Vector2 rotation = Vector2.Zero; //回転
        private float rotate = 0.0f;
        private float angle;

        public InputState() { }

        //キー＆ボタン情報の更新
        private void Updatekey(KeyboardState keyState, GamePadState buttonState)
        {
            previousKey = currentKey;
            currentKey = keyState;

            previousButton = currentButton;
            currentButton = buttonState;
        }

        //キーが押されているか
        public bool IsKeyDown(Keys key)
        {
            bool current = currentKey.IsKeyDown(key);
            bool previous = previousKey.IsKeyDown(key);

            //現在押されていて、1フレーム前に押されていなければtrue
            return current && !previous;
        }

        //ボタンが押されているか
        public bool IsButtonDown(Buttons button)
        {
            bool current = currentButton.IsButtonDown(button);
            bool previous = previousButton.IsButtonDown(button);

            //現在押されていて、1フレーム前に押されていなければtrue
            return current && !previous;
        }

        //キーが離れたか
        public bool IsKeyUp(Keys key)
        {
            return currentKey.IsKeyUp(key);
        }

        //ボタンが離れたか
        public bool IsButtonUp(Buttons button)
        {
            return currentButton.IsButtonUp(button);
        }

        //左アナログスティックが押されているか
        public bool LeftStickDown()
        {
            return (0 != currentButton.ThumbSticks.Left.X || 0 != currentButton.ThumbSticks.Left.Y);
        }

        //移動速度の取得
        public Vector2 Velocity()
        {
            return velocity;
        }

        //キー入力のトリガー判定(1フレーム前に押されていたらfalse)
        public bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        //ボタン入力のトリガー判定(1フレーム前に押されていたらfalse)
        public bool GetButtonTrigger(Buttons button)
        {
            return IsButtonDown(button);
        }

        //キー入力の状態判定(押されていたらtrue)
        public bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        //ボタン入力の状態判定(押されていたらtrue)
        public bool GetButtonState(Buttons button)
        {
            return currentButton.IsButtonDown(button);
        }

        private void UpdateVelocity(KeyboardState keyState, GamePadState buttonState)
        {
            //移動速度をゼロベクトル(0, 0)で初期化
            velocity = Vector2.Zero;

            //キー入力処理
            if (keyState.IsKeyDown(Keys.Right)) velocity.X = 1.0f;
            if (keyState.IsKeyDown(Keys.Left))  velocity.X = -1.0f;
            if (keyState.IsKeyDown(Keys.Down))  velocity.Y = 1.0f;
            if (keyState.IsKeyDown(Keys.Up))    velocity.Y = -1.0f;

            //アナログスティック入力処理
            velocity.X += buttonState.ThumbSticks.Left.X;
            velocity.Y -= buttonState.ThumbSticks.Left.Y;

            //ベクトルの長さを単位ベクトルへ変更処理
            //（斜めも移動も同じ速度にする）
            if (velocity.Length() != 0.0f) //0でなければ処理対象
            {
                //正規化（長さを１にする）
                velocity.Normalize();
            }
        }

        //更新処理
        public void Update()
        {
            var keyState = Keyboard.GetState();
            var gamepadState = GamePad.GetState(PlayerIndex.One);
            Updatekey(keyState, gamepadState);

            //移動速度の更新
            UpdateVelocity(keyState, gamepadState);
        }
    }
}
