using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _camera1 = default;
    [SerializeField] CinemachineVirtualCamera _camera2 = default;
    enum CurrentCamType
    {
        None,
        Cam1,
        Cam2
    }

    CurrentCamType _currentCamType = CurrentCamType.None;
    // Start is called before the first frame update
    void Start()
    {
        ChengeCameraType(CurrentCamType.Cam1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_camera1) return;
        if (!_camera2) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log($"{other.name}と接触");
            switch (_currentCamType)
            {
                case CurrentCamType.None:
                    break;
                case CurrentCamType.Cam1:
                    ChengeCameraType(CurrentCamType.Cam2);
                    break;
                case CurrentCamType.Cam2:
                    ChengeCameraType(CurrentCamType.Cam1);
                    break;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    void ChengeCameraType(CurrentCamType _nextCamType)
    {
        switch (_currentCamType)
        {
            case CurrentCamType.None:
                _camera1.Priority = 11;
                _camera2.Priority = 0;
                break;
            case CurrentCamType.Cam1:
                _camera2.Priority = 11;
                _camera1.Priority = 0;
                break;
            case CurrentCamType.Cam2:
                _camera1.Priority = 11;
                _camera2.Priority = 0;
                break;
        }
        _currentCamType = _nextCamType;
    }
}
