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
    int index;
    string[] convo = {
        "what are you?",
        "you try again, why?",
        "existence is futile, humanity is but a fragment of memories. there is no point, no meaning.",
        "would you like a chocolate chip?"
    };


    void Awake(){
        guards = FindObjectsOfType<Guard>();
        foreach (Guard guard in guards)
        {
            guard.FoundPlayer += ResetGame;
        }
    }

    void Start(){
        originalPosition = player.transform.position;
        index = 0;
        
    }

    void Update(){

    }

    void ResetGame(){
        index = index % convo.Length;
        gameOverScreen.SetActive(true);
        StopAllCoroutines();
        gameOverText.text = null;
        StartCoroutine(AnimateText(convo[index]));
        player.transform.position = originalPosition;
        index++;
    }

    IEnumerator AnimateText(string text){
        yield return new WaitForSeconds(1.5f);
        foreach (char letter in text)
        {
            gameOverText.text += letter;
            yield return new WaitForSeconds(.15f);
        }
        yield return new WaitForSeconds(3f);
        gameOverText.text = null;
    }


}