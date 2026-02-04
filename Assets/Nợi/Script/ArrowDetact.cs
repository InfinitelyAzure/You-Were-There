using UnityEngine;

public class ArrowDetact : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) Destroy(gameObject);
    }
}
