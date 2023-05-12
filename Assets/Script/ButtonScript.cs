using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void EnablePanel(GameObject Panel)
    {
        Panel.SetActive(true);
    }
    public void DisablePanel(GameObject Panel)
    {
        Panel.SetActive(false);
    }
}
