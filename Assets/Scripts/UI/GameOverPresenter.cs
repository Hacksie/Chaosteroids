using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign.UI
{
    public class GameOverPresenter : AbstractPresenter
    {
        [SerializeField] private UnityEngine.UI.Text score;
        [SerializeField] private UnityEngine.UI.Text highscore;


        public override void Repaint()
        {
            score.text = GameManager.Instance.Score.ToString("N0");
            highscore.text = GameManager.Instance.Score.ToString("N0");

        }

        public void ReplayClickEvent()
        {
            GameManager.Instance.SetPlaying();
        }

        public void QuitClickEvent()
        {
            GameManager.Instance.SetMenu();
        }
    }
}