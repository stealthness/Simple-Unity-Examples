using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class MouseClickerTracker : MonoBehaviour
{
    private Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("Click");
            var mousePosition = Mouse.current.position.ReadValue();
            var worldPosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));
            CheckForTargets(worldPosition);
        }
    }
    
    
    private static void CheckForTargets(Vector3 worldPosition)
    {
        var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
        
        Debug.DrawLine(worldPosition, worldPosition + Vector3.forward * 10, Color.red, 1f);
        Debug.Log($"Raycast hit: {hit.collider?.name ?? "None"}");
        
        
        
        if (!hit || !hit.collider.CompareTag("Target")) return;

        hit.collider.gameObject.SetActive(false);
    }
}