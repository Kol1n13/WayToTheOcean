using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    [SerializeField] private AudioSource shootSound;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            PlayShootSound();
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void PlayShootSound()
    {
        if (shootSound != null)
        {
            shootSound.Play();
        }
    }
}
