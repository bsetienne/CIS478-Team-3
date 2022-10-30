using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    // we need to test the offset for the camera.
    private Vector2 offset; 
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // using lateupdate to make camera follows fluently. 
    void LateUpdate()
    {
        
        //offset the camera not in the center by adding to the player
        transform.position = player.transform.position + offset;
    }
}
