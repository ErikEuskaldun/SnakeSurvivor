using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Interactable")
        {
            IInteractable interactable = collision.GetComponent(typeof(IInteractable)) as IInteractable;
            interactable.OnInteract();
        }
    }
}
