using SphereFitting;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeviceVisualizer : MonoBehaviour
{
    private const float waitTime = 1.0f;
    private Camera _camera;
    private EmDevice _emDevice;
    private FitterManager _fitterManager;
    public float CurrentTime;

    public float Speed;
    public GameObject Sphere;

    public TextMeshPro TextMesh;
    private void FixedUpdate()
    {
        try
        {
            _fitterManager.GetEstimates(out Vector3 destination, out float estimatedRadius, out float error);

            TextMesh.transform.LookAt(_camera.transform);

            if (!destination.IsValid())
                return;

            transform.position = destination;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _emDevice = GetComponent<EmDevice>();
        _fitterManager = GetComponent<FitterManager>();
        TextMesh.text = _emDevice.SSID;

        Helpers.AttachARAnchor(Sphere);
    }
}