using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputActionReference _clickAction;
    [SerializeField] InputActionReference _mouseMoveAction;

    private Action _onMouseUpActions;
    private Action _onMouseMoveActions;

    void Awake()
    {
        _clickAction.action.canceled += OnMouseUpCallback;
        _mouseMoveAction.action.performed += OnMouseMoveCallback;
    }

    #region MouseUpLogic
    public void OnMouseUpCallback(InputAction.CallbackContext context)
    {
        if (_onMouseUpActions != null)
        {
            _onMouseUpActions.Invoke();
        }
    }

    public void AddOnMouseUpAction(Action onClickAction)
    {
        _onMouseUpActions += onClickAction;
    }

    public void RemoveOnMouseUpAction(Action onClickAction)
    {
        _onMouseUpActions -= onClickAction;
    }
    #endregion MouseUpLogic


    #region MouseMoveLogic
    public void OnMouseMoveCallback(InputAction.CallbackContext context)
    {
        if (_onMouseMoveActions != null)
        {
            _onMouseMoveActions.Invoke();
        }
    }

    public void AddOnMouseMoveAction(Action onMouseMoveAction)
    {
        _onMouseMoveActions += onMouseMoveAction;
    }

    public void RemoveOnMouseMoveAction(Action onMouseMoveAction)
    {
        _onMouseMoveActions -= onMouseMoveAction;
    }
    #endregion MouseMoveLogic
}
