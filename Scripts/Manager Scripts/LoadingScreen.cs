using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;

    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private Image loadingBar;

    #region Singelton
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    #endregion




    public void ShowLoadingScreen(string scene)
    {
        StartCoroutine(LoadASync(scene));
    }

    private IEnumerator LoadASync(string level)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(level, LoadSceneMode.Single);
        loadingPanel.SetActive(true);

        if(op == null)
        {
            Debug.LogError("Unable to load level due to incorrect name or scene was not added to unity");
        }

        while (!op.isDone)
        {
            float loadingProgress = op.progress / 0.9f;
            loadingBar.fillAmount = loadingProgress;

            if(loadingProgress >= 1f)
            {
                loadingPanel.SetActive(false);
            }

            yield return null;
        }

       
    }


}
