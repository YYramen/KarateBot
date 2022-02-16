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

    void Start()
    {
        ChangeCameraType(CurrentCamType.Cam1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_camera1) return;
        if (!_camera2) return;
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{other.name}�ƐڐG");
            switch (_currentCamType)
            {
                case CurrentCamType.None:
                    break;
                case CurrentCamType.Cam1:
                    ChangeCameraType(CurrentCamType.Cam2);
                    break;
                case CurrentCamType.Cam2:
                    ChangeCameraType(CurrentCamType.Cam1);
                    break;
            }
        }
    }

    void ChangeCameraType(CurrentCamType _nextCamType)
    {
        switch (_currentCamType)
        {
            case CurrentCamType.None:
                _camera1.Priority = 11;
                _camera2.Priority = 0;
                _camera1.MoveToTopOfPrioritySubqueue();
                break;
            case CurrentCamType.Cam1:
                _camera2.Priority = 11;
                _camera1.Priority = 0;
                _camera2.MoveToTopOfPrioritySubqueue();
                break;
            case CurrentCamType.Cam2:
                _camera1.Priority = 11;
                _camera2.Priority = 0;
                _camera1.MoveToTopOfPrioritySubqueue();
                break;
        }
        _currentCamType = _nextCamType;
    }
}