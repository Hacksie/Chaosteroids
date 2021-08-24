using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign.UI
{
    public class GameOverPresenter : AbstractPresenter
    {



        public override void Repaint()
        {

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