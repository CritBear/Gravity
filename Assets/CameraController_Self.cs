using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Self : MonoBehaviour {

    public float distanceAway = 10f;
    public float distanceUP = 7f;

    public Transform follow;

    private void Start()
    {
        transform.position = follow.position + Vector3.up * distanceUP - Vector3.forward * distanceAway;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, follow.position + Vector3.up * distanceUP - Vector3.forward * distanceAway, Time.deltaTime * 3);
    }
}