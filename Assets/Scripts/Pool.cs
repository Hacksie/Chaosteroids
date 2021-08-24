using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HackedDesign
{
    public class Pool : MonoBehaviour
    {
        [Header("Asteroids")]
        [SerializeField] Transform enemiesPoolParent = null;
        [SerializeField] List<GameObject> asteroidPrefabs = null;

        [Header("Bullets")]
        [SerializeField] Transform bulletPoolParent = null;
        [SerializeField] Bullet bulletPrefab = null;

        [Header("Alien")]
        [SerializeField] Alien alien = null;
        // Start is called before the first frame update

        private Dictionary<AsteroidSize, List<Asteroid>> asteroids = new Dictionary<AsteroidSize, List<Asteroid>>();
        private List<Bullet> bullets = new List<Bullet>();

        private void Awake()
        {
            asteroids[AsteroidSize.Large] = new List<Asteroid>();
            asteroids[AsteroidSize.Medium] = new List<Asteroid>();
            asteroids[AsteroidSize.Small] = new List<Asteroid>();
            asteroids[AsteroidSize.Tiny] = new List<Asteroid>();
        }

        public int GetAsteroidCount(AsteroidSize size)
        {
            return asteroids[size].Where(s => s.gameObject.activeInHierarchy).Count();
        }

        public void Reset()
        {
            asteroids.Select(e => { e.Value.ForEach(a => a.gameObject.SetActive(false)); return true; }).ToList();
            bullets.ForEach(b => b.gameObject.SetActive(false));
        }

        private Asteroid InstantiateAsteroid(AsteroidSize size)
        {
            Debug.Log(size);
            var asteroid = asteroids[size].FirstOrDefault(a => !a.gameObject.activeInHierarchy);

            if (asteroid == null)
            {
                var gameObject = Instantiate(asteroidPrefabs[(int)size], enemiesPoolParent);

                if (gameObject == null)
                {
                    Debug.LogError("Unable to instantiate Asteroid", this);
                    return null;
                }

                asteroid = gameObject.GetComponent<Asteroid>();

                asteroids[size].Add(asteroid);
            }
            else
            {
                asteroid.gameObject.SetActive(true);
            }

            return asteroid;
        }

        public void SpawnAsteroid(AsteroidSize size, Vector3 position)
        {
            var asteroid = InstantiateAsteroid(size);
            asteroid.transform.position = position;
            asteroid.Boost(Random.insideUnitCircle.normalized * 100);
        }

        public Bullet InstantiateBullet()
        {
            var bullet = bullets.FirstOrDefault(b => !b.gameObject.activeInHierarchy);

            if (bullet == null)
            {
                var gameObject = Instantiate(bulletPrefab, bulletPoolParent);

                if (gameObject == null)
                {
                    Debug.LogError("Unable to instantiate Bullet", this);
                    return null;
                }

                bullet = gameObject.GetComponent<Bullet>();
                bullets.Add(bullet);
            }
            else
            {
                bullet.gameObject.SetActive(true);
            }

            return bullet;
        }

        public void LaunchAlien(Vector3 position)
        {
            alien.Launch(position, Random.insideUnitCircle.normalized * 50);
        }
    }
}
