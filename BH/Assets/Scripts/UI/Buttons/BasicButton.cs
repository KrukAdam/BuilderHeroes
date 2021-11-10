using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BasicButton : MonoBehaviour
{
    public Button Button => button;

    [SerializeField] protected Button button = null;

    private UnityAction action = delegate { };

    protected void Awake()
    {
        if (!button) gameObject.GetComponent<Button>();

        button.onClick.AddListener(() => OnClick());
    }

    public bool ChangeSelectableBlockState(bool state)
    {
        button.interactable = state;
        return state;
    }

    public void SetupListener(UnityAction call)
    {
        action = call;
    }

    private void OnClick()
    {
        action.Invoke();
    }
}
