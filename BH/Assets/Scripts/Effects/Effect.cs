using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Effect : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private float timeToHide = 1;

    private void Awake()
    {
        if (!animator) animator = GetComponent<Animator>();

        timeToHide = animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public void Setup(int sortingOrder)
    {
        spriteRenderer.sortingOrder = sortingOrder;
    }

    public void Show()
    {
        Debug.Log("Time to hide : " + timeToHide);
        Destroy(gameObject, timeToHide);
    }
}
