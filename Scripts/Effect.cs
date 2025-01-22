using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Effect class is responsible for managing the lifecycle of game effects
public class Effect : MonoBehaviour
{
    // The time after which the effect is destroyed, -1 indicates it will not be destroyed automatically
    public float destoryTime;
    
    // Start is called before the first frame update
    void Start()
    {
        // If the destroy time is set and not equal to -1, then destroy the game object after the specified time
        if (destoryTime!=-1)
        {
            Destroy(gameObject, destoryTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Currently, the Update method has no functionality, but it can be used for future updates or cleanup work
    }
}