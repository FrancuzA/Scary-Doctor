using Unity.Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class LockCameraY : CinemachineExtension
{
    public float fixedYPosition = 0f; // Set this in Inspector

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            Vector3 rawPos = state.RawPosition;
            Vector3 correctedPos = new Vector3(rawPos.x, fixedYPosition, rawPos.z);
            Vector3 finalPos = state.GetFinalPosition();
            state.PositionCorrection = correctedPos - finalPos;
        }
    }
}