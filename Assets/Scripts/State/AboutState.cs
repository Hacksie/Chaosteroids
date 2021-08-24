

namespace HackedDesign
{
    public class AboutState : IState
    {
        private UI.AbstractPresenter aboutPresenter;
        public AboutState(UI.AbstractPresenter aboutPresenter)
        {
            this.aboutPresenter = aboutPresenter;
        }

        public bool Playing => false;

        public void Begin()
        {
            this.aboutPresenter.Show();
            this.aboutPresenter.Repaint();
            
        }

        public void End()
        {
            this.aboutPresenter.Hide();
        }

        public void FixedUpdate()
        {
            
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            
        }
    }

}