using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class GameManager : MonoBehaviour
    {
        [Header("Referenced Game Objects")]
        [SerializeField] private Pool pool = null;
        [SerializeField] private PlayerController player = null;
        [SerializeField] private GameObject fakePlayer = null;

        [Header("UI")]
        [SerializeField] private UI.AbstractPresenter menuPanel = null;
        [SerializeField] private UI.AbstractPresenter gameoverPanel = null;
        [SerializeField] private UI.AbstractPresenter aboutPanel = null;
        [SerializeField] private UI.AbstractPresenter hudPanel = null;

        [Header("State")]
        [SerializeField] private int score;
        [SerializeField] private int gametime = 0;
        [SerializeField] private float gameStart = 0;
        [SerializeField] private GameplayType gameType = GameplayType.Chaos;
        [SerializeField] private bool sound = false;

        private IState state = new EmptyState();

        public IState State
        {
            get
            {
                return state;
            }
            private set
            {
                state.End();
                state = value;
                state.Begin();
            }
        }

        public Pool Pool { get { return pool; } private set { pool = value; } }
        public PlayerController Player { get { return player; } private set { player = value; } }
        public int Score { get { return score; } private set { score = value; }}
        public int GameTime { get { return gametime; } set { gametime = value; }}
        public float GameStart { get { return gameStart;} private set { gameStart = value; }}
        public GameplayType GameType { get { return gameType; } set { gameType = value; }}
        public bool Sound { get { return sound; } set { sound = value; }}

        public static GameManager Instance { get; private set; }

        public void SetPlaying() 
        { 
            switch(GameType)
            {
                case GameplayType.Normal:
                State = new PlayingNormalState(Player, this.hudPanel);
                break;
                case GameplayType.Crazy:
                State = new PlayingCrazyState(Player, this.hudPanel);
                break;
                case GameplayType.Chaos:
                State = new PlayingChaosState(Player, this.hudPanel);
                break;
            }
            
        }
        public void SetGameOver() => State = new GameOverState(this.gameoverPanel);
        public void SetAbout() => State = new AboutState(this.aboutPanel);
        public void SetMenu() => State = new MenuState(this.fakePlayer, this.menuPanel);

        GameManager()
        {
            Instance = this;
        }

        void Update() => state.Update();
        void FixedUpdate() => state.FixedUpdate();

        public void Start()
        {
            Player.gameObject.SetActive(false);
            Player.Reset();
            Pool.Reset();
            GameStart = Time.time;
            HideAllUI();
            SetMenu();
        }


        public void IncreaseScore(int amount)
        {
            score += amount;
        }

        public int CalcFrameScore()
        {
            return pool.GetAsteroidCount(AsteroidSize.Large) + (pool.GetAsteroidCount(AsteroidSize.Medium) * 2) + (pool.GetAsteroidCount(AsteroidSize.Small) * 4) + (pool.GetAsteroidCount(AsteroidSize.Tiny) * 8);
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            SetGameOver();
        }

        private void HideAllUI()
        {
            this.menuPanel.Hide();
            this.gameoverPanel.Hide();
            this.aboutPanel.Hide();
            this.hudPanel.Hide();
        }

        public enum GameplayType 
        {
            Normal,
            Crazy,
            Chaos
        }
    }
}