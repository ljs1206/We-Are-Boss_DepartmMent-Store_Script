using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdinStyles
{
    public static OdinMenuStyle menuButtonStyle =
    new OdinMenuStyle()
    {
        Height = 30,
        Offset = 15.00f,
        IndentAmount = 15.00f,
        IconSize = 35.00f,
        IconOffset = -10.00f,
        NotSelectedIconAlpha = 0.85f,
        IconPadding = -5.00f,
        TriangleSize = 17.00f,
        TrianglePadding = 8.00f,
        AlignTriangleLeft = false,
        Borders = true,
        BorderPadding = 13.00f,
        BorderAlpha = 0.50f,
        SelectedColorDarkSkin = new Color(0.243f, 0.373f, 0.588f, 1.000f),
        SelectedColorLightSkin = new Color(0.243f, 0.490f, 0.900f, 1.000f)
    };
}
