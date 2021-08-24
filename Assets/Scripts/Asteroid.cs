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
        [SerializeField] private float autoPopTime = 30;

        private new Rigidbody2D rigidbody = null;

        private float spawnTime = 0;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (GameManager.Instance.GameType == GameManager.GameplayType.Chaos && spawnTime <= (Time.time - autoPopTime))
            {
                Pop();
            }
        }

        public void Boost(Vector3 boost)
        {
            rigidbody.AddForce(boost);
            spawnTime = Time.time;
        }

        public void Explode()
        {
            GameManager.Instance.IncreaseScore((int)size);

            Pop();
        }

        public void Pop()
        {
            if (GameManager.Instance.GameType == GameManager.GameplayType.Chaos)
            {
                gameObject.SetActive(false);
                if (size != AsteroidSize.Tiny)
                {
                    SpawnSmallerAsteroids();
                }
            }
            else
            {
                gameObject.SetActive(false);
                if (size != AsteroidSize.Small)
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