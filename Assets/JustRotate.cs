using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustRotate : MonoBehaviour {

    private void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 5f);
    }
}
