using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectEntity : MonoBehaviour
{
    [SerializeField] InputActionReference _selectEntity;

    public bool _isSelecting = false;
    public bool _isPlayerTeam = false;
    public bool _isEnemyTeam = false;

    public Entity _entity;

    // Start is called before the first frame update
    void Awake()
    {
        _selectEntity.action.started += StartSelect;
        _selectEntity.action.canceled += EndSelect;
    }

    private void EndSelect(InputAction.CallbackContext context)
    {
        _isSelecting = false;
    }

    private void StartSelect(InputAction.CallbackContext context)
    {
        _isSelecting = true;
    }

    void FixedUpdate()
    {
        if (_isSelecting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("PlayerTeam"))
                {
                    _isPlayerTeam = true;
                    _isEnemyTeam = false;
                    _entity = hit.collider.GetComponent<Entity>();
                }
                if (hit.collider.CompareTag("EnemyTeam"))
                {
                    _isEnemyTeam = true;
                    _isPlayerTeam = false;
                    _entity = hit.collider.GetComponent<Entity>();
                }
            }
        }
    }
}
