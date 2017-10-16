using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour {

    GameObject world;
    Quaternion targetRotation;

    public float rotateSpeed = 1f;
    Vector3 originGravity;
    
    float rotateTime = 0.5f;
    int isGravityZero;

    private void Start()
    {
        world = GameObject.Find("World");

        originGravity = Physics.gravity;
    }

    private void Update()
    {
        OnChangeGravityUp();
        OnChangeGravityForward();

        world.transform.rotation = Quaternion.Slerp(world.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

    }

    IEnumerator RotateWorld(Vector3 axis, float rotateAmount)
    {
        float step = 0.0f;
        float rate = 1.0f / rotateTime;
        float smoothStep = 0.0f;
        float lastStep = 0.0f;

        while(step < 1.0f)
        {
            step += Time.deltaTime * rate;
            smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step);
            world.transform.RotateAround(transform.position, axis, rotateAmount * (smoothStep - lastStep));
            lastStep = smoothStep;
            yield return null;
        }
        if (step > 1.0) transform.RotateAround(transform.position, axis, rotateAmount * (1.0f - lastStep));

    }//http://answers.unity3d.com/questions/29110/easing-a-rotation-of-rotate-around.html

    IEnumerator GravityZero()
    {
        isGravityZero++;
        if (isGravityZero > 0)
        {
            Physics.gravity = Vector3.zero;
        }

        yield return new WaitForSeconds(rotateTime);

        isGravityZero--;
        if (isGravityZero <= 0)
        {
            Physics.gravity = originGravity;
        }
    }

    void OnChangeGravityUp()
    {
        if (Input.GetKeyDown("q"))
        {
            StartCoroutine(GravityZero());
            StartCoroutine(RotateWorld(new Vector3(-1, 0, 0),180));
        }
    }
    
    void OnChangeGravityForward()
    {
        if (Input.GetKeyDown("e"))
        {
            StartCoroutine(GravityZero());
            if (transform.eulerAngles.y > 45 && transform.eulerAngles.y <= 135)
            {
                StartCoroutine(RotateWorld(new Vector3(0, 0, -1), 90));
            }
            else if(transform.eulerAngles.y > 135 && transform.eulerAngles.y <= 225)
            {
                StartCoroutine(RotateWorld(new Vector3(-1, 0, 0), 90));
            }
            else if (transform.eulerAngles.y > 225 && transform.eulerAngles.y <= 275)
            {
                StartCoroutine(RotateWorld(new Vector3(0, 0, 1), 90));
            }
            else
            {
                StartCoroutine(RotateWorld(new Vector3(1, 0, 0), 90));
            }
        }
    }

}
