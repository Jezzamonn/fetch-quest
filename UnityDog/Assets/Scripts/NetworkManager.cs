using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour
{

    public SocketIOComponent socket;
    // Start is called before the first frame update
    void Start()
    {
        socket.On("reaction", HandleReaction);
        // var socket = IO.Socket("http://localhost:3000");
        // socket.On(Socket.EVENT_CONNECT, () => {
        //     // TODO: Connect saying this is a player
        //     socket.Emit("hi");
        // });
        // socket.On("reaction", (message) => {
        //     Debug.Log(message);
        // });
    }

    void HandleReaction(SocketIOEvent socketIOEvent) {
        Debug.Log("we got something...");
        Debug.Log(socketIOEvent.data.ToString());
    }
}
