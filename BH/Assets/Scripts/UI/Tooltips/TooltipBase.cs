using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TooltipBase : MonoBehaviour
{
    [SerializeField] protected TooltipsStatModifierBar modifierBarPrefab = null;
    [SerializeField] protected int modifiersBarsInstantiate = 10;

    protected List<TooltipsStatModifierBar> modifierBars = new List<TooltipsStatModifierBar>();
    protected Color statPositive;
    protected Color statNegative;
    protected readonly StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        InstantiateModifiersBars();
        HideModifiersBars();
        gameObject.SetActive(false);
    }

    public virtual void Setup(Color positive, Color negative)
    {
        statPositive = positive;
        statNegative = negative;
    }

    public virtual void ShowTooltip(CharacterStat stat) { }
    public virtual void ShowTooltip(Item item) { }
    public virtual void ShowTooltip(Skill skill) { }
    public virtual void HideTooltip() { }

    protected virtual void ShowModifiers(CharacterStat characterStat) { }
    protected virtual void ShowModifiers(Item item) { }

    protected void InstantiateModifiersBars()
    {
        for (int i = 0; i < modifiersBarsInstantiate; i++)
        {
            TooltipsStatModifierBar newBar = Instantiate(modifierBarPrefab, gameObject.transform);
            modifierBars.Add(newBar);
        }
    }

    protected void ShowModifierBar(StringBuilder content, int barIndex, Color textColor)
    {
        if (barIndex >= modifierBars.Count)
        {
            for (int i = 0; i < barIndex + 1 - modifierBars.Count; i++)
            {
                TooltipsStatModifierBar newBar = Instantiate(modifierBarPrefab, gameObject.transform);
                modifierBars.Add(newBar);
            }
        }

        modifierBars[barIndex].gameObject.SetActive(true);
        modifierBars[barIndex].SetText(content, textColor);
    }

    protected void HideModifiersBars()
    {
        for (int i = 0; i < modifierBars.Count; i++)
        {
            modifierBars[i].gameObject.SetActive(false);
        }
    }
}
