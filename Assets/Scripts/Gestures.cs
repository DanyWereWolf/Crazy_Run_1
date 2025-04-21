using UnityEngine;

public class Gestures : MonoBehaviour
{
    private Vector2 _touch1StartPos;
    private Vector2 _touch2StartPos;
    private float _initialDistance;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 touch1CurrentPos = touch1.position;
            Vector2 touch2CurrentPos = touch2.position;

            float currentDistance = Vector2.Distance(touch1CurrentPos, touch2CurrentPos);

            if (touch1.phase == TouchPhase.Began && touch2.phase == TouchPhase.Began)
            {
                _touch1StartPos = touch1.position;
                _touch2StartPos = touch2.position;
                _initialDistance = currentDistance;
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                float distanceIncreaseThreshold = _initialDistance * 0.1f;
                if (currentDistance > _initialDistance + distanceIncreaseThreshold)
                {
                    Vector2 touch1Delta = touch1CurrentPos - _touch1StartPos;
                    Vector2 touch2Delta = touch2CurrentPos - _touch2StartPos;

                    if (IsOppositeDirection(touch1Delta, touch2Delta))
                    {
                        Debug.Log("Жест увеличение");
                    }
                }
                _initialDistance = currentDistance;
            }
        }
    }

    private bool IsOppositeDirection(Vector2 delta1, Vector2 delta2)
    {
        return (delta1.x > 0 && delta2.x < 0) || (delta1.x < 0 && delta2.x > 0) ||
               (delta1.y > 0 && delta2.y < 0) || (delta1.y < 0 && delta2.y > 0);
    }
}
