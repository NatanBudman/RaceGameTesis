using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Server", menuName = "Natan/Server", order = 1)]
public class Server : ScriptableObject
{
    public string server;
    public Service[] Services;

    public bool isBusy = false;
    public Answers answer;
    public UserData UserData;

    public IEnumerator ConsumeService(string name, string[] data,UnityAction e)
    {
        isBusy = true;
        WWWForm formulari = new WWWForm();
        Service s = new Service();
        
        for (int i = 0; i < Services.Length; i++)
        {
            if (Services[i].name.Equals(name))
            {
                s = Services[i];
            }
        }

        for (int i = 0; i < s.parameters.Length; i++)
        {
            formulari.AddField(s.parameters[i],data[i]);
        }
        
        UnityWebRequest www = UnityWebRequest.Post(server + "/" + s.URL, formulari);
        Debug.Log(server + "/" + s.URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            answer = new Answers();
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            answer = JsonUtility.FromJson<Answers>(www.downloadHandler.text);
        }   

        isBusy = false;

        e.Invoke();

    }
    
    
    // GetData
    
      public IEnumerator GetUserData(string name, string[] data) {
        
        isBusy = true;
        WWWForm formulari = new WWWForm();
        Service s = new Service();
        
        for (int i = 0; i < Services.Length; i++)
        {
            if (Services[i].name.Equals(name))
            {
                s = Services[i];
            }
        }

        for (int i = 0; i < s.parameters.Length; i++)
        {
            formulari.AddField(s.parameters[i],data[i]);
        }
        
        UnityWebRequest www = UnityWebRequest.Post(server + "/" + s.URL, formulari);
        Debug.Log(server + "/" + s.URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            UserData = new UserData();
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            UserData = JsonUtility.FromJson<UserData>(www.downloadHandler.text);
        }   
        
        isBusy = false;
    }
}
[System.Serializable]
public class Service
{
    public string   name;
    public string   URL;
    public string[] parameters;
    
}

[System.Serializable]
public class Answers
{
    public int    Code;
    public string Messege;
    public string Answer;
    

    public Answers()
    {
        this.Code = 404;
        this.Messege = "Error";
    }

}

[System.Serializable]
public class UserData
{
    public int ID;
    public string User;
    public string Pass;
    public string Player;
    public int Level;
}


