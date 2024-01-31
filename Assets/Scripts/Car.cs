using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 20f;

    public Rigidbody2D rb;

    private bool canMove = false;

    public GameObject vfx;

    private Vector3 startPos;

    public Vector2 direction;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void OnMouseDown()
    {
        canMove = true;
    }

    private void Update()
    {
        if (!canMove) return;

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject vfxExplosin = Instantiate(vfx, transform.position, Quaternion.identity);
        Destroy(vfxExplosin, 0.75f);

        if (collision.tag == "Garage")
        {
            canMove = false;
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
            transform.position = startPos;
            gameObject.SetActive(false);
            GameManager.Instance.CheckLevelUp();
        }
        else if (collision.tag == "Car")
        {
            gameObject.SetActive(false);

            canMove = false;

            transform.position = startPos;

            gameObject.SetActive(true);
        }
        else
        {
            transform.position = startPos;
            canMove = false;
            gameObject.SetActive(false);
        }
    }
}