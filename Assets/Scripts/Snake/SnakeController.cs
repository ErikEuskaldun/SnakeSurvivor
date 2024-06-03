using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SnakeVariables))]
public class SnakeController : MonoBehaviour
{
    [SerializeField] private EDirection inputA = EDirection.Null;
    [SerializeField] private EDirection inputB = EDirection.Null;
    [SerializeField] private EDirection direction = EDirection.Null;
    public SnakePart snakeHead;
    private SnakeVariables snakeVariables;
    public bool inputLocked = true;
    [SerializeField] PlayerInput playerInput;

    public bool IsGameStarted { get => direction == EDirection.Null ? false : true; }

    private void Start()
    {
        snakeVariables = GetComponent<SnakeVariables>();
        Time.timeScale = 0;

        snakeVariables.StartingLenght(3);
    }

    void Update()
    {
        Move();
    }

    bool lockMovement = false;
    public IEnumerator TileMovement(Vector3 targetPosition)
    {
        lockMovement = true;

        Vector3 startingPosition = snakeHead.transform.position;
        float count = 0f;
        snakeHead.UpdatePosition(targetPosition);
        do //Timer for next action
        {
            count += Time.deltaTime * snakeVariables.speed;
            //snakeHead.transform.position = Vector3.Lerp(startingPosition, targetPosition, count);
            yield return new WaitForEndOfFrame();
        } while (count < 1f);

        lockMovement = false;
    }

    private void Move()
    {
        if (lockMovement)
            return;

        if (this.direction == EDirection.Null)
        {
            GameStart();
            return;
        }
            
        EDirection direction = inputA;
        if (inputA == EDirection.Null) //keep direction if input no selected
            direction = this.direction;

        Vector3 targetPosition = default;
        switch (direction)
        {
            case EDirection.Up:
                targetPosition = snakeHead.transform.position + Vector3.up * SnakeUtils.TILE_SIZE;
                break;
            case EDirection.Down:
                targetPosition = snakeHead.transform.position + Vector3.down * SnakeUtils.TILE_SIZE;
                break;
            case EDirection.Right:
                targetPosition = snakeHead.transform.position + Vector3.right * SnakeUtils.TILE_SIZE;
                break;
            case EDirection.Left:
                targetPosition = snakeHead.transform.position + Vector3.left * SnakeUtils.TILE_SIZE;
                break;
        }
        this.direction = direction;

        //Get the secoundary input (fast input)
        inputA = inputB;
        inputB = EDirection.Null;
        if(direction!=EDirection.Null)StartCoroutine(TileMovement(targetPosition)); //Recall movement
    }

    private void GameStart()
    {
        if (inputA != EDirection.Null && inputA != EDirection.Right)
        {
            Time.timeScale = 1;
            this.direction = inputA;
        }
        ResetDirection();
    }

    public void ResetDirection()
    {
        inputA = EDirection.Null;
        inputB = EDirection.Null;
    }

    public void GetInput(InputAction.CallbackContext callbackContext)
    {
        if (inputLocked || !callbackContext.performed)
            return;

        Vector2 input = callbackContext.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Gamepad" && input.magnitude>0.5f)
        {
            SnakeUtils.ConvertToFixedDirection(ref input);
        }
            
        EDirection inputDirection = EDirection.Null;

        if (input == Vector2.up)
            inputDirection = EDirection.Up;
        else if (input == Vector2.down)
            inputDirection = EDirection.Down;
        else if (input == Vector2.right)
            inputDirection = EDirection.Right;
        else if (input == Vector2.left)
            inputDirection = EDirection.Left;

        if(inputDirection != EDirection.Null) //Double input save
        {
            if (inputA == EDirection.Null)
                inputA = ValidateDirection(direction, inputDirection) ? inputDirection : EDirection.Null;
            else if (inputB == EDirection.Null)
                inputB = ValidateDirection(inputA, inputDirection) ? inputDirection : EDirection.Null;
        }
    }

    //Check if posible the input direction
    bool ValidateDirection(EDirection direction, EDirection input)
    {
        EDirection d = direction;
        EDirection i = input;
        if (d == i || d == EDirection.Right && i == EDirection.Left || d == EDirection.Left && i == EDirection.Right ||
            d == EDirection.Up && i == EDirection.Down || d == EDirection.Down && i == EDirection.Up)
            return false;
        return true;
    }
}

public enum EDirection
{
    Null, Right, Left, Up, Down
}
