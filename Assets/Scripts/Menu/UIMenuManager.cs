using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _menuButtons;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _levelsCanvas;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _buttonSound;

    private void OnEnable()
    {
        Time.timeScale = 1.0f;
        SetCanvasActive(_menuCanvas, true);
        SetCanvasActive(_settingsCanvas, false);
    }

    private void SetCanvasActive(GameObject canvas, bool isActive)
    {
        canvas.SetActive(isActive);
    }

    public void Levels()
    {
        SetCanvasActive(_menuCanvas, false);
        SetCanvasActive(_levelsCanvas, true);
        _audioSource.PlayOneShot(_buttonSound);        
    }

    public void Settings()
    {
        SetCanvasActive(_menuCanvas, false);
        SetCanvasActive(_settingsCanvas, true);
        _audioSource.PlayOneShot(_buttonSound);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SetCanvasActive(_menuCanvas, true);
        SetCanvasActive(_settingsCanvas, false);
        SetCanvasActive(_levelsCanvas, false);
        _audioSource.PlayOneShot(_buttonSound);
    }
}
