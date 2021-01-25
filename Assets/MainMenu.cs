using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    static public bool playMenuSound = false;
    public AudioSource menuAudioSource;

    private void Start()
    {
        ChangePlayMenuSong(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangePlayMenuSong(bool status)
    {
        playMenuSound = status;
        if (status)
        {
            menuAudioSource.Play();
        }
        else
        {
            menuAudioSource.Pause();
        }
    }
}
