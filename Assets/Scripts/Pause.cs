using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    // hi hi ha ha
    [SerializeField] private GameObject panelPause;

    public void PauseGame()
    {
        Time.timeScale = 0;
        panelPause.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        panelPause.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("StartScene");
        ItemCollecter.isGunCollected = false;
        Time.timeScale = 1;
    }
}
