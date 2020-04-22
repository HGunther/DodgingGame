using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public Vector2 speedMinMax;
    float speed;

    Vector2 screenHalfSizeWorldUnits;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);

        speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.getDifficultyPercent());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime, Space.Self);

        if (transform.position.y + transform.localScale.y < -screenHalfSizeWorldUnits.y)
        {
            Destroy(gameObject);
        }
    }
}
