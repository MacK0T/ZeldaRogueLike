using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public HealthBar healthBar { get; private set; }

	void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
	}
	
}
