using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    // 0: Мут, 1: Не мут
    [SerializeField] private GameObject[] _soundButtons;
    [SerializeField] private GameObject[] _musicButtons; 
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSound;

    // Состояние (0: Мут, 1: Не мут)
    private int _soundInfo; 
    private int _musicInfo;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void ToggleSound()
    {
        _soundInfo = 1 - _soundInfo;
        PlayerPrefs.SetInt("SoundInfo", _soundInfo);
        ApplySettings(_soundInfo, "Sound", _soundButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void ToggleMusic()
    {
        _musicInfo = 1 - _musicInfo;
        PlayerPrefs.SetInt("MusicInfo", _musicInfo);
        ApplySettings(_musicInfo, "Music", _musicButtons);
        _audioSource.PlayOneShot(_buttonSound);
    }

    private void Initialize()
    {
        _soundInfo = PlayerPrefs.GetInt("SoundInfo", 1);
        _musicInfo = PlayerPrefs.GetInt("MusicInfo", 1);

        ApplySettings(_soundInfo, "Sound", _soundButtons);
        ApplySettings(_musicInfo, "Music", _musicButtons);
    }

    private void ApplySettings(int setting, string parameterName, GameObject[] buttons)
    {
        bool isMuted = setting == 0;
        buttons[0].SetActive(isMuted);
        buttons[1].SetActive(!isMuted);
        _audioMixer.SetFloat(parameterName, isMuted ? -80f : 0f);
    }
    private void ApplySettings(int setting, GameObject[] buttons)
    {
        bool isMuted = setting == 0;
        buttons[0].SetActive(isMuted);
        buttons[1].SetActive(!isMuted);
    }
}
