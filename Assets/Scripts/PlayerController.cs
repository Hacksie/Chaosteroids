using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float rotationSpeed = 360;
        [SerializeField] private float maxVelocity = 1;
        [SerializeField] private float fireRate = 0.5f;

        [Header("Referenced GameObjects")]
        [SerializeField] private Pool pool = null;
        [SerializeField] private Transform firePosition = null;

        private new Rigidbody2D rigidbody;
        private Vector2 moveVector;

        private float fireTimer = 0;

        void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame


        public void FireEvent(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Fire();
            }
        }

        private void Fire()
        {
            if ((Time.time - fireTimer) > fireRate)
            {
                fireTimer = Time.time;
                var bullet = GameManager.Instance.Pool.InstantiateBullet();
                bullet.Fire(firePosition.position, transform.up);

            }
        }

        public void MoveEvent(InputAction.CallbackContext context)
        {
            moveVector = context.ReadValue<Vector2>();
        }

        public void Reset()
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            Stop();
        }

        public void UpdateBehaviour()
        {
            UpdateThrust();
            UpdateRotation();

        }        

        public void Stop()
        {
            rigidbody.velocity = Vector3.zero;
        }

        public void UpdateThrust()
        {
            rigidbody.AddForce(transform.up * Mathf.Clamp01(moveVector.y));

            if (rigidbody.velocity.sqrMagnitude <= (maxVelocity * maxVelocity))
            {
                rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
            }
        }

        public void UpdateRotation()
        {
            var rotationAngle = transform.rotation.eulerAngles.z - (moveVector.x * rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
        }

    public void Explode()
    {
        Debug.Log("Player exploded", this);
        GameManager.Instance.GameOver();
    }        
    }

}