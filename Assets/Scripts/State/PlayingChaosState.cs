
using UnityEngine;

namespace HackedDesign
{
    public class PlayingChaosState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter hudPresenter;

        public const int Countdown = 10;
        public bool first = true;

        public float nextAlienSpawn = 0;



        public PlayingChaosState(PlayerController player, UI.AbstractPresenter hudPresenter)
        {
            this.player = player;
            this.hudPresenter = hudPresenter;
            Debug.Log("New chaos state");
        }

        public bool Playing => true;

        public void Begin()
        {
            Debug.Log("Begin");
            this.player.gameObject.SetActive(true);
            Time.timeScale = 1;
            Cursor.visible = false;
            this.hudPresenter.Show();
            this.nextAlienSpawn = Time.time + Random.Range(10, 30);
        }

        public void End()
        {
            this.player.Stop();
            this.player.gameObject.SetActive(false);
            Time.timeScale = 0;
            Cursor.visible = true;
            this.hudPresenter.Hide();
            GameManager.Instance.Music.pitch = 1.0f;
        }

        public void FixedUpdate()
        {

        }

        public void Start()
        {

        }

        public void Update()
        {
            if (nextAlienSpawn < Time.time)
            {
                var circle = (Random.insideUnitCircle.normalized * 3.0f);
                var position = GameManager.Instance.Player.transform.position + new Vector3(circle.x, circle.y);
                nextAlienSpawn = Time.time + Random.Range(30, 90);
                GameManager.Instance.Pool.LaunchAlien(position);
            }

            if (Mathf.FloorToInt(Time.time - GameManager.Instance.GameStart) > GameManager.Instance.GameTime)
            {
                GameManager.Instance.SpawnCountdown--;
                GameManager.Instance.GameTime = Mathf.FloorToInt(Time.time);
                GameManager.Instance.IncreaseScore(GameManager.Instance.CalcFrameScore());
            }

            if (GameManager.Instance.SpawnCountdown <= 0)
            {
                for (int i = 0; i < GameManager.Instance.Level; i++)
                {
                    var circle = (Random.insideUnitCircle.normalized * 3.0f);
                    var position = GameManager.Instance.Player.transform.position + new Vector3(circle.x, circle.y);
                    GameManager.Instance.Pool.SpawnAsteroid(AsteroidSize.Large, position);
                }
                GameManager.Instance.SpawnCountdown = Countdown;
                // Hack to stop the pitch increases immediately
                if (!first)
                {
                    GameManager.Instance.Music.pitch += 0.05f;
                }
                first = false;
            }

            // If we ever run out, spawn some more in
            if (GameManager.Instance.Pool.GetAsteroidCount(AsteroidSize.Large)
            + GameManager.Instance.Pool.GetAsteroidCount(AsteroidSize.Medium)
            + GameManager.Instance.Pool.GetAsteroidCount(AsteroidSize.Small)
            + GameManager.Instance.Pool.GetAsteroidCount(AsteroidSize.Tiny) <= 0)
            {
                GameManager.Instance.Level++;
                for (int i = 0; i < GameManager.Instance.Level; i++)
                {
                    var circle = (Random.insideUnitCircle.normalized * 3.0f);
                    var position = GameManager.Instance.Player.transform.position + new Vector3(circle.x, circle.y);
                    GameManager.Instance.Pool.SpawnAsteroid(AsteroidSize.Large, position);
                }
            }


            this.player.UpdateBehaviour();
        }
    }

}