using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    float orthographicSize;
    float targetorthographicSize;

    private void Start()
    {
        orthographicSize = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        targetorthographicSize = orthographicSize;
    }

    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    void HandleZoom()
    {
        float zoomAmount = 2f;
        targetorthographicSize += Input.mouseScrollDelta.y * zoomAmount;

        float minOrtographicSize = 10f;
        float maxOrtographicSize = 30f;
        targetorthographicSize = Mathf.Clamp(targetorthographicSize, minOrtographicSize, maxOrtographicSize);

        float zoomSpeed = 5f;
        orthographicSize = Mathf.Lerp(orthographicSize, targetorthographicSize, Time.deltaTime * zoomSpeed);

        cinemachineVirtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(x, y).normalized;
        float moveSpeed = 30f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
