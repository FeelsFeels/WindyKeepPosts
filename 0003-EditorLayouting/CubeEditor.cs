using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CrazyCube))]
public class CubeEditor : Editor
{
    private GUIContent primaryColorFieldLabel = new GUIContent("Primary");
    private GUIContent secondaryColorFieldLabel = new GUIContent("Secondary");
    private GUIContent colorMixLabel = new GUIContent("Color Mix");

    private SerializedProperty primaryColorProperty;
    private SerializedProperty secondaryColorProperty;

    private void OnEnable()
    {
        primaryColorProperty = serializedObject.FindProperty("primaryColor");
        secondaryColorProperty = serializedObject.FindProperty("secondaryColor");
    }

    public override void OnInspectorGUI()
    {
        AddColorControl(primaryColorProperty, primaryColorFieldLabel);
        AddColorControl(secondaryColorProperty, secondaryColorFieldLabel);

        GUILayout.Space(20);

        Rect labelRect = GUILayoutUtility.GetRect(10, 15);
        EditorGUI.DropShadowLabel(labelRect, colorMixLabel);

        GUILayout.Space(2);

        Rect colorMixSwatchRect = GUILayoutUtility.GetRect(10, 10);
        colorMixSwatchRect.width *= 0.67f;
        colorMixSwatchRect.x += (colorMixSwatchRect.width / 0.67f) * (0.33f * 0.5f);
        EditorGUIUtility.DrawColorSwatch(colorMixSwatchRect, (primaryColorProperty.colorValue + secondaryColorProperty.colorValue) * 0.5f);

        GUILayout.Space(2);
    }

    private void AddColorControl(SerializedProperty colorProperty, GUIContent guiContent)
    {
        EditorGUI.BeginProperty(EditorGUILayout.GetControlRect(), primaryColorFieldLabel, primaryColorProperty);
        EditorGUI.BeginChangeCheck();

        Color color = EditorGUILayout.ColorField(guiContent, colorProperty.colorValue);
        if (EditorGUI.EndChangeCheck())
        {
            colorProperty.colorValue = color;
            serializedObject.ApplyModifiedProperties();
        }

        EditorGUI.EndProperty();

        GUILayout.Space(2);

        Color invertedColor = new Color(1 - color.r, 1 - color.g, 1 - color.b, 1 - color.a);
        Color colorPlusWhite = new Color((1 + color.r) * 0.5f, (1 + color.g) * 0.5f, (1 + color.b) * 0.5f, 1f);
        Color colorPlusBlack = new Color(color.r * 0.5f, color.g * 0.5f, color.b * 0.5f, 1f);

        Rect rect = GUILayoutUtility.GetRect(10, 10);

        Rect rect1 = new Rect(rect.x, rect.y, (rect.width / 3f), rect.height);
        Rect rect2 = new Rect(rect.x + rect.width / 3f + rect.width / 30f, rect.y, (rect.width / 3f) * 0.8f, rect.height);
        Rect rect3 = new Rect(rect.x + 2 * rect.width / 3f, rect.y, (rect.width / 3f), rect.height);

        EditorGUIUtility.DrawColorSwatch(rect1, invertedColor);
        EditorGUIUtility.DrawColorSwatch(rect2, colorPlusWhite);
        EditorGUIUtility.DrawColorSwatch(rect3, colorPlusBlack);
    }
}