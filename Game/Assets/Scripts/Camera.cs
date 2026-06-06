using UnityEngine;
using Unity.Cinemachine;

public class DownwardCamera : CinemachineExtension
{
    private float minY;
    private bool isInitialized = false;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam, 
        CinemachineCore.Stage stage, 
        ref CameraState state, 
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 pos = state.RawPosition;

            if (!isInitialized)
            {
                minY = pos.y;
                isInitialized = true;
            }

            if (pos.y < minY)
            {
                minY = pos.y;
            }
            // If it tries to move UP (higher Y), lock it to the lowest point we've reached
            else
            {
                pos.y = minY;
            }

            state.RawPosition = pos;
        }
    }

    public void ResetCamera()
    {
        isInitialized = false;
    }
}