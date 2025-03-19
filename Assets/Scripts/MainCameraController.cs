using UnityEngine;
using Unity.Cinemachine;

public class MainCameraController : MonoBehaviour
{
    public CinemachineCamera virtualCamera;    
    public float rotationY;

    void Update()
    {
        if (virtualCamera != null)
        {
            var state = virtualCamera.State;
            var rotation = state.RawOrientation; // Use RawOrientation instead of FinalOrientation

            var euler = rotation.eulerAngles;
            rotationY = euler.y; // Store Y rotation separately

            var roundedRotationY = Mathf.RoundToInt(rotationY);
        }
    }

    public Quaternion flatRotation => Quaternion.Euler(0, rotationY, 0); // Fixed typo in property name
}
