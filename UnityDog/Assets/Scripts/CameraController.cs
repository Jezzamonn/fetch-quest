using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private AnimationCurve orthoAnimCurve;

    private Camera cam;
    private readonly Timer timer = new Timer();

    private void Awake()
    {
        cam = GetComponent<Camera>();

        EventManager.onGoalDone.Register(OnGoalDone);
    }

    private void Destroy()
    {
        EventManager.onGoalDone.Unregister(OnGoalDone);
    }

    private void OnGoalDone()
    {
        timer.Start(FeedbackUI.GetLastCurveTime(orthoAnimCurve));
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
        cam.orthographicSize = orthoAnimCurve.Evaluate(timer.ClampedTime());
    }
}
