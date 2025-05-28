using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform PTrans;
    public Vector3 offset;
    private void LateUpdate()
    {
        transform.position = PTrans.position + offset;
    }



}
