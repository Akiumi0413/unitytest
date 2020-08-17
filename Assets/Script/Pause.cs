using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenu;
    public AudioMixer audiomixer;
    public void pausegame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void setVolume(float value)
    {
        audiomixer.SetFloat("MainVolume",value);
    }
}
