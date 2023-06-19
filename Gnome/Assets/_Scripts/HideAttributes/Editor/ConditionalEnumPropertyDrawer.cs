using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices.WindowsRuntime;

[CustomPropertyDrawer(typeof(ConditionalEnumHideAttribute))]
public class ConditionalEnumHidePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalEnumHideAttribute condHAtt = (ConditionalEnumHideAttribute)attribute;
        int enumValue = GetConditionalHideAttributeResult(condHAtt, property);
        int enum2Value = int.MaxValue;
        if (condHAtt.ConditionalSourceField2 != "")
            enum2Value = GetConditionalHideAttributeResult2(condHAtt, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = CheckEnums(condHAtt, enumValue, enum2Value);
        if (!condHAtt.HideInInspector || CheckEnums(condHAtt, enumValue, enum2Value))
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        GUI.enabled = wasEnabled;

        
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalEnumHideAttribute condHAtt = (ConditionalEnumHideAttribute)attribute;
        int enumValue = GetConditionalHideAttributeResult(condHAtt, property);
        int enum2Value = int.MaxValue;
        if (condHAtt.ConditionalSourceField2 != "")
            enum2Value = GetConditionalHideAttributeResult2(condHAtt, property);

        if (!condHAtt.HideInInspector || CheckEnums(condHAtt, enumValue, enum2Value))
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }

    private int GetConditionalHideAttributeResult(ConditionalEnumHideAttribute condHAtt, SerializedProperty property)
    {
        int enumValue = 0;

        SerializedProperty sourcePropertyValue = null;
        //Get the full relative property path of the sourcefield so we can have nested hiding
        if (!property.isArray)
        {
            string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
            string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField); //changes the path to the conditionalsource property path
            sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            //if the find failed->fall back to the old system
            if (sourcePropertyValue == null)
            {
                //original implementation (doens't work with nested serializedObjects)
                sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField);
            }
        }
        else
        {
            //original implementation (doens't work with nested serializedObjects)
            sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField);
        }


        if (sourcePropertyValue != null)
        {
            enumValue = sourcePropertyValue.enumValueIndex;
        }
        else
        {
            //Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
        }

        return enumValue;
    }

    private int GetConditionalHideAttributeResult2(ConditionalEnumHideAttribute condHAtt, SerializedProperty property)
    {
        int enumValue = 0;

        SerializedProperty sourcePropertyValue = null;
        //Get the full relative property path of the sourcefield so we can have nested hiding
        if (!property.isArray)
        {
            string propertyPath = property.propertyPath; //returns the property path of the property we want to apply the attribute to
            string conditionPath = propertyPath.Replace(property.name, condHAtt.ConditionalSourceField2); //changes the path to the conditionalsource property path
            sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            //if the find failed->fall back to the old system
            if (sourcePropertyValue == null)
            {
                //original implementation (doens't work with nested serializedObjects)
                sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField2);
            }
        }
        else
        {
            //original implementation (doens't work with nested serializedObjects)
            sourcePropertyValue = property.serializedObject.FindProperty(condHAtt.ConditionalSourceField2);
        }


        if (sourcePropertyValue != null)
        {
            enumValue = sourcePropertyValue.enumValueIndex;
        }
        else
        {
            //Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + condHAtt.ConditionalSourceField);
        }

        return enumValue;
    }

    bool CheckEnums(ConditionalEnumHideAttribute condHAtt, int enumValue, int enum2Value)
    {
        if (enum2Value == int.MaxValue)
        {
            if (CheckEnum(condHAtt, enumValue, false))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (CheckEnum(condHAtt, enumValue, false) && CheckEnum(condHAtt, enum2Value, true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }

    bool CheckEnum(ConditionalEnumHideAttribute condHAtt, int enumValue, bool second)
    {
        if (second)
        {
            if (condHAtt.Enum2Value1 == enumValue || condHAtt.Enum2Value2 == enumValue)
            {
                return !condHAtt.Inverse2;
            }
            else
            {
                return condHAtt.Inverse2;
            }
        }
        else
        {
            if (condHAtt.EnumValue1 == enumValue || condHAtt.EnumValue2 == enumValue)
            {
                return !condHAtt.Inverse;
            }
            else
            {
                return condHAtt.Inverse;
            }
        }
        
    }
}