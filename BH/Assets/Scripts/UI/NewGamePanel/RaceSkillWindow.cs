using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceSkillWindow : MonoBehaviour
{
    public ButtonSkill BtnMainSkill { get => btnMainSkill; }
    public ButtonSkill BtnSecondSkill { get => btnSecondSkill; }

    [SerializeField] private ButtonSkill btnMainSkill = null;
    [SerializeField] private ButtonSkill btnSecondSkill = null;

    public void SetupSkills()
    {
        btnMainSkill.Setup(GameManager.Instance.CharacterCreator.RaceData.MainRaceSkill);
        btnSecondSkill.Setup(GameManager.Instance.CharacterCreator.RaceData.SecondRaceSkill);
    }
}
