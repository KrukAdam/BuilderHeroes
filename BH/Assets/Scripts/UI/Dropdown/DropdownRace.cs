using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownRace : MonoBehaviour
{
    public event Action OnValueChange = delegate { };
    
    [SerializeField] private Dropdown dropdown;

    public void Setup()
    {
        var options = new List<Dropdown.OptionData>();
        int selected = 0;
        for (int i = 0; i < GameManager.Instance.RaceDatabase.Race.Length; ++i)
        {
            var race = GameManager.Instance.RaceDatabase.Race[i];

            options.Add(new Dropdown.OptionData(race.RaceName.GetLocalizedString()));
        }

        dropdown.options = options;
        dropdown.value = selected;

        dropdown.onValueChanged.AddListener(RaceSelected);

        RaceSelected(selected);
    }

    private void RaceSelected(int index)
    {
        GameManager.Instance.CharacterCreator.SetupRace(GameManager.Instance.RaceDatabase.Race[index]);
        OnValueChange();
    }
}
