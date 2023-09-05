using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KatSkillChoise : MonoBehaviour
{
    public Skills[] skills;
    
    [Space]
    [Space]
    [Space]

    public Color CurrentSkillColor;
    public Color SkillDefaultColor;

    public Text SkillDescription;
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

    public void SkillSelected(int i) 
    {
        SkillDescription.text = skills[i].Description;
    }
}

[System.Serializable]
public struct Skills 
{
    public string SkillName;
    [TextArea]
    public string Description;

}
