using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private float waitTime;

    private readonly Timer timer = new Timer();

    private void Awake()
    {
        EventManager.onGameEnd.Register(Show);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.onGameEnd.Unregister(Show);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        timer.Start(waitTime);
    }

    private void Update()
    {
        timer.Tick(Time.deltaTime);
        if (timer.IsDone() && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }
}
