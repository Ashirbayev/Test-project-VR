
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FlashlightBoardCheck : MonoBehaviour
{
    [Header("Flashlight")]
    public Light flashlight;
    public XRGrabInteractable flashlightGrab;

    [Header("Board")]
    public Collider boardCollider;

    [Header("Settings")]
    public float maxDistance = 10f;
    public float requiredTime = 1f;

    private float illuminationTime;
    private bool completed;

    private void Update()
    {
        if (completed)
            return;

        if (flashlight == null ||
            flashlightGrab == null ||
            boardCollider == null)
            return;

        bool isHolding = flashlightGrab.isSelected;

        bool lightEnabled =
            flashlight.enabled &&
            flashlight.gameObject.activeInHierarchy &&
            flashlight.type == LightType.Spot;

        bool isLookingAtBoard = false;

        if (isHolding && lightEnabled)
        {
            Ray ray = new Ray(
                flashlight.transform.position,
                flashlight.transform.forward
            );

            if (Physics.Raycast(
                ray,
                out RaycastHit hit,
                Mathf.Min(maxDistance, flashlight.range),
                ~0,
                QueryTriggerInteraction.Ignore))
            {
                isLookingAtBoard =
                    hit.collider == boardCollider;
            }
        }

        if (isLookingAtBoard)
        {
            illuminationTime += Time.deltaTime;

            if (illuminationTime >= requiredTime)
            {
                completed = true;

                if (ScenarioManager.Instance != null)
                {
                    ScenarioManager.Instance.CompleteStep(5);
                }

                Debug.Log("Board illuminated! Step 5 DONE");
            }
        }
        else
        {
            illuminationTime = 0f;
        }
    }
}
