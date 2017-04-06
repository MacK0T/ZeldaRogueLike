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
    List<Image> hearts = new List<Image>();
	
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
            hearts.Add(g.transform.GetChild(0).GetComponent<Image>());
        }
    }

    public void UpdateHealth(int newHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (newHealth > hpPerHeart)
            {
                hearts[i].fillAmount = 1;
            }
            else if (newHealth <= 0)
            {
                hearts[i].fillAmount = 0;
            }
            else
            {
                hearts[i].fillAmount = (float) newHealth / hpPerHeart;
            }
            newHealth -= hpPerHeart;
        }
    }
}
