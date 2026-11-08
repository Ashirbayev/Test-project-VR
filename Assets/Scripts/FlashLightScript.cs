using UnityEngine;

public class FlashLightScript : MonoBehaviour
{
    public Material lens;
    private Light _light;
    private AudioSource _audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _light = GetComponentInChildren<Light>();
        _audioSource = GetComponent<AudioSource>();

    }

    public void FlashLightOn()
    {
        _audioSource.Play();
        lens.EnableKeyword("_EMISSION");
        _light.enabled = true;

    }

    public void FlashLightOff()
    {
        _audioSource.Play();
        lens.DisableKeyword("_EMISSION");
        _light.enabled = false;
    }
}
