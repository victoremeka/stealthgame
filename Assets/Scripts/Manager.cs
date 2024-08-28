using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    Guard[] guards;
    public GameObject gameOverScreen, player;
    public TextMeshProUGUI gameOverText;

    Vector3 originalPosition;


    void Awake(){
        guards = FindObjectsOfType<Guard>();
        foreach (Guard guard in guards)
        {
            guard.FoundPlayer += ResetGame;
        }
    }

    void Start(){
        originalPosition = player.transform.position;
    }

    void Update(){

    }

    void ResetGame(){
        gameOverScreen.SetActive(true);
        StopAllCoroutines();
        gameOverText.text = null;
        StartCoroutine(AnimateText("what are you?"));
        player.transform.position = originalPosition;
    }

    IEnumerator AnimateText(string text){
        yield return new WaitForSeconds(1.5f);
        foreach (char letter in text)
        {
            gameOverText.text += letter;
            yield return new WaitForSeconds(.3f);
        }
        yield return new WaitForSeconds(2);
    }


}