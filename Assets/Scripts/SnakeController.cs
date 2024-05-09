using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public EDirection inputA = EDirection.Null;
    public EDirection inputB = EDirection.Null;
    public EDirection direction = EDirection.Null;
    public Transform nextPosition;
    public float speed = 2f;
    public bool testClasicMovement = false;
    public SnakePart snakeHead;
    private SnakeVariables snakeVariables;

    private void Start()
    {
        snakeVariables = GetComponent<SnakeVariables>();

        for (int i = 0; i < 3; i++)
        {
            snakeVariables.IncreaseLenght();
        }
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
        TEST_IncreaseDecreaseSnakeLenght();
    }

    public void TEST_IncreaseDecreaseSnakeLenght()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            snakeVariables.IncreaseLenght();
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
            snakeVariables.DecreaseLenght();
    }

    void Movement()
    {
        Move(inputA);
    }

    bool lockMovement = false;
    public IEnumerator TileMovement(Vector3 targetPosition)
    {
        lockMovement = true;

        Vector3 startingPosition = snakeHead.transform.position;
        float count = 0f;
        snakeHead.UpdatePosition(targetPosition);
        do
        {
            //if(!testClasicMovement) snakeHead.transform.position = Vector3.Lerp(startingPosition, targetPosition, count);
            count += Time.deltaTime * speed;
            yield return new WaitForEndOfFrame();
        } while (count < 1f);

        lockMovement = false;
    }

    private void Move(EDirection direction)
    {
        if (lockMovement)
            return;
        if (inputA == EDirection.Null)
            direction = this.direction;
        Vector3 targetPosition = default;
        switch (direction)
        {
            case EDirection.Up:
                targetPosition = snakeHead.transform.position + Vector3.up;
                break;
            case EDirection.Down:
                targetPosition = snakeHead.transform.position + Vector3.down;
                break;
            case EDirection.Right:
                targetPosition = snakeHead.transform.position + Vector3.right;
                break;
            case EDirection.Left:
                targetPosition = snakeHead.transform.position + Vector3.left;
                break;
        }
        this.direction = direction;
        inputA = inputB;
        inputB = EDirection.Null;
        StartCoroutine(TileMovement(Vector3Int.RoundToInt(targetPosition)));
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

        if(input!=EDirection.Null)
        {
            if (inputA == EDirection.Null)
                inputA = ValidateDirection(direction, input) ? input : EDirection.Null;
            else if (inputB == EDirection.Null)
                inputB = ValidateDirection(inputA, input) ? input : EDirection.Null;
        }
    }

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
