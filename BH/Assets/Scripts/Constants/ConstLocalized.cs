using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(menuName = "Game/Constants/Localized")]
public class ConstLocalized : ScriptableObject
{
    //Stats
    public LocalizedString StatMinValue;
    public LocalizedString StatMaxValue;

    //Time
    public LocalizedString ForTime;
    public LocalizedString Seconds;
}
