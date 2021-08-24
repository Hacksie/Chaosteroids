using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign.UI
{
    public class HudPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Text score;
        [SerializeField] UnityEngine.UI.Text level;
        [SerializeField] UnityEngine.UI.Text countdown;

        void Update()
        {
            Repaint();
        }

        public override void Repaint()
        {
            score.text = GameManager.Instance.Score.ToString("N0");
            level.text = GameManager.Instance.Level.ToString("N0");
            
            if(GameManager.Instance.GameType == GameManager.GameplayType.Crazy || GameManager.Instance.GameType == GameManager.GameplayType.Chaos)
            {
                countdown.text = GameManager.Instance.SpawnCountdown.ToString("N0");
            }
            else
            {
                countdown.text = "";
            }
        }
    }
}