using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class DrawScreenHelperEditor
{
    static Vector3 bottomLeft = new Vector3(-6, 0, -13);  // Adjusted to start from ground
    static Vector3 rectangleSize = new Vector3(12, 0, 26);
    private static bool shouldDrawRectangle = true;

    static DrawScreenHelperEditor()
    {
        SceneView.duringSceneGui -= OnSceneGUI;  // Ensure no duplicates
        SceneView.duringSceneGui += OnSceneGUI;

    }
    private static void PlayModeChanged(PlayModeStateChange state)
    {
        if (SceneView.lastActiveSceneView != null)
        {
            SceneView.lastActiveSceneView.Repaint();
        }
    }

    static void OnSceneGUI(SceneView sceneView)
    {

        if (shouldDrawRectangle)
        {
            DrawRectangle();
        }
    }

    static void DrawRectangle()
    {
        Vector3 bottomRight = bottomLeft + new Vector3(rectangleSize.x, 0, 0);
        Vector3 topLeft = bottomLeft + new Vector3(0, 0, rectangleSize.z);
        Vector3 topRight = bottomLeft + new Vector3(rectangleSize.x, 0, rectangleSize.z);

        Handles.color = Color.red;

        // Draw the rectangle
        Handles.DrawLine(topLeft, topRight);
        Handles.DrawLine(topRight, bottomRight);
        Handles.DrawLine(bottomRight, bottomLeft);
        Handles.DrawLine(bottomLeft, topLeft);
    }

    [MenuItem("Tools/Toggle Rectangle Drawing")]
    public static void ToggleRectangleDrawing()
    {
        shouldDrawRectangle = !shouldDrawRectangle;
        SceneView.RepaintAll();  // Repaint all SceneViews to reflect changes

    }
}

