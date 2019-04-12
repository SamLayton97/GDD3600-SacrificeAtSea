using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collidable side of the cavern wall
/// </summary>
public class CavernWall : MonoBehaviour
{
    // movement support
    [SerializeField] float movementSpeed = 0.01f;
    Rigidbody2D rigidbody2D;
    Vector3 displacementVector = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        // retrieve reference to object's rigidbody component
        rigidbody2D = GetComponent<Rigidbody2D>();

        // calculate fixed displacement vector
        displacementVector.x = movementSpeed;
    }

    // Called once per frame
    void Update()
    {

    }

    // Called at fixed number of times per second
    void FixedUpdate()
    {
        rigidbody2D.MovePosition(transform.position - displacementVector);
    }

    // Called when object collides with another
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        Destroy(gameObject);
    }
}
