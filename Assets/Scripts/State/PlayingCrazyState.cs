
using UnityEngine;

namespace HackedDesign
{
    public class PlayingCrazyState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter hudPresenter;

        private int spawnTimer = 0;
        private int spawnCount = 0;

        public PlayingCrazyState(PlayerController player, UI.AbstractPresenter hudPresenter)
        {
            this.player = player;
            this.hudPresenter = hudPresenter;
            Debug.Log("New crazy state");
        }

        public bool Playing => true;

        public void Begin()
        {
            Debug.Log("Begin");
            this.player.gameObject.SetActive(true);
            Time.timeScale = 1;
            Cursor.visible = false;
            this.hudPresenter.Show();
        }

        public void End()
        {
            this.player.Stop();
            this.player.gameObject.SetActive(false);
            Time.timeScale = 0;
            Cursor.visible = true;
            this.hudPresenter.Hide();
        }

        public void FixedUpdate()
        {

        }

        public void Start()
        {

        }

        public void Update()
        {
            if (Mathf.FloorToInt(Time.time - GameManager.Instance.GameStart) > GameManager.Instance.GameTime)
            {
                spawnTimer--;
                GameManager.Instance.GameTime = Mathf.FloorToInt(Time.time);
                GameManager.Instance.IncreaseScore(GameManager.Instance.CalcFrameScore());
            }

            if (spawnTimer <= 0)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    var circle = (Random.insideUnitCircle.normalized * 3.0f);
                    var position = GameManager.Instance.Player.transform.position + new Vector3(circle.x, circle.y);
                    GameManager.Instance.Pool.SpawnAsteroid(AsteroidSize.Large, position);
                }
                spawnTimer = 15;
            }

            // If we ever run out, spawn some more in
            if (GameManager.Instance.Pool.GetAsteroidCount(AsteroidSize.Large)
            + GameManager.Instance.Pool.GetAsteroidCount(AsteroidSize.Medium)
            + GameManager.Instance.Pool.GetAsteroidCount(AsteroidSize.Small)
            + GameManager.Instance.Pool.GetAsteroidCount(AsteroidSize.Tiny) <= 0)
            {
                spawnCount++;
                for (int i = 0; i < spawnCount; i++)
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