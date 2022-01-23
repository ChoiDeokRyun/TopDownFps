using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    public Character CameraOwner;

    private void FollowOwner()
    {
        if(CameraOwner)
        {
            Vector3 followPos = new Vector3(CameraOwner.transform.position.x, transform.position.y, CameraOwner.transform.position.z);
            transform.position = followPos;
        }
    }

    void Update()
    {
        FollowOwner();
    }
}
