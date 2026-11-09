
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class PassportStepTracker : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private bool wasGrabbed = false;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if (grabInteractable == null || wasGrabbed)
            return;

        
        if (grabInteractable.isSelected)
        {
            wasGrabbed = true;

            Debug.Log("Passport grabbed detected!");

            if (ScenarioManager.Instance != null)
            {
                ScenarioManager.Instance.CompleteStep(2);
            }
            else
            {
                Debug.LogError("ScenarioManager not found!");
            }
        }
    }
}
