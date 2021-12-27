using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionPanel : MonoBehaviour
{
    [SerializeField] private BasicButton btnDestroyConstruction = null;
    [SerializeField] private BasicButton btnCloseConstructionPanel = null;
    [SerializeField] private BasicButton btnBuildConstruction = null;
    [SerializeField] private Transform parentLabels = null;
    [SerializeField] private ConstructionLabel constructionLabelPrefab = null;
    [SerializeField] private int instantiateLabels = 10;

    private Construction construction;
    private List<ConstructionLabel> constructionLabels = new List<ConstructionLabel>();

    public void Setup(GameUiManager gameUiManager)
    {
        InstantiateLabels();
        btnCloseConstructionPanel.SetupListener(gameUiManager.ToggleContructionPanel);
        btnBuildConstruction.SetupListener(BuildContruction);
        btnDestroyConstruction.SetupListener(DestroyConstruction);
    }

    public void SetContruction(Construction construction)
    {
        this.construction = construction;
        OffLabels();
        SetAndEnableLabels();
    }

    public bool AddItemToConstruction(ConstructItemData data)
    {
        return construction.AddConstructItem(data);
    }

    private void BuildContruction()
    {
        if (construction.Build())
        {
            btnCloseConstructionPanel.OnClick();
        }
    }

    private void SetAndEnableLabels()
    {
        for (int i = 0; i < construction.ItemsNeeded.Count; i++)
        {
            if (constructionLabels.Count <= i)
            {
                ConstructionLabel label = Instantiate(constructionLabelPrefab, parentLabels);
                constructionLabels.Add(label);
            }
            constructionLabels[i].gameObject.SetActive(true);
            constructionLabels[i].SetupItem(construction.ItemsNeeded[i]);
        }
    }

    private void InstantiateLabels()
    {
        for (int i = 0; i < instantiateLabels; i++)
        {
            ConstructionLabel label = Instantiate(constructionLabelPrefab, parentLabels);
            constructionLabels.Add(label);
            label.Setup(this);
            label.gameObject.SetActive(false);
        }
    }

    private void OffLabels()
    {
        foreach (var label in constructionLabels)
        {
            if (label.gameObject.activeSelf)
            {
                label.gameObject.SetActive(false);
            }
        }
    }

    private void DestroyConstruction()
    {
        btnCloseConstructionPanel.OnClick();
        construction.Destroy();
    }
}
