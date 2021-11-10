using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    public bool CheckCanTakeDamage(int valueToUse);
    public void Death();
    public void TakeDamage(SkillOffenseStat skillOffenseStat);
}
