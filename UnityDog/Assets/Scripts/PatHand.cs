using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatHand : MonoBehaviour
{
    [SerializeField] private AnimationCurve animCurve;

    private RectTransform rectTransform;
    private readonly Timer timer = new Timer();

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        EventManager.onGoalDone.Register(OnGoalDone);
    }

    private void Destroy()
    {
        EventManager.onGoalDone.Unregister(OnGoalDone);
    }

    private void OnGoalDone()
    {
        timer.Start(FeedbackUI.GetLastCurveTime(animCurve));
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
        Vector2 position = rectTransform.anchoredPosition;
        position.x = animCurve.Evaluate(timer.ClampedTime());
        rectTransform.anchoredPosition = position;
    }

}
