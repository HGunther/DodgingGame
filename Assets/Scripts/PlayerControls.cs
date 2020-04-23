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
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        float touchInput = getTouchInput();

        float velocity;
        if (Mathf.Abs(touchInput) > Mathf.Abs(keyboardInput))
        {
            var maxMovement = speed * Time.deltaTime;
            if (maxMovement > Mathf.Abs(touchInput))
            {
                velocity = touchInput;
            }
            else
            {
                velocity = speed * Time.deltaTime * (touchInput / Mathf.Abs(touchInput));
            }
        }
        else
        {
            velocity = keyboardInput * speed * Time.deltaTime;
        }

        transform.Translate(Vector2.right * velocity);

        KeepInsideBounds();
    }

    float getTouchInput()
    {
        //var rawMouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        //return rawMouseX - transform.position.x;
        if (Input.touches.Length > 0)
        {
            var difference = Camera.main.ScreenToWorldPoint(Input.touches[0].position).x - transform.position.x;
            return difference;
        }
        return 0;
    }

    void KeepInsideBounds()
    {
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
