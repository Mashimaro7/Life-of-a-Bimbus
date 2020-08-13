using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject loadScreen;
    public GameObject pauseScreen;
    public bool paused;

    void Awake()
    {
        instance = this;

        SceneManager.LoadSceneAsync((int)SceneIndexer.MENU, LoadSceneMode.Additive);
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name + 1);
        }
    }
    public void LoadGame()
    {
        loadScreen.SetActive(true);

        scenesLoading.Add(SceneManager.UnloadSceneAsync((int)SceneIndexer.MENU));
        scenesLoading.Add(SceneManager.LoadSceneAsync((int)SceneIndexer.MAINMAP, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }

        loadScreen.SetActive(false);
    }

    public static IEnumerator RestartLevel(int delay)
    {
        yield return new WaitForSeconds(delay);
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void Pause()
    {
        paused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        paused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
