
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PassportCheck : MonoBehaviour
{
    [Header("Passport")]
    [SerializeField] private XRGrabInteractable passport;

    [Header("UI")]
    [SerializeField] private TMP_Text resultText;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip errorSound;

    private float nextCheckTime;

    private void Start()
    {
        resultText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
       
        CharacterController player =
            other.GetComponentInParent<CharacterController>();

        if (player == null)
            return;

      
        if (Time.time < nextCheckTime)
            return;

        nextCheckTime = Time.time + 1.5f;


        bool hasPassport = passport != null && passport.isSelected;

        Debug.Log("Passport: " + (passport == null ? "None" : passport.name));
        Debug.Log("Passport grab: " + hasPassport);

        if (passport != null)
        {
            Debug.Log("How much Interactor: " +
                      passport.interactorsSelecting.Count);
        }

        if (hasPassport)
        {
            resultText.text = "Succes!";
            resultText.color = Color.green;

            if (successSound != null)
                audioSource.PlayOneShot(successSound);
        }
        else
        {
            resultText.text = "You Fogot Passport!";
            resultText.color = Color.red;

            if (errorSound != null)
                audioSource.PlayOneShot(errorSound);
        }
    }
}
