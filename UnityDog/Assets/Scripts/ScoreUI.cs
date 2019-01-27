using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private static string SCORE_FORMAT = "SCORE: {0}";

    private Text text;
    private bool initialized;

    private void Initialize()
    {
        if (initialized)
        {
            return;
        }

        text = GetComponent<Text>();
        initialized = true;
    }

    public void UpdateScore(int newScore)
    {
        Initialize();
        text.text = string.Format(SCORE_FORMAT, newScore);
    }
}
