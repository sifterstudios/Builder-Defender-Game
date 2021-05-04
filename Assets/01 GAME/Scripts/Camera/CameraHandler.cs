using Cinemachine;
using UnityEngine;

namespace Camera
{
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
        bool _edgeScrolling;

        float _orthographicSize;
        float _targetorthographicSize;
        public static CameraHandler Instance { get; private set; }

        void Awake()
        {
            Instance = this;
            _edgeScrolling = PlayerPrefs.GetInt("edgeScrolling", 1) == 1;
        }

        void Start()
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
            var zoomAmount = 2f;
            _targetorthographicSize += Input.mouseScrollDelta.y * zoomAmount;

            var minOrtographicSize = 10f;
            var maxOrtographicSize = 30f;
            _targetorthographicSize = Mathf.Clamp(_targetorthographicSize, minOrtographicSize, maxOrtographicSize);

            var zoomSpeed = 5f;
            _orthographicSize = Mathf.Lerp(_orthographicSize, _targetorthographicSize, Time.deltaTime * zoomSpeed);

            cinemachineVirtualCamera.m_Lens.OrthographicSize = _orthographicSize;
        }

        void HandleMovement()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");

            if (_edgeScrolling)
            {
                float edgeScrollingSize = 35;
                if (Input.mousePosition.x > Screen.width - edgeScrollingSize) x = 1f;

                if (Input.mousePosition.x < edgeScrollingSize) x = -1f;

                if (Input.mousePosition.y > Screen.height - edgeScrollingSize) y = 1f;

                if (Input.mousePosition.y < edgeScrollingSize) y = -1f;
            }

            var moveDir = new Vector3(x, y).normalized;
            var moveSpeed = 30f;

            transform.position += moveDir * (moveSpeed * Time.deltaTime);
        }

        public void SetEdgeScrolling(bool edgeScrolling)
        {
            _edgeScrolling = edgeScrolling;
            PlayerPrefs.SetInt("edgeScrolling", edgeScrolling ? 1 : 0);
        }

        public bool GetEdgeScrolling()
        {
            return _edgeScrolling;
        }
    }
}