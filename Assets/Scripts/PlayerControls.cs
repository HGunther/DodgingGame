using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed = 5;
    public event System.Action PlayerDestroyed;

    float screenHalfWidthInWorldUnits = 9.5f;
    float halfPlayerWidth;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;

        halfPlayerWidth = transform.localScale.x / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * speed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        if (transform.position.x - halfPlayerWidth < -screenHalfWidthInWorldUnits)
        {
            var newX = -screenHalfWidthInWorldUnits + halfPlayerWidth;
            transform.position = new Vector2(newX, transform.position.y);
        }

        if (transform.position.x + halfPlayerWidth > screenHalfWidthInWorldUnits)
        {
            var newX = screenHalfWidthInWorldUnits - halfPlayerWidth;
            transform.position = new Vector2(newX, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D triggerCollider)
    {
        if (triggerCollider.tag == "Hazard")
        {
            if (PlayerDestroyed != null) PlayerDestroyed();
            Destroy(gameObject);
        }
    }
}
