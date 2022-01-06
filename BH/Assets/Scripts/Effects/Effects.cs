using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Effects/Effects")]
public class Effects : ScriptableObject
{
    [SerializeField] private List<Effect> effects = new List<Effect>();

    private Effect effect;
    private Transform transformTarget;
    private int orderLayer;

    public void Setup(int orderLayer, Transform transformTarget)
    {
        if (effects.Count <= 0) return;

        this.orderLayer = orderLayer;
        this.transformTarget = transformTarget;
    }

    public void Show()
    {
        int indexEffect = Random.Range(0, effects.Count);
        Debug.Log("Effect: " + indexEffect);
        effect = Instantiate(effects[indexEffect], transformTarget.position, Quaternion.identity);
        effect.Setup(orderLayer);
        effect.Show();
    }
}
