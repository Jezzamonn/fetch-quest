using UnityEngine;
using UnityEngine.UI;


public class FloatUp : MonoBehaviour
{
    [SerializeField] private AnimationCurve floatCurve;
    [SerializeField] private float time;

    private Text text;
    private RectTransform rt;
    private Vector2 initialPos;
    private readonly Timer timer = new Timer();

    public void Initialize(string content, Vector2 initialPos)
    {
        text = GetComponent<Text>();
        rt = GetComponent<RectTransform>();

        this.initialPos = initialPos;
        text.text = content;
        timer.duration = floatCurve[floatCurve.length - 1].time;

        rt.localPosition = initialPos;
    }

    public void StartFloating() {
        timer.Start();
    }

    void Update()
    {
        timer.Tick(Time.deltaTime);
        rt.localPosition = initialPos + Vector2.up * floatCurve.Evaluate(timer.ClampedTime());
        if (timer.IsDone())
        {
            // not v. efficient, sorry
            Destroy(this.gameObject);
        }
    }
}
