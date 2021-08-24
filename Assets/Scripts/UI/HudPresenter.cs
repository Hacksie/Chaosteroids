using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign.UI
{
    public class HudPresenter : AbstractPresenter
    {
        [SerializeField] UnityEngine.UI.Text score;

        void Update()
        {
            Repaint();
        }
        public override void Repaint()
        {
            score.text = GameManager.Instance.Score.ToString("N0");
        }
    }
}