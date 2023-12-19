using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{

    public string sceneLoadName;
    public Text progres;
    public Slider SliderProgress;
    public float currentPercent;
    public GameObject button;
    private AsyncOperation loadAsync;
    private void Start()
    {
        StartCoroutine(LoadScene(sceneLoadName));
    }


    IEnumerator LoadScene(string sceneToLoad)
    {
        progres.text = " Loading..";

        loadAsync = SceneManager.LoadSceneAsync(sceneToLoad);

        loadAsync.allowSceneActivation = false;
        while (!loadAsync.isDone)
        {
            currentPercent = loadAsync.progress * 100 / 0.9f;
            progres.text = "Loading.." + currentPercent.ToString("00") + "%";
            button.SetActive(true);
            yield return null;
        }

    }
    void Update()
    {
        SliderProgress.value = Mathf.MoveTowards(SliderProgress.value, currentPercent, 10 * Time.deltaTime);
    }

    public void StartScene() 
    {
        loadAsync.allowSceneActivation = true;
    }
}
