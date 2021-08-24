
using UnityEngine;

namespace HackedDesign
{
    public class PlayingEliminationState : IState
    {
        private PlayerController player;

        private int spawnTimer = 0;

        public PlayingEliminationState(PlayerController player)
        {
            this.player = player;
        }

        public bool Playing => true;

        public void Begin()
        {
            Time.timeScale = 1;
        }

        public void End()
        {
            this.player.Stop();
            Time.timeScale = 0;
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