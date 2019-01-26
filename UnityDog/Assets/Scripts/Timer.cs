using UnityEngine;

[System.Serializable]
public class Timer
{
    public float duration;
    public float timer { get; private set; }

    public Timer(float duration = 0, bool startDone = true)
    {
        this.duration = duration;

        if (startDone)
        {
            End();
        }
        else
        {
            Start();
        }
    }

    // Get a 0-1 value for time
    // If we don't have a value duration, timer is always 'done', normalizes to 1
    public float Progress()
    {
        if (duration <= 0)
            return 1f;
        return Mathf.Clamp01(timer / duration);
    }

    // Get the remaining time left of the duration in seconds.
    public float Remaining()
    {
        return Mathf.Max(duration - timer, 0f);
    }

    public bool IsDone()
    {
        return timer >= duration;
    }

    // Increment / decrement the timer by a passed in delta
    public void Tick(float deltaTime)
    {
        timer += deltaTime;
    }

    public void Start()
    {
        timer = 0f;
    }

    public void Start(float inDuration)
    {
        duration = inDuration;
        Start();
    }

    public void End()
    {
        timer = duration;
    }
}
