using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class AnimateMenuPlayer : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 10;
        [SerializeField] private Transform ship;

        void Update()
        {

            switch (GameManager.Instance.GameType)
            {
                case GameManager.GameplayType.Chaos:
                    this.transform.Rotate(new Vector3(0, 0, 1.5f * -rotateSpeed * Time.unscaledDeltaTime));
                    ship.transform.Rotate(new Vector3(0, 0, 6 * rotateSpeed * Time.unscaledDeltaTime));
                    break;
                case GameManager.GameplayType.Crazy:
                    this.transform.Rotate(new Vector3(0, 0, 1.5f * -rotateSpeed * Time.unscaledDeltaTime));
                    ship.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    break;
                case GameManager.GameplayType.Normal:
                default:
                    this.transform.Rotate(new Vector3(0, 0, -rotateSpeed * Time.unscaledDeltaTime));
                    ship.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    break;
            }
        }
    }
}