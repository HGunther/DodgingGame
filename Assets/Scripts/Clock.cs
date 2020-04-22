using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<PlayerControls>().PlayerDestroyed += disableClock;
    }

    void disableClock()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        var textBox = gameObject.GetComponent<Text>();
        textBox.text = Mathf.RoundToInt(Time.timeSinceLevelLoad).ToString();
    }
}
