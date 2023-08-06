using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KatSkillChoise : MonoBehaviour
{

    public Color CurrentSkillColor;
    public Color SkillDefaultColor;


    public Image CurrentSkillSelected;

    public void SkillSelected(Image BackgroundSkill) 
    {
        if (CurrentSkillSelected != null) 
        {
            CurrentSkillSelected.color = SkillDefaultColor;
        }
        CurrentSkillSelected = BackgroundSkill;
        BackgroundSkill.color = CurrentSkillColor;
    }
}
