using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePartCollider : MonoBehaviour, IInteractable
{

    public void OnInteract()
    {
        GameObject snake = GameObject.FindGameObjectWithTag("SnakeController");
        snake.GetComponent<SnakeVariables>().GameOver();
    }
}
