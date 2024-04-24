using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LaserGenerator : MonoBehaviour
{
    private List<LineRenderer> lineSegments = new();
    private bool linesShown = true;

    [SerializeField]
    [Tooltip("The Scene Manager that will provide walls.")]
    private OVRSceneManager sceneManager;

    [SerializeField]
    [Tooltip("The color of the laser.")]
    private Color color = Color.white;

    [SerializeField]
    [Tooltip("The body of the laser.")]
    private MeshRenderer body;

    private void RegenerateLines()
    {
        // Delete existing segments
        for (int i = lineSegments.Count -1; i >=0 ; i--) 
        {
            GameObject.Destroy(lineSegments[i]);
            lineSegments.RemoveAt(i);
        }

        // If there is no room loaded, nothing to do
        if (sceneManager.RoomLayout == null)
        {
            return;
        }

        // Get all walls
        var walls = sceneManager.RoomLayout.Walls;

        // Loop through all walls
        foreach (var wall in walls)
        {
            // Create line segment
            LineRenderer segment = wall.AddComponent<LineRenderer>();

            // Set left and right points
            segment.SetPosition(0, new Vector3(-wall.Width / 2, wall.Height/2, 0));
            segment.SetPosition(0, new Vector3(+wall.Width / 2, wall.Height / 2, 0));
            
            // Set the color
            segment.startColor = color;
            segment.endColor = color;

            // Remember it
            lineSegments.Add(segment);
        }

    }

    private void UpdateSegmentPositions()
    {
        foreach (var segment in lineSegments)
        {
            // Get left and right positions
            Vector3 left = segment.GetPosition(0);
            Vector3 right = segment.GetPosition(1);

            // Update height of line
            left = new Vector3(left.x, this.gameObject.transform.position.y, left.z);
            right = new Vector3(right.x, this.gameObject.transform.position.y, right.z);

            // Update the segment
            segment.SetPosition(0, left);
            segment.SetPosition(1, right);
        }
    }

    private void UpdateLineVisibility()
    {
        foreach (var segment in lineSegments)
        {
            segment.enabled = linesShown;
        }
    }

    public void ShowLines()
    {
        linesShown = true;
        UpdateLineVisibility();
    }

    public void HideLines()
    {
        linesShown = false;
        UpdateLineVisibility();
    }

    public void ToggleLines()
    {
        linesShown = !linesShown;
        UpdateLineVisibility();
    }
    private void Awake()
    {
        // Match laser color
        body.material.color = color;

        // Generate Lines
        RegenerateLines();
    }

    void Update()
    {
        if (linesShown)
        {
            UpdateSegmentPositions();
        }
    }

    private void Start()
    {
        // If no color specified, make it random
        if (color ==  Color.white)
        {
            color = Random.ColorHSV();
        }
    }

    public void OnClick()
    {
        ToggleLines();
    }
    
}