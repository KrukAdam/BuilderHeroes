using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkill : BasicButton
{
    [SerializeField] private Image imageSkill = null;

    private Skill skill;

    public void Setup(Skill skill)
    {
        this.skill = skill;

        imageSkill.sprite = skill.SkillSprite;
    }
}
