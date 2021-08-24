

namespace HackedDesign
{
    public class GameOverState : IState
    {
        private UI.AbstractPresenter gameoverPresenter;
        public GameOverState(UI.AbstractPresenter gameoverPresenter)
        {
            this.gameoverPresenter = gameoverPresenter;
        }

        public bool Playing => false;

        public void Begin()
        {
            this.gameoverPresenter.Show();
            this.gameoverPresenter.Repaint();
            
        }

        public void End()
        {
            this.gameoverPresenter.Hide();
            GameManager.Instance.Pool.Reset();            
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