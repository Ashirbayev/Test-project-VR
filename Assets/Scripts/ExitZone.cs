
using UnityEngine;
using TMPro;

public class ExitZone : MonoBehaviour
{
    [Header("Exit Settings")]
    public bool isCorrectExit = false;

    [Header("UI")]
    public TextMeshProUGUI resultText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip failSound;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponentInParent<CharacterController>() == null)
            return;

        if (hasTriggered)
            return;

        hasTriggered = true;

        if (isCorrectExit)
        {
            Debug.Log("Correct Exit!");

            if (resultText != null)
                resultText.text = "Correct Exit!";

            if (audioSource != null && successSound != null)
                audioSource.PlayOneShot(successSound);

            if (ScenarioManager.Instance != null)
            {
                ScenarioManager.Instance.CompleteStep(6);
            }
        }
        else
        {
            Debug.Log("Wrong Exit!");

            if (resultText != null)
                resultText.text = "Wrong Exit!";

            if (audioSource != null && failSound != null)
                audioSource.PlayOneShot(failSound);
        }
    }

    public void ResetZone()
    {
        hasTriggered = false;
    }
}
