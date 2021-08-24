using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign.UI
{
    public class MenuPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Button normalButton;
        [SerializeField] UnityEngine.UI.Button eliminationButton;
        [SerializeField] UnityEngine.UI.Button chaosButton;
        [SerializeField] UnityEngine.UI.Button quitButton;


        void Update()
        {
            Repaint();
        }
        public override void Repaint()
        {
            switch (GameManager.Instance.GameType)
            {
                case GameManager.GameplayType.Chaos:
                    chaosButton.Select();
                    break;
                case GameManager.GameplayType.Elimination:
                    eliminationButton.Select();
                    break;
                case GameManager.GameplayType.Normal:
                    normalButton.Select();
                    break;
            }

            if(Application.platform == RuntimePlatform.WebGLPlayer)
            {
                quitButton.interactable = false;
            }
        }

        public void PlayClickEvent()
        {
            GameManager.Instance.SetPlaying();
        }

        public void NormalClickEvent()
        {
            GameManager.Instance.GameType = GameManager.GameplayType.Normal;
            Repaint();
        }

        public void EliminationClickEvent()
        {
            GameManager.Instance.GameType = GameManager.GameplayType.Elimination;
            Repaint();
        }

        public void ChaosClickEvent()
        {
            GameManager.Instance.GameType = GameManager.GameplayType.Chaos;
            Repaint();
        }

        public void SoundClickEvent()
        {

        }

        public void QuitClickEvent()
        {
            Application.Quit();
        }
    }
}