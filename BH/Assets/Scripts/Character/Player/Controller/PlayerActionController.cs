using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{

    public Transform InteractionPointer { get => interactionPointer; }

    [SerializeField] private float timeToNextInteraction = 1;
    [SerializeField] private Transform interactionPointer = null;
    [SerializeField] private float interactionRange = 1;
    [SerializeField] private LayerMask interactionObjectLayer;

    private BuildingBuilderManager buildingBuilderManager;
    private EquipmentManager equipmentManager;
    private PlayerCharacter playerCharacter;
    private float baseTimetoNextInteraction;
    private bool interactionOnMap = false;

    private void Start()
    {
        baseTimetoNextInteraction = timeToNextInteraction;

        GameManager.Instance.InputManager.InputController.Player.Interaction.performed += ctx => InteractionOnMapSet(ctx.ReadValue<float>());
    }

    private void Update()
    {
        CalculateTimeToNextInteraction();
        InteractionInit();
    }

    public void Setup(LocalManagers localManagers, PlayerCharacter playerCharacter)
    {
        this.equipmentManager = localManagers.EquipmentManager;
        this.buildingBuilderManager = localManagers.BuildingBuilder;
        this.playerCharacter = playerCharacter;
    }

    public void AddTimeToNextInteraction(float timeBlock, bool resetTime = false)
    {
        if (resetTime)
        {
            timeToNextInteraction = timeBlock;
        }
        else
        {
            if (timeToNextInteraction < timeBlock) timeToNextInteraction = timeBlock;
        }
    }

    private void CalculateTimeToNextInteraction()
    {
        if (timeToNextInteraction <= 0) return;

        timeToNextInteraction -= Time.deltaTime;
    }

    private void InteractionOnMapSet(float interaction)
    {
        interactionOnMap = interaction > 0 ? true : false;
    }

    private void InteractionInit()
    {
        if (timeToNextInteraction > 0 || !interactionOnMap) return;

        if (interactionOnMap)
        {
            timeToNextInteraction = baseTimetoNextInteraction;
            Interaction();
        }
    }

    private void Interaction()
    {
        if (buildingBuilderManager.BuildingSelected)
        {
            buildingBuilderManager.Build();
            return;
        }

        Collider2D[] objectOnRange = Physics2D.OverlapCircleAll(interactionPointer.position, interactionRange, interactionObjectLayer);
        foreach (var obj in objectOnRange)
        {
            if (obj.TryGetComponent(out BaseObjectOnMap objectOnMap))
            {
                objectOnMap.InteractionOnWorldMap(equipmentManager);
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(interactionPointer.position, interactionRange);
    }
}
