using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;

    // Start is called before the first frame update
    public void DrawScoreBoard()
    {
        //scoreManager.AddScore(new Score("Pi", 3));
        //scoreManager.AddScore(new Score("Fa", 3));

        Score[] scores = scoreManager.GetHightScores().ToArray();
        for(int i = 0; i < scores.Length && i < 9; i++)
        {
            var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.score.text = scores[i].score.ToString();
        }
    }
}
