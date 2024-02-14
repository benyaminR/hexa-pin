
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public Texture2D defaultCursor; // Assign your default sprite here in the inspector
    public Texture2D clickCursor;   // Assign your clicking sprite here in the inspector


    [SerializeField] private RawImage cursorImage;


    private void Start()
    {
        SetDefaultCursor();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is for left mouse button
        {
            SetClickCursor();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SetDefaultCursor();
        }

        // update cursor position
        cursorImage.rectTransform.position = Input.mousePosition;

        // stop editor when space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //UnityEditor.EditorApplication.isPaused = true;
        }
    }

    private void SetDefaultCursor()
    {
        cursorImage.texture = defaultCursor;
    }

    private void SetClickCursor()
    {
        cursorImage.texture = clickCursor;
    }
}
