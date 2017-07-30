using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using D3Project.Device;
using D3Project.Utility;

namespace D3Project.Device
{
    class Motion
    {
        private Range range;              //範囲
        private Timer timer;              //モーション時間
        private int motionNumber;         //モーション番号
        private bool deadFlag = false;    //削除フラグ

        //表示位置を番号で管理
        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        public Motion()
        {
            Initialize(new Range(0, 0), new Timer());
        }

        public Motion(Range range, Timer timer)
        {
            Initialize(range, timer);
        }

        public void Initialize(Range range, Timer timer)
        {
            this.range = range;
            this.timer = timer;
            motionNumber = range.First();
        }

        //モーション矩形情報の追加
        public void Add(int index, Rectangle rect)
        {
            if (rectangles.ContainsKey(index)) return;
            rectangles.Add(index, rect);
        }

        //モーションの更新
        private void MotionUpdate()
        {
            motionNumber += 1;
            if (range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
                deadFlag = true;
            }
        }

        public bool OnceMotion()
        {
           return deadFlag;
        }

        //更新
        public void Update(GameTime gameTime)
        {
            if (range.IsOutOfRange()) return;

            timer.Update();
            if (timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }

        //描画範囲の取得
        public Rectangle DrawingRange()
        {
            return rectangles[motionNumber];
        }
    }
}
