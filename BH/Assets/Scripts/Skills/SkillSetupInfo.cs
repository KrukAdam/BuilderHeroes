using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSetupInfo 
{
    public Transform InteractionPointer;
    public Transform UserTransform;
    public Stats UserStats;
    public LayerMask AllyLayersMask;
    public LayerMask EnemyLayerMask;
    public Character SkillOwner;

    public SkillSetupInfo(PlayerCharacter playerCharacter, Transform userTransform)
    {
        InteractionPointer = playerCharacter.PlayerActionController.InteractionPointer;
        UserTransform = userTransform;
        UserStats = playerCharacter.Stats;
        AllyLayersMask = playerCharacter.AllyLayersMask;
        EnemyLayerMask = playerCharacter.EnemyLayerMask;
        SkillOwner = playerCharacter;
    }

}
