using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputWindowUI : MonoBehaviour
{
    [SerializeField] GameObject scoreManagerObject;
    [SerializeField] TextMeshProUGUI playerName;

    ScoreManager scoreManager;
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        //playerName = GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        Debug.Log(scoreManager);
        scoreManager.AddScore(new Score(playerName.text, Keys.keys));
        scoreManager.LoadScores();
        Keys.keys = 0;
    }
}
