using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SnakeVariables))]
public class SnakeController : MonoBehaviour
{
    [SerializeField] private EDirection inputA = EDirection.Null;
    [SerializeField] private EDirection inputB = EDirection.Null;
    [SerializeField] private EDirection direction = EDirection.Null;
    public SnakePart snakeHead;
    private SnakeVariables snakeVariables;

    private void Start()
    {
        snakeVariables = GetComponent<SnakeVariables>();

        snakeVariables.StartingLenght(3);
    }

    void Update()
    {
        GetInput();
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
            yield return new WaitForEndOfFrame();
        } while (count < 1f);

        lockMovement = false;
    }

    private void Move()
    {
        if (lockMovement)
            return;

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

    void GetInput()
    {
        EDirection input = EDirection.Null;

        if (Input.GetKeyDown(KeyCode.W))
            input = EDirection.Up;
        else if (Input.GetKeyDown(KeyCode.S))
            input = EDirection.Down;
        else if (Input.GetKeyDown(KeyCode.D))
            input = EDirection.Right;
        else if (Input.GetKeyDown(KeyCode.A))
            input = EDirection.Left;

        if(input!=EDirection.Null) //Double input save
        {
            if (inputA == EDirection.Null)
                inputA = ValidateDirection(direction, input) ? input : EDirection.Null;
            else if (inputB == EDirection.Null)
                inputB = ValidateDirection(inputA, input) ? input : EDirection.Null;
        }
    }

    //Check if posible the input direction
    bool ValidateDirection(EDirection direction, EDirection input)
    {
        EDirection d = direction;
        EDirection i = input;
        if (d == EDirection.Right && i == EDirection.Left || d == EDirection.Left && i == EDirection.Right ||
            d == EDirection.Up && i == EDirection.Down || d == EDirection.Down && i == EDirection.Up)
            return false;
        else
            return true;
    }
}

public enum EDirection
{
    Null, Right, Left, Up, Down
}
