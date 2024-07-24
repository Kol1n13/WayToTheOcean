using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Vector3 checkpointPosition;
    public static bool isCheckpointCollect = false;
    [SerializeField] private AudioSource collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            checkpointPosition = collision.transform.position;
            isCheckpointCollect = true;
            collectSound.Play();
        }
    }
}