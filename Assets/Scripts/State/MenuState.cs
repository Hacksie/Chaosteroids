using UnityEngine;

namespace HackedDesign
{
    public class MenuState : IState
    {
        private UI.AbstractPresenter menuPresenter;
        private GameObject fakePlayer;

        public MenuState(GameObject fakePlayer, UI.AbstractPresenter menuPresenter)
        {
            this.fakePlayer = fakePlayer;
            this.menuPresenter = menuPresenter;
        }

        public bool Playing => false;

        public void Begin()
        {
            this.menuPresenter.Show();
            this.menuPresenter.Repaint();
            this.fakePlayer.SetActive(true);
        }

        public void End()
        {
            this.menuPresenter.Hide();
            this.fakePlayer.SetActive(false);
        }

        public void FixedUpdate()
        {
            
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            this.menuPresenter.Repaint();
            
        }
    }

}