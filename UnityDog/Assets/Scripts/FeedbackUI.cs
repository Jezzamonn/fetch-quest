using UnityEngine;

public class FeedbackUI : MonoBehaviour
{
    public bool isPositive;

    [SerializeField] private AnimationCurve xCurve;

    private readonly Timer animTimer = new Timer();

    private RectTransform rectTransform;
    private float initialX;
    private float animStartX;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initialX = rectTransform.anchoredPosition.x;
        gameObject.SetActive(false);
    }

    public void Show(float maxYOffset)
    {
        rectTransform.anchoredPosition = new Vector2(initialX, Random.Range(-maxYOffset, maxYOffset));
        rectTransform.localScale = Vector3.one;

        //move to random side
        if (Random.value > 0.5f)
        {
            rectTransform.anchoredPosition = new Vector2(-rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
            rectTransform.localScale = new Vector3(-rectTransform.localScale.x, rectTransform.localScale.y, rectTransform.localScale.z);
        }

        animStartX = rectTransform.anchoredPosition.x;
        animTimer.Start(GetLastCurveTime(xCurve));

        gameObject.SetActive(true);
    }

    private void Update()
    {
        animTimer.Tick(Time.deltaTime);

        float x = animStartX - Mathf.Sign(animStartX) * xCurve.Evaluate(animTimer.ClampedTime());
        float y = rectTransform.anchoredPosition.y;
        rectTransform.anchoredPosition = new Vector2(x, y);

        if (animTimer.IsDone())
        {
            FeedbackManager.Return(this);
            gameObject.SetActive(false);
        }
    }

    private static float GetLastCurveTime(AnimationCurve curve)
    {
        int lastKey = curve.keys.Length - 1;
        return curve.keys[lastKey].time;
    }
}
