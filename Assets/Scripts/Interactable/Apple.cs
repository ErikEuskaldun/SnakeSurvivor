using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("SnakeController");
        snake.GetComponent<SnakeVariables>().IncreasePoints(10);
        snake.GetComponent<SnakeVariables>().IncreaseLenght();
        this.transform.position = new Vector3(Random.Range(-9, 9), Random.Range(-9, 9));
    }
}
