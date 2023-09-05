using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public AudioSource confirm;
    public AudioSource confirm2;
    public AudioSource confirm3;
    public AudioSource back;
    public AudioSource error;
    public AudioSource kart_motor;
    public AudioSource select;
    public AudioSource choose;
    public AudioSource options;
    public AudioSource credits;
    public AudioSource chooseSkill;
    public AudioSource selectamap;
    public AudioSource ready;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void EnablePanel(GameObject Panel)
    {
        Panel.SetActive(true);   
    }
    public void DisablePanel(GameObject Panel)
    {
        Panel.SetActive(false); 
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene($"Scenes/{SceneName}");
    }

    public void PlaySoundConfirm()
    {
        confirm.Play();
    }

    public void PlaySoundConfirm2()
    {
        confirm2.Play();
    }

    public void PlaySoundConfirm3()
    {
        confirm3.Play();
    }

    public void PlaySoundKartMotor()
    {
        kart_motor.Play();
    }

    public void PlaySoundSelect()
    {
        select.Play();
    }

    public void PlaySoundBack()
    {
        back.Play();
    }

    public void PlaySoundChoose()
    {
        choose.Play();
    }

    public void PlaySoundChooseSkill()
    {
        chooseSkill.Play();
    }

    public void PlaySoundSelectAMap()
    {
        selectamap.Play();
    }

    public void PlaySoundOptions()
    {
        options.Play();
    }

    public void PLaySoundCredits()
    {
        credits.Play();
    }

    public void PlaySoundReady()
    {
        ready.Play();
    }

    public void PlaySoundError()
    {
        error.Play();
    }

}
