using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Alien : MonoBehaviour
    {

        [SerializeField] private float timeout = 7.0f;
        [SerializeField] private int score = 100;
        private new Rigidbody2D rigidbody = null;
        [SerializeField] private Camera mainCamera;

        private float spawnTime = 0;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Launch(Vector3 position, Vector3 boost)
        {
            gameObject.SetActive(true);
            transform.position = position;
            rigidbody.AddForce(boost);
            spawnTime = Time.time;
        }

        void Update()
        {
            if(gameObject.activeInHierarchy)
            {
                if(spawnTime <= (Time.time - timeout))
                {
                    Missed();
                }
            }
        }

        public void Explode()
        {
            GameManager.Instance.IncreaseScore(score);
            gameObject.SetActive(false);
        }

        public void Missed()
        {
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Bullet") )
            {
                Explode();
            }
        }        
    }
}