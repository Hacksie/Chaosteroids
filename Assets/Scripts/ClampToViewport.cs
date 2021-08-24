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

            if (viewPosition.x < viewPort.x)
            {
                viewPosition.x = viewPort.x + viewPort.width;
            }
            if (viewPosition.x > (viewPort.x + viewPort.width))
            {
                viewPosition.x = viewPort.x;
            }
            if (viewPosition.y < viewPort.y)
            {
                viewPosition.y = viewPort.y + viewPort.height;
            }
            if (viewPosition.y > (viewPort.y + viewPort.height))
            {
                viewPosition.y = viewPort.y;
            }

            var newPosition = mainCamera.ViewportToWorldPoint(viewPosition);

            if (newPosition != transform.position)
            {
                transform.position = newPosition;
            }
        }
    }
}