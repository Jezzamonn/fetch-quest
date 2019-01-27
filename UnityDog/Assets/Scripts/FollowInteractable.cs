using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowInteractable : MonoBehaviour
{

    public InteractableObject Target;
    public Transform Sprite;

    // Update is called once per frame
    void Update()
    {
        if (Target)
        {
            transform.position = Target.transform.position + Vector3.up * Target.Height;
            Sprite.gameObject.SetActive(true);
        }
        else
        {
            Sprite.gameObject.SetActive(false);
        }
    }
}
