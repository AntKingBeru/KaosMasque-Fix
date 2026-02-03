using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SfxVolumeController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private const string SfxVolumeKey = "SfxVolume";

    private void Start()
    {
        var savedVolume = PlayerPrefs.GetFloat(SfxVolumeKey, 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float value)
    {
        var dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("SfxVolume", dB);
        
        PlayerPrefs.SetFloat(SfxVolumeKey, value);
    }
}