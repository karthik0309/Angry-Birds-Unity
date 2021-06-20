using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Lives : MonoBehaviour
{
    public static AudioClip defeat;
    static AudioSource audiosrc;
    public TextMeshProUGUI livesLeft;
    Bird bird;
    int count,x=1;

    public void Awake()
    {
        livesLeft = GetComponent<TextMeshProUGUI>();
        bird = FindObjectOfType<Bird>();
        audiosrc = GetComponent<AudioSource>();
        defeat = Resources.Load<AudioClip>("Defeat");
    }

    public void Update()
    {
        count = bird.lives;
        livesLeft.text = "Lives: " + count.ToString();
        if (count == 0 && x==1)
        {
            audiosrc.PlayOneShot(defeat);
            x = 0;
            StartCoroutine(ResetAfterDelay());
        }
        
    }
     IEnumerator ResetAfterDelay(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Home");
    }
}
