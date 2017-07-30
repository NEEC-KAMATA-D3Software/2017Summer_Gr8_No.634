using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;

namespace D3Project.Device
{
    //サウンドクラス(シングルトン)
    //sealedは継承禁止
    public sealed class Sound
    {
        private ContentManager contentManager;
        private Dictionary<string, Song> bgms;                         //MP3管理用
        private Dictionary<string, SoundEffect> soundEffects;          //WAV管理用
        private Dictionary<string, SoundEffectInstance> seInstances;   //WAVインスタンス管理用
        private List<SoundEffectInstance> sePlayList;                  //WAVインスタンスの再生リスト
        private string currentBGM;                                     //現在再生中のアセット名

        private static Sound sound;

        public static Sound GetInstance()
        {
            if (sound == null) sound = new Sound();
            return sound;
        }

        private Sound()
        {
            MediaPlayer.IsRepeating = true;       //BGMは繰り返し再生

            //各Dictionaryの実体生成
            bgms = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffect>();
            seInstances = new Dictionary<string, SoundEffectInstance>();

            //再生Listの実体生成
            sePlayList = new List<SoundEffectInstance>();

            //何も再生していないのでnull初期化
            currentBGM = null;
        }

        public void Initialize(ContentManager content)
        {
            contentManager = content;             //Game1のコンテンツ管理者と紐付け
        }


        private string ErrorMessage(string name)
        {
            return "再生する音データのアセット名(" + name + ")がありません\n"
                +
                "アセット名の確認、Dictionaryに登録されているか確認してください\n";
        }

        #region BGM関連処理

        //BGM(MP3)の読み込み
        public void LoadBGM(String name, string filepath = "./Sound/BGM/")
        {
            //二重登録防止
            if (bgms.ContainsKey(name)) return;

            //読み込みと追加
            bgms.Add(name, contentManager.Load<Song>(filepath + name));
        }

        //BGMが停止中か？
        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        //BGMが再生中か？
        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        //BGMが一時停止中か？
        public bool IsPausedBGM()
        {
            return (MediaPlayer.State == MediaState.Paused);
        }

        //BGMを停止
        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        //BGMを再生
        public void PlayBGM(string name)
        {
            //BGM用ディクショナリをチェック
            Debug.Assert(bgms.ContainsKey(name), ErrorMessage(name));

            //同じ曲だったら何もしない
            if (currentBGM == name) return;

            //他のBGMが再生中の場合、停止処理をする
            if (IsPlayingBGM()) StopBGM();

            //音量設定
            MediaPlayer.Volume = 0.5f;

            //再生開始
            currentBGM = name;
            MediaPlayer.Play(bgms[currentBGM]);
        }

        //BGMループフラグの変更
        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }

        #endregion

        #region WAV関連

        //WAVの読み込み
        public void LoadSE(string name, string filepath = "./Sound/SE/")
        {
            //二重登録防止
            if (soundEffects.ContainsKey(name)) return;

            //読み込みと追加
            soundEffects.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        //SEインスタンス生成
        public void CreateSEInstance(string name)
        {
            //二重登録防止
            if (seInstances.ContainsKey(name)) return;

            //WAV用ディクショナリに登録されてないとエラー
            Debug.Assert(soundEffects.ContainsKey(name),
                "先に" + name + "の読み込み処理をしてください");

            //WAVデータのインスタンス生成をし、登録
            seInstances.Add(name, soundEffects[name].CreateInstance());
        }

        //単純SE再生（連続で呼ばれた場合、音は重なる。途中停止不可）
        public void PlaySE(string name)
        {
            //WAV用ディクショナリに登録されてないとエラー
            Debug.Assert(soundEffects.ContainsKey(name), ErrorMessage(name));

            soundEffects[name].Play();
        }

        
        public void PlaySEInstance(string name, bool loopFlag = false)
        {
            //WAVインスタンス用ディクショナリに登録されてないとエラー
            Debug.Assert(seInstances.ContainsKey(name), ErrorMessage(name));

            var data = seInstances[name];
            data.IsLooped = loopFlag;
            data.Play();
            sePlayList.Add(data);
        }

        //sePlayListにある再生中の音を停止
        public void StoppedSE()
        {
            foreach (var se in sePlayList)
            {
                if (se.State == SoundState.Playing) se.Stop();
            }
        }

        //sePlayListにある再生中の音を一時停止
        public void PausedSE(string name)
        {
            foreach (var se in sePlayList)
            {
                if (se.State == SoundState.Playing) se.Pause();
            }
        }

        //停止している音の削除
        public void RemoveSE()
        {
            //停止中のモノはListから削除
            sePlayList.RemoveAll(se => (se.State == SoundState.Stopped));
        }

        #endregion

        public void Unload()
        {
            bgms.Clear();
            soundEffects.Clear();
            sePlayList.Clear();
        }
    }
}