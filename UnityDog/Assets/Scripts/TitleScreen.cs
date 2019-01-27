using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private float transitionTime;
    [SerializeField] private bool triggerEvent;
    [SerializeField] private GameObject nextScreen;
     
    private CanvasGroup canvasGroup;
    private bool done;
    private readonly Timer timer = new Timer();

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (done)
        {
            timer.Tick(Time.deltaTime);
            canvasGroup.alpha = 1.0f - timer.Progress();

            if (timer.IsDone())
            {
                if (triggerEvent)
                {
                    EventManager.onGameStart.Dispatch();
                }

                gameObject.SetActive(false);
            }

            return;
        }

        if (Input.anyKeyDown)
        {
            done = true;
            timer.Start(transitionTime);

            if (nextScreen != null)
            {
                nextScreen.SetActive(true);
            }
        }
    }
}
