
using UnityEngine;

public class wheel : MonoBehaviour
{
    public WheelCollider wheelCollider;
    public Transform wheelMesh;
    public bool wheelTurn;

    private void LateUpdate()
    {
        if (wheelCollider == null || wheelMesh == null)
            return;

        wheelCollider.GetWorldPose(
            out Vector3 position,
            out Quaternion rotation
        );

        wheelMesh.position = position;
        wheelMesh.rotation = rotation;
    }
}
