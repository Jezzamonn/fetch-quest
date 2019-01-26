using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour
{

    private static NetworkManager instance;

    public SocketIOComponent socket;

    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
            // if not, set instance to this
            instance = this;
        
        // If instance already exists and it's not this:
        else if (instance != this)
            // Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        socket.On("reaction", HandleReaction);
    }

    void HandleReaction(SocketIOEvent socketIOEvent) {
        string reaction = socketIOEvent.data.ToString();

        if (reaction.Contains("good")) {
            FeedbackManager.feedbackQueue.Enqueue(true);
        }
        else if (reaction.Contains("bad")) {
            FeedbackManager.feedbackQueue.Enqueue(false);
        }
        else {
            Debug.LogError("Don't know how to deal with reaction " + reaction);
        }
    }
}
