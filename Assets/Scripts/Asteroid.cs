using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private AsteroidSize size = AsteroidSize.Large;
        [SerializeField] private int numberOfSpawns = 2;
        [SerializeField] private float radius = 1;

        private new Rigidbody2D rigidbody = null;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Boost(Vector3 boost)
        {
            rigidbody.AddForce(boost);
        }

        public void Explode()
        {
            GameManager.Instance.IncreaseScore(1);

            if(GameManager.Instance.GameType == GameManager.GameplayType.Chaos)
            {
                if (size != AsteroidSize.Tiny)
                {
                    gameObject.SetActive(false);
                    SpawnSmallerAsteroids();
                }
            }
            else
            {
                gameObject.SetActive(false);
                if (size != AsteroidSize.Small || size != AsteroidSize.Tiny)
                {
                    SpawnSmallerAsteroids();
                }
            }
        }

        private void SpawnSmallerAsteroids()
        {
            for (int i = 0; i < numberOfSpawns; i++)
            {
                GameManager.Instance.Pool.SpawnAsteroid(size + 1, transform.position + (Random.insideUnitSphere.normalized * radius));
            }
        }

    }

    public enum AsteroidSize
    {
        Large,
        Medium,
        Small,
        Tiny
    }

}