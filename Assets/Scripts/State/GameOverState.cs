
using UnityEngine;

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
            SaveHighScore();

            this.gameoverPresenter.Show();
            this.gameoverPresenter.Repaint();

        }

        public void End()
        {
            this.gameoverPresenter.Hide();
            GameManager.Instance.Reset();
        }

        private void SaveHighScore()
        {
            var highscore = PlayerPrefs.GetInt("Score" + GameManager.Instance.GameType.ToString(), 0);

            if (GameManager.Instance.Score > highscore)
            {
                PlayerPrefs.SetInt("Score" + GameManager.Instance.GameType.ToString(), GameManager.Instance.Score);
            }
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