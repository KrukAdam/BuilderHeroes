using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSkill : BasicButton, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<Skill> OnPointerEnterAction = delegate { };
    public event Action OnPointerExitAction = delegate { };

    [SerializeField] private Image imageSkill = null;

    private Skill skill;

    public void Setup(Skill skill)
    {
        this.skill = skill;

        imageSkill.sprite = skill.SkillSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnPointerEnterAction(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerExitAction();
    }

}
