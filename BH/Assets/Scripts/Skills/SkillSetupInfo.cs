using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetupInfo 
{
    public Transform InteractionPointer;
    public Transform UserTransform;
    public Stats UserStats;

    public SkillSetupInfo(Transform interactionPointer, Transform userTransform, Stats userStats)
    {
        InteractionPointer = interactionPointer;
        UserTransform = userTransform;
        UserStats = userStats;
    }

}
