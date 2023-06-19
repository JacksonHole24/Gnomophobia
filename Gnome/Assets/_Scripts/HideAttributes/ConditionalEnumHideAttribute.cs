using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
    AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
public class ConditionalEnumHideAttribute : PropertyAttribute
{
    //The name of the bool field that will be in control
    public string ConditionalSourceField = "";
    public string ConditionalSourceField2 = "";

    public int EnumValue1 = 0;
    public int EnumValue2 = 0;

    public int Enum2Value1 = 0;
    public int Enum2Value2 = 0;

    public bool HideInInspector = false;
    public bool Inverse = false;
    public bool Inverse2 = false;

    public ConditionalEnumHideAttribute(string conditionalSourceField, int enumValue1)
    {
        ConditionalSourceField = conditionalSourceField;
        EnumValue1 = enumValue1;
        EnumValue2 = enumValue1;
    }

    public ConditionalEnumHideAttribute(string conditionalSourceField, int enumValue1, int enumValue2)
    {
        ConditionalSourceField = conditionalSourceField;
        EnumValue1 = enumValue1;
        EnumValue2 = enumValue2;
    }

}