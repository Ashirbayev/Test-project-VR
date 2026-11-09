
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabStepTracker : MonoBehaviour
{
    [Header("Scenario")]
    [Range(1, 9)]
    public int stepNumber = 4;

    private XRGrabInteractable grabInteractable;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if (grabInteractable == null)
            return;

        if (!grabInteractable.isSelected)
            return;

        if (ScenarioManager.Instance != null)
        {
            ScenarioManager.Instance.CompleteStep(stepNumber);
        }
    }
}
