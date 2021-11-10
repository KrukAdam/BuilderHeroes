using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character
{
	public PlayerActionController PlayerActionController { get => playerActionController; }
	public PlayerSkillsController PlayerSkillsController { get => playerSkillsController; }

	[SerializeField] private PlayerActionController playerActionController = null;
	[SerializeField] private PlayerSkillsController playerSkillsController = null;
	[SerializeField] private EquipmentManager equipmentManager = null;

	public void Init(GameUiManager gameUiManager)
	{
		this.gameUiManager = gameUiManager;

		if(!Stats) stats = gameObject.GetComponent<Stats>();
		this.Stats.Init();

		playerActionController.Init(equipmentManager, this);
		moveController.Init(this);
		playerSkillsController.Init(this);
		equipmentManager.Init(gameUiManager, this);

		gameUiManager.SetStatsOnStatsPanel(Stats);

		Stats.OnDamage += UpdateStatValues;

	}

	public void UpdateStatValues()
	{
		gameUiManager.UpdateStatValues();
	}

}
