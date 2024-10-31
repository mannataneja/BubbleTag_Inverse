using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPosition : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _distOfUI;
    [SerializeField] private float _farClipPlane;
    [SerializeField] private float _nearClipPlane;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _camera.transform.position + _camera.transform.forward * _distOfUI;
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera != null)
        {
            float canvasDistance = _nearClipPlane + _distOfUI;
            transform.position = _camera.transform.position + _camera.transform.forward * canvasDistance;
            transform.LookAt(_camera.transform.position + _camera.transform.forward);

            if (Vector3.Distance(_camera.transform.position, transform.position) > _farClipPlane)
            {
                transform.position = _camera.transform.position + _camera.transform.forward * _farClipPlane;
            }
        }
    }
}
