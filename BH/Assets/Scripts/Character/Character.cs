using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Character : MonoBehaviour
{
	public Stats Stats { get => stats; }
	public Rigidbody2D CharacterRigidbody { get => characterRigidbody; }
	public MoveController MoveController { get => moveController; }
	public GameUiManager GameUiManager { get => gameUiManager; }

	[SerializeField] protected Stats stats;
	[SerializeField] protected Rigidbody2D characterRigidbody;
	[SerializeField] protected MoveController moveController;

    protected GameUiManager gameUiManager;

}
