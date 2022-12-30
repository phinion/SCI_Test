using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    [SerializeField] private GameObject failMenu;
    [SerializeField] private GameObject successMenu;

    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        failMenu.SetActive(false);
        successMenu.SetActive(false);
        pauseMenu.SetActive(false);

        pauseButton.SetActive(true);

        PlayerCharacter player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
        player.OnDeadPlayerCallback += OpenFailMenu;
    }

    private void OpenFailMenu()
    {
        failMenu.SetActive(true);
    }

    public void OpenPauseMenu()
    {
        pauseButton.SetActive(false);

        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeFromPauseMenu()
    {
        pauseButton.SetActive(true);

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

}
