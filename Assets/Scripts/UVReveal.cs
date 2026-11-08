
using UnityEngine;
using TMPro;

public class UVReveal : MonoBehaviour
{
    [Header("Flashlight")]
    public Light flashlight;

    [Header("Secret Hint")]
    public TextMeshPro secretText;

    [Header("Settings")]
    public LayerMask obstacleMask = ~0;

    private void Start()
    {
        if (secretText != null)
            secretText.enabled = false;
    }

    private void Update()
    {
        if (flashlight == null || secretText == null)
            return;

        bool visible = IsIlluminated();
        secretText.enabled = visible;
    }

    private bool IsIlluminated()
    {
        if (!flashlight.enabled ||
            !flashlight.gameObject.activeInHierarchy ||
            flashlight.type != LightType.Spot)
            return false;

        Vector3 origin = flashlight.transform.position;
        Vector3 target = secretText.transform.position;

        Vector3 direction = target - origin;
        float distance = direction.magnitude;

        if (distance < 0.001f ||
            distance > flashlight.range)
            return false;

        
        float angle = Vector3.Angle(
            flashlight.transform.forward,
            direction
        );

        if (angle > flashlight.spotAngle * 0.5f)
            return false;

       
        if (Physics.Raycast(
            origin,
            direction.normalized,
            out RaycastHit hit,
            distance + 0.05f,
            obstacleMask,
            QueryTriggerInteraction.Ignore))
        {
            if (hit.distance < distance - 0.02f)
                return false;
        }

        return true;
    }
}
