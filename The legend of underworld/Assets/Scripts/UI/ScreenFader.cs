using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour {

    public float _fadeTime = 1f;
    public bool _isFading { get; private set; }
    private Image _image;

    private void Awake()
    {
        _image = gameObject.GetComponent<Image>();
        _isFading = true;
        StartCoroutine(Fade(true));
    }

    public IEnumerator Fade(bool sceneFadeIn)
    {
        _isFading = true;
        
            if (sceneFadeIn)
                _image.CrossFadeAlpha(0f, _fadeTime, false);//осветление
            else
                _image.CrossFadeAlpha(1f, _fadeTime, false);//затемнение

            yield return new WaitForSeconds(_fadeTime);
        _isFading = false;
    }
}