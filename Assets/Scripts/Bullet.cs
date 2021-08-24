using UnityEngine;

namespace HackedDesign
{


    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [Header("Referenced GameObjects")]
        private Camera mainCamera;
        private new Rigidbody2D rigidbody;
        [SerializeField] private TrailRenderer trail;

        [Header("Settings")]
        [SerializeField] private float bulletSpeed = 50.0f;


        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }

            rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (CheckOutOfBounds())
            {
                gameObject.SetActive(false);

            }
        }

        public void Explode()
        {
            rigidbody.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }

        public void Fire(Vector2 position, Vector3 up)
        {
            transform.position = position;
            transform.up = up;
            rigidbody.AddForce(up.normalized * bulletSpeed);
            trail.Clear();
            
        }

        private bool CheckOutOfBounds()
        {
            Vector3 viewPosition = mainCamera.WorldToViewportPoint(transform.position);
            var viewPort = mainCamera.rect;

            return (viewPosition.x < viewPort.x) || (viewPosition.x > (viewPort.x + viewPort.width)) || (viewPosition.y < viewPort.y) || (viewPosition.y > (viewPort.y + viewPort.height));
        }
    }
}