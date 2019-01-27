using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private float transitionTime;

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
                gameObject.SetActive(false);
                EventManager.onGameStart.Dispatch();
            }

            return;
        }

        if (Input.anyKey)
        {
            done = true;
            timer.Start(transitionTime);
        }
    }
}
