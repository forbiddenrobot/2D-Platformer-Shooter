using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MultipleTargetCamera : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float smoothTime = 0.5f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float minZoom = 10;
    [SerializeField] private float maxZoom = Mathf.Infinity;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private Vector2 zoomOffset;

    private Vector3 velocity;

    private void LateUpdate()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
        if (targets.Length < 1) return;

        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void Zoom()
    {
        Vector2 boundsSize = GetGreatestDistance();
        float sizeX = boundsSize.x / zoomOffset.x;
        float sizeY = boundsSize.y / zoomOffset.y;

        if (sizeX > sizeY)
        {
            if (sizeX < minZoom)
            {
                sizeX = minZoom;
            }
            else if (sizeX > maxZoom)
            {
                sizeX = maxZoom;
            }
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, sizeX, Time.deltaTime);
        }
        else
        {
            if (sizeY < minZoom)
            {
                sizeY = minZoom;
            }
            else if (sizeY > maxZoom)
            {
                sizeY = maxZoom;
            }
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(vcam.m_Lens.OrthographicSize, sizeY, Time.deltaTime);
        }
        
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Length == 0)
        {
            return targets[0].transform.position;
        }

        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return bounds.center;
    }

    private Vector2 GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0;i < targets.Length; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }

        return new Vector2(bounds.size.x, bounds.size.y);
    }
}
