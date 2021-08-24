using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace HackedDesign
{

    public class Exploder : MonoBehaviour
    {
        [SerializeField] private UnityEvent collideEvent = null;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!gameObject.CompareTag("Asteroid") || (gameObject.CompareTag("Asteroid") && (!collision.gameObject.CompareTag("Asteroid") && !collision.gameObject.CompareTag("Alien"))))
            {
                collideEvent.Invoke();
            }
        }
    }
}