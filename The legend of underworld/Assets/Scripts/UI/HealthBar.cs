using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject _heartPrefab;
    [SerializeField]
    private int hpPerHeart;

    private List<Image> _hearts = new List<Image>();
	
	public void SpawnHearts(int maxhealth, int currenthealth)
    {
        AddHearts(maxhealth / hpPerHeart);
        UpdateHealth(currenthealth);
    }

    public void AddHearts(int heartCount)
    {
        for (int i = 0; i < heartCount; i++)
        {
            GameObject g = GameObject.Instantiate(_heartPrefab) as GameObject;
            g.transform.SetParent(this.transform, false);
            _hearts.Add(g.transform.GetChild(0).GetComponent<Image>());
        }
    }

    public void UpdateHealth(int newHealth, int delta = 0)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].fillAmount = Mathf.Clamp((float)newHealth / (float)hpPerHeart, 0f, 1f);
            newHealth -= hpPerHeart;
        }
    }
    
    
    private void Start()
    {
        var temp = GameManager.Instance.player.health;
        SpawnHearts(temp.maxValue, temp.value);
        temp.onChanged += UpdateHealth;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance.player != null)
        GameManager.Instance.player.health.onChanged -= UpdateHealth;
    }
}
