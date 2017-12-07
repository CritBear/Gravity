using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour {

    public Transform sensor;
    public GameObject[] gunPivots = new GameObject[4];
    public AudioSource awakeAudio;
    public AudioClip awakeSound;
    public AudioClip sleepSound;

    private Transform target;
    private Vector3 targetHeadPos = new Vector3(0, 0.7f, 0);
    private Ray ray;
    private RaycastHit hit;
    private bool result;
    private bool gunDelayCheck = true;
    private bool isAwake = false;
    private bool isDetect = false;
    private Quaternion originRotation;

    private float findTargetTime;

    private void Awake()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        originRotation = transform.rotation;
    }

    private void Update()
    {
        if (!isAwake)
        {
            if(Vector3.Distance(target.position, transform.position) < 30)
            {
                isAwake = true;

                awakeAudio.clip = awakeSound;
                awakeAudio.Play();
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, originRotation, 1f);

                return;
            }
        }
        else
        {
            if(Vector3.Distance(target.position, transform.position) < 30 || isDetect)
            {
                findTargetTime = Time.time;
            }
            else if(findTargetTime + 3f < Time.time)
            {
                isAwake = false;

                awakeAudio.clip = sleepSound;
                awakeAudio.Play();
            }
        }

        ray = new Ray(sensor.position, sensor.forward);
        result = Physics.Raycast(ray, out hit, 50f);

        sensor.localScale = new Vector3(1, 1, Vector3.Distance(hit.point, sensor.position) / 3);

        if (result)
        {
            if (hit.transform.tag == "Player")
            {
                isDetect = true;
                if (gunDelayCheck)
                {
                    StartCoroutine(Fire());
                }
            }
            else
            {
                isDetect = false;
            }
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.position - sensor.position), 1.5f);
    }

    IEnumerator Fire()
    {
        gunDelayCheck = false;
        gunPivots[Random.Range(0, 4)].SendMessage("PlayShotEffects", SendMessageOptions.DontRequireReceiver);

        yield return new WaitForSeconds(Random.Range(0.07f, 0.15f));
        gunDelayCheck = true;
    }
}
