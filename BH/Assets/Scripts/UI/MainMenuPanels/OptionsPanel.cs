using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private DropdownLocale localeDropdown = null;

    public void Setup()
    {
        localeDropdown.Setup();

        gameObject.SetActive(false);
    }
}
