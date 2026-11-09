
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class StepZone : MonoBehaviour
{
    [Range(1, 9)]
    public int stepNumber = 1;

    private void Start()
    {
        Debug.Log("TableZone script started!");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTER: " + other.name);

        CharacterController player =
            other.GetComponentInParent<CharacterController>();

        if (player == null)
        {
            Debug.LogWarning(
                "Collider entered, but CharacterController not found!"
            );
            return;
        }

        Debug.Log("PLAYER DETECTED!");

        if (ScenarioManager.Instance != null)
        {
            ScenarioManager.Instance.CompleteStep(stepNumber);
        }
        else
        {
            Debug.LogError("ScenarioManager not found!");
        }
    }
}
