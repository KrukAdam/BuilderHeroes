using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character
{
	public PlayerActionController PlayerActionController { get => playerActionController; }
	public PlayerSkillsController PlayerSkillsController { get => playerSkillsController; }

	[SerializeField] private PlayerActionController playerActionController = null;
	[SerializeField] private PlayerSkillsController playerSkillsController = null;
	[SerializeField] private EquipmentManager equipmentManager = null;

	public void SetupCharacter(LevelController levelController)
	{

		SetupLayerMask(true);

		if(!Stats) stats = gameObject.GetComponent<Stats>();
		this.Stats.Init();

		playerActionController.Init(equipmentManager, this);
		moveController.Init(this);
		playerSkillsController.Setup(levelController);

		Stats.OnStatsChange += levelController.GameUiManager.CharacterPanels.StatsPanel.UpdateStatValues;
	}
}
