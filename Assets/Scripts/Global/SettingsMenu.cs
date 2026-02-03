using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private const string MusicParam = "MusicVolume";
    private const string SfxParam = "SfxVolume";

    private void OnEnable()
    {
        LoadVolumes();
    }

    private void LoadVolumes()
    {
        var music = PlayerPrefs.GetFloat(MusicParam, 0.75f);
        var sfx = PlayerPrefs.GetFloat(SfxParam, 0.75f);

        musicSlider.value = music;
        sfxSlider.value = sfx;

        ApplyVolume(MusicParam, music);
        ApplyVolume(SfxParam, sfx);
    }

    public void SetMusicVolume(float value)
    {
        ApplyVolume(MusicParam, value);
        PlayerPrefs.SetFloat(MusicParam, value);
    }

    public void SetSfxVolume(float value)
    {
        ApplyVolume(SfxParam, value);
        PlayerPrefs.SetFloat(SfxParam, value);
    }

    private void ApplyVolume(string parameter, float value)
    {
        var dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(parameter, dB);
    }

    public void Close()
    {
        UIManager.Instance?.CloseSettings();
    }
}