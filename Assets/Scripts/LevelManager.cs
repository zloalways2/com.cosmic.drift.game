using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private Button[] _levelButtons;
    [SerializeField] private GameObject _levelsCanvas;

    [SerializeField] private Sprite _lockedSprite;
    [SerializeField] private Sprite _unlockedSprite;

    private int _lastLevel;
    private int _currentLevel;

    private void Start()
    {
        LoadGameProgress();
        InitializeButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerPrefs.DeleteAll();
            LoadGameProgress();
            InitializeButtons();
        }
    }

    public int GetCurrentLevel()
    {
        return _currentLevel;
    }

    public int GetLastLevel()
    {
        return _lastLevel;
    }

    public void CompleteLevel(int starsCollected)
    {
        if (_currentLevel+1 == _lastLevel)
        {
            _lastLevel++;
        }
        SaveGameProgress(starsCollected);
        InitializeButtons();
    }

    private void InitializeButtons()
    {
        for (int i = 0; i < _levelButtons.Length; i++)
        {
            int level = i + 1;
            Text buttonText = _levelButtons[i].GetComponentInChildren<Text>();

            _levelButtons[i].onClick.RemoveAllListeners();
            _levelButtons[i].onClick.AddListener(() => OnLevelButtonClicked(level));

            if (level <= _lastLevel)
            {
                _levelButtons[i].interactable = true;
                buttonText.text = level.ToString();
                _levelButtons[i].GetComponent<Image>().sprite = _unlockedSprite;
            }
            else
            {
                _levelButtons[i].interactable = false;
                buttonText.text = string.Empty;
                _levelButtons[i].GetComponent<Image>().sprite = _lockedSprite;
            }
        }
    }

    private void OnLevelButtonClicked(int level)
    {
        if (level <= _lastLevel)
        {
            _currentLevel = level - 1;
            PlayerPrefs.SetInt("CurrentLevel", _currentLevel);   
            OpenLevel();
        }
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        if (_currentLevel < _lastLevel)
        {
            _currentLevel++;
            OpenLevel();           
        }
    }

    private void SaveGameProgress(int starsCollected)
    {
        PlayerPrefs.SetInt("LastLevel", _lastLevel);
        PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        PlayerPrefs.Save();
    }

    private void LoadGameProgress()
    {
        _lastLevel = PlayerPrefs.GetInt("LastLevel", 1);
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    }
}
