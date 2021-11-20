using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character
{

	//To test, use this on spawn init
    private void Start()
    {
		Init(null);
    }

    //OnSpawn
    public void Init(GameUiManager gameUiManager)
	{
		this.gameUiManager = gameUiManager; //?

		SetupLayerMask(false);

		if (Stats == null)
		{
			stats = gameObject.GetComponent<Stats>();
		}
		this.Stats.Init();
	}
}
