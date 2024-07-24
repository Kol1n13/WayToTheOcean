using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelCompleted = false;

    private void Start() => finishSound = GetComponent<AudioSource>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayerCollision(collision) && !levelCompleted)
        {
            PlayFinishSound();
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
            Checkpoint.isCheckpointCollect = false;
        }
    }

    private bool IsPlayerCollision(Collider2D collision)
    {
        string gameObjectName = collision.gameObject.name;
        return gameObjectName == "Player" || gameObjectName == "PlayerWithGun";
    }

    private void PlayFinishSound() => finishSound.Play();

    private void CompleteLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    private void PauseGame() => Time.timeScale = 0;
}
