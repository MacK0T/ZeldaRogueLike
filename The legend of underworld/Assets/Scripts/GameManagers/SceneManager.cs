using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ScreenFader))]
public class SceneManager : MonoBehaviour {
    
    private ScreenFader _scenefader;
    private float prevtime = 1;

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
        StartCoroutine(ChangeLevel(Application.loadedLevel));
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
        Application.LoadLevel(level);
    }

    void Awake()
    {
        _scenefader = transform.GetComponent<ScreenFader>();
    }
}
