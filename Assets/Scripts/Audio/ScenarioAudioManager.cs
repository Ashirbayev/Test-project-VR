using UnityEngine;
using VRTraining.Scenario;

namespace VRTraining.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ScenarioAudioManager : MonoBehaviour
    {
        [Header("Звуковые эффекты")]
        [SerializeField] private AudioClip successClip;
        [SerializeField] private AudioClip wrongActionClip;
        [SerializeField] private AudioClip sequenceViolationClip;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        private void OnEnable()
        {
            ScenarioManager.OnActionSuccess += PlaySuccess;
            ScenarioManager.OnActionWrong += PlayWrongAction;
            ScenarioManager.OnSequenceViolation += PlaySequenceViolation;
        }

        private void OnDisable()
        {
            ScenarioManager.OnActionSuccess -= PlaySuccess;
            ScenarioManager.OnActionWrong -= PlayWrongAction;
            ScenarioManager.OnSequenceViolation -= PlaySequenceViolation;
        }

        private void PlaySuccess()
        {
            PlaySound(successClip);
        }

        private void PlayWrongAction()
        {
            PlaySound(wrongActionClip);
        }

        private void PlaySequenceViolation()
        {
            PlaySound(sequenceViolationClip);
        }

        private void PlaySound(AudioClip clip)
        {
            if (clip != null)
            {
                _audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"[{gameObject.name}] Аудиоклип не назначен!");
            }
        }
    }
}
