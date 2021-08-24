using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign.UI
{
    public class MenuPresenter : AbstractPresenter
    {
        [SerializeField] private UnityEngine.UI.Button normalButton;
        [SerializeField] private UnityEngine.UI.Button crazyButton;
        [SerializeField] private UnityEngine.UI.Button chaosButton;
        [SerializeField] private UnityEngine.UI.Button quitButton;
        [SerializeField] private UnityEngine.UI.Image musicIcon;
        [SerializeField] private Sprite musicOn;
        [SerializeField] private Sprite musicOff;
        [SerializeField] private UnityEngine.Audio.AudioMixer audioMixer;

        void Update() => Repaint();

        public override void Repaint()
        {
            switch (GameManager.Instance.GameType)
            {
                case GameManager.GameplayType.Chaos:
                    chaosButton.Select();
                    break;
                case GameManager.GameplayType.Crazy:
                    crazyButton.Select();
                    break;
                case GameManager.GameplayType.Normal:
                    normalButton.Select();
                    break;
            }

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                quitButton.interactable = false;
            }

            musicIcon.sprite = GameManager.Instance.Sound ? musicOn : musicOff;
        }

        public void PlayClickEvent() => GameManager.Instance.SetPlaying();

        public void NormalClickEvent()
        {
            GameManager.Instance.GameType = GameManager.GameplayType.Normal;
            Repaint();
        }

        public void CrazyClickEvent()
        {
            GameManager.Instance.GameType = GameManager.GameplayType.Crazy;
            Repaint();
        }

        public void ChaosClickEvent()
        {
            GameManager.Instance.GameType = GameManager.GameplayType.Chaos;
            Repaint();
        }

        public void AboutClickEvent() => GameManager.Instance.SetAbout();

        public void SoundClickEvent()
        {
            GameManager.Instance.Sound = !GameManager.Instance.Sound;
            audioMixer.SetFloat("Master", GameManager.Instance.Sound ? 0 : -80);
        }
        public void QuitClickEvent() => Application.Quit();

    }
}