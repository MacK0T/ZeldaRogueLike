using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScreenFader))]
public class SceneController : MonoBehaviour {
    
    private ScreenFader _scenefader;

    public void Load(int level)
    {
        StartCoroutine(ChangeLevel(level));
    }

    /*
    public void LoadPrevious()
    {
        if(Toolbox.Instance._prevLevel!=0)
        StartCoroutine(ChangeLevel(Toolbox.Instance._prevLevel));
    }
    */

    public void ReloadCurrent()
    {
        StartCoroutine(ChangeLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void CloseApplication()
    {
        StartCoroutine(QuitApplication());
    }
    
    private IEnumerator QuitApplication()
    {
        //while (_isFading)
        //    yield return null;
        yield return StartCoroutine(_scenefader.Fade(false));
        Application.Quit();
    }

    private IEnumerator ChangeLevel(int level)
    {
        yield return StartCoroutine(_scenefader.Fade(false));
        SceneManager.LoadScene(level);
    }

    void Awake()
    {
        _scenefader = transform.GetComponent<ScreenFader>();
    }
}
