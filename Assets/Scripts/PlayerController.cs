using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    private bool isMoving;
    public float speed;
    private Vector2 input;

    public LayerMask solidObjectsLayer, pokemonLayer;

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if(input.x != 0)
            {
                input.y = 0;
            }


            if(input != Vector2.zero )
            {
                var targetPosition = transform.position;
                targetPosition.x += input.x;
                targetPosition.y += input.y;

                if (IsAvailable(targetPosition))
                {
                    StartCoroutine(MoveTowards(targetPosition));
                }
            }

        }
    }

    IEnumerator MoveTowards(Vector3 destination)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, destination) > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = destination;
        isMoving = false;

        CheckForPokemon();

        
    }

    private bool IsAvailable(Vector3 target)
    {
        if(Physics2D.OverlapCircle(target, 0.2f, solidObjectsLayer))
        {
            return false;
        }
        return true;
    }

    private void CheckForPokemon()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, pokemonLayer) != null)
        {
            if (Random.Range(0, 100) < 10)
            {
                print("Empezamos batalla");
            }
        }

    }
}
