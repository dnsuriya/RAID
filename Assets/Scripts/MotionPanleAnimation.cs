using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(RectTransform))]
public class MotionPanleAnimation : MonoBehaviour {

    public Vector3 beginPos;
    public Vector3 endPos;
    public float duration = 0.5f;
    public Ease ease;
    public GameObject panel;
}

#if UNITY_EDITOR
[CustomEditor(typeof(MotionPanleAnimation))]
class MotionPanleAnimationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MotionPanleAnimation panel = (MotionPanleAnimation)target;
        if (GUILayout.Button("Set Begin Pos"))
        {
            panel.beginPos = panel.panel.GetComponent<RectTransform>().anchoredPosition;
        }

        if (GUILayout.Button("Set End Pos"))
        {
            panel.endPos = panel.panel.GetComponent<RectTransform>().anchoredPosition;
        }

        if (GUILayout.Button("Move to Begin Pos"))
        {
            panel.panel.GetComponent<RectTransform>().anchoredPosition = panel.beginPos;
        }

        if (GUILayout.Button("Move to End Pos"))
        {
            panel.panel.GetComponent<RectTransform>().anchoredPosition = panel.endPos;
        }
    }
}
#endif
