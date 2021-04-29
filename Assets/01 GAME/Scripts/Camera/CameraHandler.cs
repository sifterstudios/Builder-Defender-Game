using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    float _orthographicSize;
    float _targetorthographicSize;

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

        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;

        transform.position += moveDir * (moveSpeed * Time.deltaTime);
    }
}