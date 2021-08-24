

namespace HackedDesign
{
    public class MenuState : IState
    {
        private UI.AbstractPresenter menuPresenter;

        public MenuState(UI.AbstractPresenter menuPresenter)
        {
            this.menuPresenter = menuPresenter;
        }

        public bool Playing => false;

        public void Begin()
        {
            this.menuPresenter.Show();
            this.menuPresenter.Repaint();
        }

        public void End()
        {
            this.menuPresenter.Hide();
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