using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    Monster[] _monsters;
    [SerializeField] public GameObject LevelCompleteCanvas;
    [SerializeField] public int NextLevel;
    void OnEnable(){
        _monsters=FindObjectsOfType<Monster>();

    }  
    
    void Update(){
        if(MonstersAreAllDead()){
            LevelCompleteCanvas.SetActive(true);
           // GoToNextLevel();
        }
    }

    private bool MonstersAreAllDead(){
        foreach (var monster in _monsters){
            if(monster.gameObject.activeSelf){
                return false;
            }
        }
        return true;
    }

    public void GoToNextLevel(){
        if(NextLevel==3){
            SceneManager.LoadScene("Win");
            return;
        }
        SceneManager.LoadScene("Level"+NextLevel.ToString());
    }
}
