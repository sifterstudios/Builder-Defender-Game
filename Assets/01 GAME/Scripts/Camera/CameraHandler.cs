using System;
using Cinemachine;
using UnityEngine;

namespace BD.Camera
{
    public class CameraHandler : MonoBehaviour
    {
        public static CameraHandler Instance { get; private set; }
        [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

        float _orthographicSize;
        float _targetorthographicSize;
        bool _edgeScrolling;

        void Awake() => Instance = this;

        private void Start()
        {
            _orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
            _targetorthographicSize = _orthographicSize;
        }

        void Update()
        {
            HandleMovement();
            HandleZoom();
        }

        void HandleZoom()
        {
            float zoomAmount = 2f;
            _targetorthographicSize += Input.mouseScrollDelta.y * zoomAmount;

            float minOrtographicSize = 10f;
            float maxOrtographicSize = 30f;
            _targetorthographicSize = Mathf.Clamp(_targetorthographicSize, minOrtographicSize, maxOrtographicSize);

            float zoomSpeed = 5f;
            _orthographicSize = Mathf.Lerp(_orthographicSize, _targetorthographicSize, Time.deltaTime * zoomSpeed);

            cinemachineVirtualCamera.m_Lens.OrthographicSize = _orthographicSize;
        }

        void HandleMovement()
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            if (_edgeScrolling)
            {
                float edgeScrollingSize = 35;
                if (Input.mousePosition.x > Screen.width - edgeScrollingSize) x = 1f;

                if (Input.mousePosition.x < edgeScrollingSize) x = -1f;

                if (Input.mousePosition.y > Screen.height - edgeScrollingSize) y = 1f;

                if (Input.mousePosition.y < edgeScrollingSize) y = -1f;
                
            }
            Vector3 moveDir = new Vector3(x, y).normalized;
            float moveSpeed = 30f;

            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }
        public void SetEdgeScrolling (bool edgeScrolling) => _edgeScrolling = edgeScrolling;

        public bool GetEdgeScrolling() => _edgeScrolling;
    }


}