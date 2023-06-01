using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{

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
        SceneManager.LoadScene("PlotScene");
        ItemCollecter.isGunCollected = false;
        Time.timeScale = 1;
    }
}
