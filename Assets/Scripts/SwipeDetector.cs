using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;
    private bool _isSwiping = false;

    private const float min_distace_x = 100f;
    private const float min_distace_y = 50f;  

    private float lastTouchX;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPos = touch.position;
                    lastTouchX = touch.position.x;
                    _isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    float currentTouchX = touch.position.x;
                    if (_isSwiping && Mathf.Sign(currentTouchX - lastTouchX) != Mathf.Sign(currentTouchX - _touchStartPos.x))
                    {
                        _touchStartPos = new Vector2(currentTouchX, _touchStartPos.y);
                    }
                    lastTouchX = currentTouchX;
                    break;

                case TouchPhase.Ended:
                    _touchEndPos = touch.position;
                    DetectSwipe();
                    _isSwiping = false;
                    break;
            }
        }
    }

    private void DetectSwipe()
    {
        float swipeDistanceX = _touchEndPos.x - _touchStartPos.x;
        float swipeDistanceY = _touchEndPos.y - _touchStartPos.y;

        if (swipeDistanceX > min_distace_x && Mathf.Abs(swipeDistanceY) <= min_distace_y)
        {
            Debug.Log("Свайп вправо распознан!");
        }
        else
        {
            Debug.Log("Свайп не распознан.");
        }
    }
}
