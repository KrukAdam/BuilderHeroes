using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActionController : MonoBehaviour
{

    public Transform InteractionPointer { get => interactionPointer; }

    [SerializeField] private float timeToNextInteraction = 1;
    [SerializeField] private Transform interactionPointer = null;
    [SerializeField] private float interactionRange = 1;
    [SerializeField] private LayerMask interactionObjectLayer;

    private LocalController localController;
    private PlayerCharacter playerCharacter;
    private float baseTimetoNextInteraction;
    private bool interactionOnMap = false;

    private void Start()
    {
        baseTimetoNextInteraction = timeToNextInteraction;

        GameManager.Instance.InputManager.InputController.Player.Interaction.performed += InteractionOnMapSet;
    }

    private void Update()
    {
        CalculateTimeToNextInteraction();
        InteractionInit();
    }

    private void OnDestroy()
    {
        GameManager.Instance.InputManager.InputController.Player.Interaction.performed -= InteractionOnMapSet;
    }

    public void Setup(LocalController localController, PlayerCharacter playerCharacter)
    {
        this.localController = localController;
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

    private void InteractionOnMapSet(InputAction.CallbackContext context)
    {
        interactionOnMap = context.ReadValue<float>() > 0 ? true : false;
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
        if (localController.LocalManagers.BuildingBuilder.BuildingSelected)
        {
            localController.LocalManagers.BuildingBuilder.Build();
            return;
        }

        Collider2D[] objectOnRange = Physics2D.OverlapCircleAll(interactionPointer.position, interactionRange, interactionObjectLayer);
        foreach (var obj in objectOnRange)
        {
            if (obj.TryGetComponent(out IObjectOnMap objectOnMap))
            {
                objectOnMap.InteractionOnWorldMap(localController);
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
