using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ClampToViewport : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private bool disableOnExit = false;

        private void Awake()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        void Update()
        {
            ClampToView();
        }

        private void ClampToView()
        {
            // FIXME: Reset trail
            Vector3 viewPosition = mainCamera.WorldToViewportPoint(transform.position);
            var viewPort = mainCamera.rect;

            bool exit = false;

            if (viewPosition.x < viewPort.x)
            {
                viewPosition.x = viewPort.x + viewPort.width;
                exit = true;
            }
            if (viewPosition.x > (viewPort.x + viewPort.width))
            {
                viewPosition.x = viewPort.x;
                exit = true;
            }
            if (viewPosition.y < viewPort.y)
            {
                viewPosition.y = viewPort.y + viewPort.height;
                exit = true;
            }
            if (viewPosition.y > (viewPort.y + viewPort.height))
            {
                viewPosition.y = viewPort.y;
                exit = true;
            }

            var newPosition = mainCamera.ViewportToWorldPoint(viewPosition);

            if (newPosition != transform.position)
            {
                transform.position = newPosition;
            }

            if(exit && disableOnExit)
            {
                gameObject.SetActive(false);
            }            
        }
    }
}