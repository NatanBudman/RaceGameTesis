using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public Server _Server;
    public TMP_InputField InputUser;
    public TMP_InputField InputPass;
    
    
    //Icons
    public GameObject LoadIcon;
    // Log button
    public Button LoginButton;

    public void StartSession()
    {
       
        StartCoroutine(StartLog());
    }


    IEnumerator StartLog()
    {
        LoadIcon.SetActive(true);
        LoginButton.interactable = false;
       
        
        string[] data = new string[2];
        data[0] = InputUser.text;
        data[1] = InputPass.text;
        
        StartCoroutine(_Server.ConsumeService("login", data));

        yield return new WaitForSeconds(2);
        yield return new WaitUntil(() => !_Server.isBusy);
        LoadIcon.SetActive(false);
        LoginButton.interactable = true;
    }
}
