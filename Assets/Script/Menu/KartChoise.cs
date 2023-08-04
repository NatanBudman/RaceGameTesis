using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartChoise : MonoBehaviour
{
    public Image Character;
    public Image CharacterModel;

    [Header("Kart")]
    public KartStats[] Stats;
    public KartStats CurrentStats;
    public int _indexStats = 0;

    public Image Speed;
    public Image Acceleration;
    public Image Handling;

    public float MaxSpeed;
    public float MaxAcce;
    public float MaxHandling;


    public bool isSelectedCharacter = false;
    public GameObject Continue;
    // Start is called before the first frame update
    void Start()
    {
        Continue.SetActive(isSelectedCharacter);
        CurrentStats = Stats[_indexStats];
    }

    // Update is called once per frame
    void Update()
    {
        Speed.fillAmount = CurrentStats.MaxSpeed / MaxSpeed;
        Acceleration.fillAmount = CurrentStats.MaxAcceleration / MaxAcce;
        Handling.fillAmount = CurrentStats.SteerDirSpeed / MaxHandling;
    }
    public void SelectedStats(int positive) 
    {
        if (_indexStats < Stats.Length - 1 && positive > 0)
        {
            _indexStats++;
        }
        else if (_indexStats >= 1 && positive < 0) 
        {
            _indexStats--;
        }

        CurrentStats = Stats[_indexStats];
    }
    public void SelectedCharacter(Image Selection) 
    {
        Character.sprite = Selection.sprite;
        isSelectedCharacter = true;
        Continue.SetActive(isSelectedCharacter);

    }
    public void SelectedModel(Sprite Selection)
    {
        CharacterModel.sprite = Selection;
    }
}
