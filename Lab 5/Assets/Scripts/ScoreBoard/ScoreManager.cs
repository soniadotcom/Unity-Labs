using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private ScoreData sd;

    [SerializeField] GameObject ScoreUI;

    private void Awake()
    {
        var json = PlayerPrefs.GetString("Scores2", "{}");
        sd = JsonUtility.FromJson<ScoreData>(json);
    }

    public IEnumerable<Score> GetHightScores()
    {
        return sd.scores.OrderByDescending(x => x.score);
    }

    public void AddScore(Score score)
    {
        sd.scores.Add(score);
    }

    private void OnDestroy()
    {
        SaveScore();        
    }

    public void LoadScores()
    {
        var scores1 = ScoreUI.GetComponent<ScoreUI>();
        scores1.DrawScoreBoard();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString("Scores2", json);
    }
}
