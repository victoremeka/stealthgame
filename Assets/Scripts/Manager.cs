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
        "there is no point, no meaning.",
        "would you care for a chocolate chip?"
    };
    public GameObject levelCompleteScreen;
    public TextMeshProUGUI levelCompleteText;

    void Awake(){
        guards = FindObjectsOfType<Guard>();
        player.GetComponent<Player>().ReachedDestination += LevelComplete;
        foreach (Guard guard in guards)
        {
            guard.FoundPlayer += ResetGame;
        }
    }

    void Start(){
        originalPosition = player.transform.position;
        index = 0;
    }

    void LevelComplete(){
        levelCompleteScreen.SetActive(true);
        levelCompleteText.text = null;
        StopAllCoroutines();
        StartCoroutine(AnimateText("keep moving.", levelCompleteText));
    }

    void ResetGame(){
        index %= convo.Length;
        gameOverScreen.SetActive(true);
        StopAllCoroutines();
        gameOverText.text = null;
        StartCoroutine(AnimateText(convo[index], gameOverText));
        player.transform.position = originalPosition;
        index++;
    }

    IEnumerator AnimateText(string text, TextMeshProUGUI target){
        yield return new WaitForSeconds(1.5f);
        foreach (char letter in text)
        {
            target.text += letter;
            yield return new WaitForSeconds(.12f);
        }
        yield return new WaitForSeconds(3f);
        target.text = null;
    }


}