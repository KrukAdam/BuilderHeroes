using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : Character
{
	public PlayerActionController PlayerActionController { get => playerActionController; }
	public PlayerSkillsController PlayerSkillsController { get => playerSkillsController; }

	[SerializeField] private PlayerActionController playerActionController = null;
	[SerializeField] private PlayerSkillsController playerSkillsController = null;

	public void SetupCharacter(LocalController localController)
	{

		SetupLayerMask(true);

		if(!Stats) stats = gameObject.GetComponent<Stats>();
		this.Stats.Init();

		playerActionController.Setup(localController, this);
		moveController.Init(this, stats);
		playerSkillsController.Setup(localController);

		Stats.OnStatsChange += localController.GameUiManager.CharacterPanels.StatsPanel.UpdateStatValues;
	}

}
