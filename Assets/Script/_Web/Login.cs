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
    //Text
    public Text Answer;

    public void StartSession()
    {
        StartCoroutine(StartLog());
        StartCoroutine(GetUserData());
    }


    IEnumerator StartLog()
    {
        LoadIcon.SetActive(true);
        LoginButton.interactable = false;
       
        
        string[] data = new string[2];
        data[0] = InputUser.text;
        data[1] = InputPass.text;
        
        StartCoroutine(_Server.ConsumeService("login", data,AfterLog));

        yield return new WaitForSeconds(2);
        yield return new WaitUntil(() => !_Server.isBusy);
        LoadIcon.SetActive(false);
        LoginButton.interactable = true;
    }

    IEnumerator GetUserData()
    {
        string[] data = new string[2];
        data[0] = InputUser.text;
        data[1] = InputPass.text;
        
        StartCoroutine(_Server.GetUserData("UpdateUserData", data));

        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => !_Server.isBusy);
    }

    private void AfterLog()
    {
        
        switch (_Server.answer.Code)
        {
            case 204: // User or Pass is incorrect
                ModifiText(Answer,true,_Server.answer.Messege,
                    true,255,0,0,1,true);
                Debug.Log("entre");
                break;
            case 205: // Successful login
                ModifiText(Answer,true,_Server.answer.Messege,
                    true,0,255,0,1,true);
                Debug.Log("entre");

                break;
            case 402: // Missing user data
                ModifiText(Answer,true,_Server.answer.Messege,
                    true,255,0,0,1,true);               
                Debug.Log("entre");

                break;
            case 404: // Error
                ModifiText(Answer,true,_Server.answer.Messege,
                    true,255,0,0,1,true);
                Debug.Log("entre");

                break;
        }
        
    }

    void ModifiText(Text text,bool isWriteMessege, string Messege,
        bool isChangeColor, int R,int G,int B,int A, bool isEnableGameObject)
    {
        if (isWriteMessege)
        {
            text.text = Messege;
        }

        if (isChangeColor)
        {
            text.color = new Color(R, G, B, A);
        }

        if (!text.gameObject.activeInHierarchy && !isEnableGameObject)
        {
            text.gameObject.SetActive(false);
        }
        else if(text.gameObject.activeInHierarchy && isEnableGameObject)
        {
            text.gameObject.SetActive(true);
        }
    }
}
