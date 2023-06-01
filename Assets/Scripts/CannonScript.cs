using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public Transform[] firepoints;
    public GameObject bullet;
    public float startTimeBetween = 1f;

    private float timeBetween;
    [SerializeField] private AudioSource cannonSound;
    [SerializeField] private bool isObjectBoss;

    private void Start() => timeBetween = startTimeBetween;

    private void Update()
    {
        if (isObjectBoss && BossLogic.currentPhase == BossLogic.BossPhase.Shoot && BossLogic.isPhaseActive ||
            !isObjectBoss)
            Timer();
    }

    private void Timer()
    {
        if (timeBetween <= 0)
        {
            FireBullet();
            timeBetween = startTimeBetween;
        }
        else
            timeBetween -= Time.deltaTime;
    }

    private void FireBullet()
    {
        if (isObjectBoss)
            foreach (Transform firepoint in firepoints)
                Instantiate(bullet, firepoint.position, firepoint.rotation);
        else
            Instantiate(bullet, firepoints[0].position, firepoints[0].rotation);

        PlayCannonSound();
    }

    private void PlayCannonSound()
    {
        if (cannonSound != null)
            cannonSound.Play();
    }
}
