using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class tracks mouse clicks and checks for targets in a 2D space. When the player clicks, it performs a raycast
/// from the mouse position to detect if a target is hit. If a target is hit, it updates the score and deactivates the target.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class MouseClickerTracker : MonoBehaviour
{
    /// <summary>
    /// Reference to the main camera in the scene. This is used to convert mouse screen coordinates to world coordinates.
    /// </summary>
    private Camera _camera;
    /// <summary>
    /// Reference to the ScoreManager instance that manages the player's score. This is set in the Awake method by
    /// finding any object of type ScoreManager in the scene.
    /// </summary>
    [SerializeField] private ScoreManager scoreManager;
    /// <summary>
    /// The number of points awarded for hitting a target. This value can be set in the Unity Inspector.
    /// </summary>
    [SerializeField] private int scorePerTarget = 10;

    private void Awake()
    {
        _camera = Camera.main;
        if (!scoreManager) scoreManager = FindAnyObjectByType<ScoreManager>(); // Ensure that the ScoreManager is assigned, either through the Inspector or by finding it in the scene.
    }
    
    /// <summary>
    /// This method is called when the player clicks the mouse. It checks if the click is pressed, logs the click event,
    /// converts the mouse position to world coordinates, and calls CheckForTargets to see if a target was hit.
    /// </summary>
    /// <param name="value">a value representing the input, which is the mouse click</param>
    private void OnClick(InputValue value)
    {
        if (!value.isPressed) return;
  
        var mousePosition = Mouse.current.position.ReadValue();
        var worldPosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
        CheckForTargets(worldPosition);
    }
    
    
    private void CheckForTargets(Vector3 worldPosition)
    {
        var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
   
        if (!hit || !hit.collider.CompareTag("Target")) return; // nothing found

        scoreManager.OnScoreChanged?.Invoke(scorePerTarget);
        hit.collider.gameObject.SetActive(false);
    }
}