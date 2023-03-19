using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "Server", menuName = "Natan/Server", order = 1)]
public class Server : ScriptableObject
{
    public string server;
    public Service[] Services;

    public bool isBusy = false;
    public Answers Answers;

    public IEnumerator ConsumeService(string name, string[] data)
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
            Answers = new Answers();
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            Answers = JsonUtility.FromJson<Answers>(www.downloadHandler.text);
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

    public Answers()
    {
        Code = 404;
        Messege = "Error";
    }

}