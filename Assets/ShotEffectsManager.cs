using System.Collections;
using UnityEngine;

public class ShotEffectsManager : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource gunAudio;
    [SerializeField] ParticleSystem impact;

    private Transform player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    //Play muzzle flash and audio
    public void PlayShotEffects()
    {
        muzzleFlash.Stop(true);
        muzzleFlash.Play(true);
        gunAudio.Stop();
        gunAudio.Play();

        Ray ray = new Ray(transform.position, transform.forward + new Vector3(Random.Range(-0.2f,0.2f), Random.Range(-0.1f,0.1f),0));
        Debug.DrawRay(ray.origin, ray.direction * 30f, Color.red, 1f);

        RaycastHit hit;

        bool result = Physics.Raycast(ray, out hit, 100f);

        if (result)
        {
            if(Vector3.Distance(hit.point, player.position) < 3f)
            {
                player.gameObject.SendMessage("Damaged", 25f, SendMessageOptions.DontRequireReceiver);
            }

            ParticleSystem impactInstance = Instantiate(impact, hit.point, Quaternion.Euler(Vector3.zero));

            Destroy(impactInstance.gameObject, 2f);
        }
    }
}