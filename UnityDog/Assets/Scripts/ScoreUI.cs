using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private static string SCORE_FORMAT = "SCORE: {0}";

    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void UpdateScore(int newScore)
    {
        text.text = string.Format(SCORE_FORMAT, newScore);
    }
}
