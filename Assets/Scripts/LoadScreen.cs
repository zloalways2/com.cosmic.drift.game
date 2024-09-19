using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _scale;
    [SerializeField] private float fillSpeed = 0.3f;
    [SerializeField] private int _sceneIndex;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Load();
        }
    }

    public void Load()
    {
        _loadingScreen.SetActive(true);
        StartCoroutine(LoadAsync());
    }

    private IEnumerator LoadAsync()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(_sceneIndex);
        loadAsync.allowSceneActivation = false;

        float progress = 0f;
        float targetProgress = 0f;

        while (!loadAsync.isDone)
        {
            targetProgress = loadAsync.progress;

            while (progress < targetProgress)
            {
                progress += Time.deltaTime * fillSpeed;
                _scale.value = Mathf.Clamp01(progress / 0.9f);
                yield return null;
            }

            if (loadAsync.progress >= 0.9f && !loadAsync.allowSceneActivation)
            {
                yield return new WaitForSeconds(1f);
                loadAsync.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
