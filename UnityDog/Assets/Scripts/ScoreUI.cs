using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private static string SCORE_FORMAT = "SCORE: {0}";

    [SerializeField] private AnimationCurve xCurve;
    [SerializeField] private AnimationCurve yCurve;
    [SerializeField] private float maxOffset;

    private Text text;
    private RectTransform rt;
    private Vector2 initialPos;
    private bool initialized;
    private readonly Timer timer = new Timer();

    private void Initialize()
    {
        if (initialized)
        {
            return;
        }

        text = GetComponent<Text>();
        rt = GetComponent<RectTransform>();
        initialPos = rt.anchoredPosition;
        initialized = true;
    }

    public void UpdateScore(int newScore)
    {
        Initialize();
        text.text = string.Format(SCORE_FORMAT, newScore);
        timer.Start(FeedbackUI.GetLastCurveTime(xCurve));
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
        rt.position = initialPos +
            new Vector2(xCurve.Evaluate(timer.ClampedTime()), yCurve.Evaluate(timer.ClampedTime()));
    }
}
