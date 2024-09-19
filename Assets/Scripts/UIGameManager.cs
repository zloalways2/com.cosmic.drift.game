using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _hearts;

    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject _loseCanvas;
    [SerializeField] private GameObject _nextLevelButton;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _scoreTextWinAfterGame;
    [SerializeField] private Text _scoreTextLoseAfterGame;
    [SerializeField] private Text _textLevelWin;
    [SerializeField] private Text _textLevelLose;
    [SerializeField] private Text _timeText;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _buttonSource;
    [SerializeField] private AudioClip _obstacleSound;
    [SerializeField] private AudioClip _starSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _winSound;

    [SerializeField] private LevelManager _levelManager;

    private int _currentStar;
    private int _currentLevel;
    private int _heartsCount = 3;
    private int _score;
    private int _purposeScore;

    private bool _isPause;

    private float _time = 90;

    private void Start()
    {
        Time.timeScale = 1.0f;
        _currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        _purposeScore = 25 * (25 + _currentLevel * 5);
        _time -= _currentLevel;

        _scoreText.text = $"SCORE\n{_score}/{_purposeScore}";
        _timeText.text = $"TIME:\n{_time}";
    }

    private void Update()
    {
        _time -= Time.deltaTime;
        int minutes = (int)_time / 60;
        int seconds = (int)_time % 60;
        _timeText.text = $"TIME:\n{minutes:00}:{seconds:00}";
    }

    public void PlayObstacleSound()
    {
        _audioSource.PlayOneShot(_obstacleSound);
    }

    public void PlayStarSound()
    {
        _audioSource.PlayOneShot(_starSound);
    }

    public void NextLevel()
    {
        _levelManager.NextLevel();

    }

    public void Win()
    {
        Time.timeScale = 0f;
        _winCanvas.SetActive(true);
        if (_currentLevel == 10)
        {
            _nextLevelButton.SetActive(false);
        }
        _audioSource.PlayOneShot(_winSound);
        _musicSource.Stop();
        _textLevelWin.text = $"Level {_currentLevel + 1}";
        _scoreTextWinAfterGame.text = $"{_score}/{_purposeScore}";
        CompleteLevel();
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        _loseCanvas.SetActive(true);
        _audioSource.PlayOneShot(_loseSound);
        _musicSource.Stop();
        _textLevelLose.text = $"Level {_currentLevel + 1}";
        _scoreTextLoseAfterGame.text = $"{_score}/{_purposeScore}";
    }
    public void Pause()
    {
        _audioSource.PlayOneShot(_buttonSource);
        _isPause = !_isPause;
        if (_isPause)
        {
            Time.timeScale = 0f;
            _pauseCanvas.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            _pauseCanvas.SetActive(false);
        }
    }

    public void CollectStar()
    {
        _score += 25;
        _scoreText.text = $"SCORE\n{_score}/{_purposeScore}";
        PlayStarSound();
        if (_score >= _purposeScore)
        {
            Win();
        }
    }

    public void ReduceHeart()
    {
        if (_heartsCount > 0)
        {
            _hearts[_heartsCount - 1].SetActive(false);
            _heartsCount--;
            if( _heartsCount <= 0 ) Lose();
        }
    }

    public void Restart()
    {
        _audioSource.PlayOneShot(_buttonSource);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveStarsRecord()
    {
        if (_currentStar > PlayerPrefs.GetInt("RecordStars"))
        {
            PlayerPrefs.SetInt("RecordStars", (int)_currentStar);
            PlayerPrefs.Save();
        }
    }

    public void CompleteLevel()
    {
        _levelManager.CompleteLevel(_currentStar);
    }
}
