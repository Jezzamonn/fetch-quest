using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : MonoBehaviour
{
    [SerializeField] private FeedbackUI positivePrefab;
    [SerializeField] private FeedbackUI negativePrefab;
    [SerializeField] private int initialPoolCapacity;


    public static Queue<bool> feedbackQueue = new Queue<bool>(50);

    private static readonly Queue<FeedbackUI> positivePool = new Queue<FeedbackUI>(100);
    private static readonly Queue<FeedbackUI> negativePool = new Queue<FeedbackUI>(100);

    private RectTransform rectTransform;

    private void Awake()
    {
        positivePool.Clear();
        negativePool.Clear();

        for (int i = 0; i < initialPoolCapacity; ++i)
        {
            positivePool.Enqueue(Instantiate(positivePrefab, transform));
            negativePool.Enqueue(Instantiate(negativePrefab, transform));
        }

        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // DEBUG ONLY ///////////////////////////////////////
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            feedbackQueue.Enqueue(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            feedbackQueue.Enqueue(false);
        }
        /////////////////////////////////////////////////////

        ProcessQueue();
    }

    private void ProcessQueue()
    {
        int total = feedbackQueue.Count;
        for (int i = 0; i < total; ++i)
        {
            bool isPositive = feedbackQueue.Dequeue();
            FeedbackUI feedbackUI =
                isPositive ?
                GetFeedbackUI(positivePool, positivePrefab) :
                GetFeedbackUI(negativePool, negativePrefab);

            feedbackUI.Show(rectTransform.rect.height * 0.5f);
        }
    }

    private FeedbackUI GetFeedbackUI(Queue<FeedbackUI> pool, FeedbackUI prefab)
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }

        return Instantiate(positivePrefab, transform);
    }

    public static void Return(FeedbackUI feedbackUI)
    {
        if (feedbackUI.isPositive)
        {
            positivePool.Enqueue(feedbackUI);
        }
        else
        {
            negativePool.Enqueue(feedbackUI);
        }
    }
}
