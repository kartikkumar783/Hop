using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float distance, time, xPos;

    private float speed;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    public bool hasGameStarted;

    private void Start()
    {
        hasGameStarted = false;
        speed = distance / time;
    }

    private void FixedUpdate()
    {
        if (!hasGameStarted) return;

        Vector3 temp = new Vector3(0, 0, speed) * Time.fixedDeltaTime; // Forward movement

        // Check for touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    touchEndPos = touch.position;

                    // Calculate horizontal movement based on touch position
                    float horizontalDelta = (touchEndPos.x - touchStartPos.x) / Screen.width;
                    temp.x = horizontalDelta * speed * 2f * Time.fixedDeltaTime;
                    break;

                case TouchPhase.Ended:
                    touchStartPos = Vector2.zero;
                    touchEndPos = Vector2.zero;
                    break;
            }
        }

        // Apply movement
        transform.Translate(temp);

        // Clamp position
        temp = transform.position;
        if (temp.x > xPos)
            temp.x = xPos;
        if (temp.x < -xPos)
            temp.x = -xPos;
        transform.position = temp;
    }

}
