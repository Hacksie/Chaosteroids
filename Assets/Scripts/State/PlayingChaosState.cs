
using UnityEngine;

namespace HackedDesign
{
    public class PlayingChaosState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter hudPresenter;

        private int spawnTimer = 0;

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
        }

        public void End()
        {
            this.player.Stop();
            this.player.gameObject.SetActive(false);
            Time.timeScale = 0;
            Cursor.visible = true;
        }

        public void FixedUpdate()
        {

        }

        public void Start()
        {

        }

        public void Update()
        {
            switch (GameManager.Instance.GameType)
            {
                case GameManager.GameplayType.Chaos:
                    if (Mathf.FloorToInt(Time.time - GameManager.Instance.GameStart) > GameManager.Instance.GameTime)
                    {
                        spawnTimer--;
                        GameManager.Instance.GameTime = Mathf.FloorToInt(Time.time);
                        GameManager.Instance.IncreaseScore(GameManager.Instance.CalcFrameScore());
                    }

                    if (spawnTimer <= 0)
                    {
                        var circle = (Random.insideUnitCircle.normalized * 3.0f);
                        var position = GameManager.Instance.Player.transform.position + new Vector3(circle.x, circle.y);
                        GameManager.Instance.Pool.SpawnAsteroid(AsteroidSize.Large, position);
                        spawnTimer = 10;
                    }
                    break;
                case GameManager.GameplayType.Elimination:
                    break;
                case GameManager.GameplayType.Normal:
                    break;
            }


            this.player.UpdateBehaviour();
        }
    }

}