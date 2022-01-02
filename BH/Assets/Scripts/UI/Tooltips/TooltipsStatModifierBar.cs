using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TooltipsStatModifierBar : MonoBehaviour
{
    [SerializeField] private Text content = null;

    public void SetText(StringBuilder text, Color colorText)
    {
        content.text = text.ToString();
        content.color = colorText;
    }
}
