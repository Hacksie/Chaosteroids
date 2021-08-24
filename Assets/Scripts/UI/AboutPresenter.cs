using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign.UI
{
    public class AboutPresenter : AbstractPresenter
    {

        public override void Repaint()
        {

        }

        public void QuitClickEvent()
        {
            GameManager.Instance.SetMenu();
        }
    }
}