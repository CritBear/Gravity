using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserControl : MonoBehaviour {

    public ParticleSystem helpEffect_90Prefab;
    public ParticleSystem helpEffect_180Prefab;

    ParticleSystem helpEffect_90;
    ParticleSystem helpEffect_180;
    Renderer helpEffect_90Rend;
    Renderer helpEffect_180Rend;
    GameObject world;

    public float rotateSpeed = 1f;
    Vector3 originGravity;
    
    float rotateTime = 0.5f;
    int isGravityZero;
    bool isRotateHelpOn = false;

    private void Start()
    {
        world = GameObject.Find("World");
        helpEffect_90 = Instantiate(helpEffect_90Prefab);
        helpEffect_180 = Instantiate(helpEffect_180Prefab);
        helpEffect_90Rend = helpEffect_90.GetComponent<Renderer>();
        helpEffect_180Rend = helpEffect_180.GetComponent<Renderer>();

        originGravity = Physics.gravity;
        
    }

    private void Update()
    {
        OnChangeGravityUp();
        OnChangeGravityForward();

        OnRotateHelp();
        if (isRotateHelpOn)
        {
            PlayHelpEffect();
        }

    }

    IEnumerator RotateWorld(Vector3 axis, float rotateAmount) //RotateAround를 Slerp형태로
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

    IEnumerator GravityZero() //World가 회전하는 동안
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

    void OnRotateHelp()
    {
        if (Input.GetKeyDown("x"))
        {
            isRotateHelpOn = !isRotateHelpOn;
            if (!isRotateHelpOn)
            {
                if (helpEffect_90Rend.enabled)
                {
                    helpEffect_90Rend.enabled = false;
                }
                if (helpEffect_180Rend.enabled)
                {
                    helpEffect_180Rend.enabled = false;
                }
            }
        }
    }

    void PlayHelpEffect()
    {
        RaycastHit hit1, hit2;
        Ray ray1, ray2;

        Vector3 origin = transform.position + new Vector3(0, 0.4f, 0);//z = 1.0f
        Vector3 direction;

        if (transform.eulerAngles.y > 45 && transform.eulerAngles.y <= 135)
        {
            direction = new Vector3(1, 0, 0);
        }
        else if (transform.eulerAngles.y > 135 && transform.eulerAngles.y <= 225)
        {
            direction = new Vector3(0, 0, -1);
        }
        else if (transform.eulerAngles.y > 225 && transform.eulerAngles.y <= 275)
        {
            direction = new Vector3(-1, 0, 0);
        }
        else
        {
            direction = new Vector3(0, 0, 1);
        }

        ray1 = new Ray(origin, direction);
        ray2 = new Ray(origin, new Vector3(0, 1, 0));
        bool result1 = Physics.Raycast(ray1, out hit1, 100f);
        bool result2 = Physics.Raycast(ray2, out hit2, 100f);

        helpEffect_90.transform.position = hit1.point - direction * 0.3f;
        helpEffect_90.transform.rotation = Quaternion.LookRotation(direction);

        helpEffect_180.transform.position = hit2.point - new Vector3(0, 1, 0) * 0.3f;
        helpEffect_180.transform.rotation = Quaternion.LookRotation(new Vector3(0, 1, 0));

        if (result1)
        {
            if (!helpEffect_90Rend.enabled)
            {
                helpEffect_90Rend.enabled = true;
            }
        }
        else
        {
            if (helpEffect_90Rend.enabled)
            {
                helpEffect_90Rend.enabled = false;
            }
        }

        if (result2)
        {
            if (!helpEffect_180Rend.enabled)
            {
                helpEffect_180Rend.enabled = true;
            }
        }
        else
        {
            if (helpEffect_180Rend.enabled)
            {
                helpEffect_180Rend.enabled = false;
            }
        }

    }

}
