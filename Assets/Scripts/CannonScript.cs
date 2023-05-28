using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bullet;
    public float startTimeBetween = 1f;

    private float timeBetween;
    [SerializeField] private AudioSource cannonSound;

    private void Start()
    {
        timeBetween = startTimeBetween;
    }

    private void Update()
    {
        if (timeBetween <= 0)
        {
            FireBullet();
            timeBetween = startTimeBetween;
        }
        else
        {
            timeBetween -= Time.deltaTime;
        }
    }

    private void FireBullet()
    {
        Instantiate(bullet, firepoint.position, firepoint.rotation);
        PlayCannonSound();
    }

    private void PlayCannonSound()
    {
        if (cannonSound != null)
        {
            cannonSound.Play();
        }
    }
}
