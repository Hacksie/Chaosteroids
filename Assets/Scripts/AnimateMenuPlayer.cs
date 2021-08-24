using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class AnimateMenuPlayer : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 10;
        [SerializeField] private Transform ship;

        // Update is called once per frame
        void Update()
        {
            this.transform.Rotate(new Vector3(0, 0, -rotateSpeed * Time.deltaTime));
            if (GameManager.Instance.GameType == GameManager.GameplayType.Chaos)
            {
                ship.transform.Rotate(new Vector3(0, 0, 6 * rotateSpeed * Time.deltaTime));
            }
            else
            {
                ship.transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
        }
    }
}