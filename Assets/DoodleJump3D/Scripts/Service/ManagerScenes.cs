using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ManagerScenes : MonoBehaviour
{
    public static ManagerScenes Instance { get; private set; }
    public string NameActiveScene => SceneManager.GetActiveScene().name;
    [HideInInspector]
    public UnityEvent LoadingSceneEventHandler = new UnityEvent();
    [HideInInspector]
    public UnityEvent StartLoadingSceneEventHandler = new UnityEvent();

    public void LoadAsyncFromCoroutine(string nameScene) => StartCoroutine(LoadAsync(nameScene));

    public void LoadAsyncFromCoroutine(string nameScene, Action action) => StartCoroutine(LoadAsync(nameScene, action));

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private IEnumerator LoadAsync(string nameScene, Action action = null)
    {
        var operation = SceneManager.LoadSceneAsync(nameScene, LoadSceneMode.Single);
        StartLoadingSceneEventHandler?.Invoke();

        while (operation.progress < 0.8)
        {
            var progressInPercent = (int)(operation.progress * 100);
            LoadingSceneEventHandler?.Invoke();

            yield return null;
        }

        if (action != null)
            action.Invoke();
    }
}
