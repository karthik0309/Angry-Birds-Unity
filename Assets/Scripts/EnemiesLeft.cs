using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnemiesLeft : MonoBehaviour
{
    public TextMeshProUGUI enemiesLeft;
    Monster[] _monsters;
    int count = 0;
    private void Awake()
    {
        enemiesLeft = GetComponent<TextMeshProUGUI>();
        enemiesLeft.text = "Enemies:" + count.ToString();
    }

    void Update()
    {
        _monsters = FindObjectsOfType<Monster>(); 
        count = _monsters.Length;
        for (int i = 0; i < _monsters.Length; i++)
        {
            if (_monsters[i]._hasDied)
            {
                count--;
            }
        }
        enemiesLeft.text="Enemies:" + count.ToString();
        
    }
}
