using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public AudioSource confirm;
    public AudioSource confirm2;
    public AudioSource confirm3;
    public AudioSource back;
    public AudioSource select;
    public AudioSource choosecandk;
    public AudioSource chooseskill;
    public AudioSource selectamap;
    public AudioSource readyfortherace;
    public AudioSource racingtour;
    public AudioSource options;
    public AudioSource credits;
    public AudioSource kartmotor;
    public AudioSource error;

    void Start()
    {
        
    }

    
    void Update()
    {
        
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

    public void PlaySoundBack()
    {
        back.Play();
    }

    public void PlaySoundSelect()
    {
        select.Play();
    }

    public void PlaySoundChooseCAndK()
    {
        choosecandk.Play();
    }

    public void PlaySoundChooseSkill()
    {
        chooseskill.Play();
    }

    public void PlaySoundReady()
    {
        readyfortherace.Play();
    }

    public void PlaySoundSelectAMap()
    {
        selectamap.Play();
    }

    public void PlaySoundOptions()
    {
        options.Play();
    }

    public void PlaySoundCredits()
    {
        credits.Play();
    }

    public void PlaySoundKartMotor()
    {
        kartmotor.Play();
    }

    public void PlaySoundRacingTour()
    {
        racingtour.Play();
    }

    public void PlaySoundError()
    {
        error.Play();
    }

}
