using UnityEngine;

namespace _Game.Scripts.Helpers
{
    public class TouchHelper : MonoBehaviour
    {
        public enum SwipeDirection
        {
            None,
            Left,
            Right,
            Up,
            Down
        }

        public enum TouchPosition
        {
            None,
            LeftHalf,
            RightHalf,
        }

        private Vector2 _fingerDown;
        private Vector2 _fingerUp;
        private static SwipeDirection _swipeDirection;
        private static TouchPosition _touchPosition;
        
        public static bool DetectSwipeOnlyAfterRelease = false;
        public float SWIPE_THRESHOLD = 20f;

        public static SwipeDirection SwipeMoveDirection => _swipeDirection;
        public static TouchPosition TouchFingerPosition => _touchPosition;
        public static bool TouchExist = false;

        private Vector2 _startingPosition;
        private bool _isFingerDown;

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.LeftArrow))
                _swipeDirection = SwipeDirection.Left;
            else if (Input.GetKey(KeyCode.RightArrow))
                _swipeDirection = SwipeDirection.Right;
            else if (Input.GetKey(KeyCode.UpArrow))
                _swipeDirection = SwipeDirection.Up;
            else if (Input.GetKey(KeyCode.DownArrow))
                _swipeDirection = SwipeDirection.Down;
            else
                _swipeDirection = SwipeDirection.None;
#else

            TouchExist = Input.touches.Length > 0;
            
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _fingerUp = touch.position;
                    _fingerDown = touch.position;
                }
        
                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!DetectSwipeOnlyAfterRelease)
                    {
                        _fingerDown = touch.position;
                        CheckSwipe();
                    }
        
                    if (touch.position.x < Screen.width / 2f)
                    {
                        _touchPosition = TouchPosition.LeftHalf;
                    }
                    else if (touch.position.x > Screen.width / 2f)
                    {
                        _touchPosition = TouchPosition.RightHalf;
                    }
                }
        
                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                    _swipeDirection = SwipeDirection.None;
                    _fingerDown = touch.position;
                    CheckSwipe();
                }
        
                if (touch.phase != TouchPhase.Moved)
                {
                    _touchPosition = TouchPosition.None;
                    _swipeDirection = SwipeDirection.None;
                }
            }
#endif
        }

        private void CheckSwipe()
        {
            //Check if Vertical swipe
            if (VerticalMove() > SWIPE_THRESHOLD && VerticalMove() > HorizontalValMove())
            {
                if (_fingerDown.y - _fingerUp.y > 0)//up swipe
                {
                    OnSwipeUp();
                }
                else if (_fingerDown.y - _fingerUp.y < 0)//Down swipe
                {
                    OnSwipeDown();
                }
                _fingerUp = _fingerDown;
            }

            //Check if Horizontal swipe
            else if (HorizontalValMove() > SWIPE_THRESHOLD && HorizontalValMove() > VerticalMove())
            {
                if (_fingerDown.x - _fingerUp.x > 0)//Right swipe
                {
                    OnSwipeRight();
                }
                else if (_fingerDown.x - _fingerUp.x < 0)//Left swipe
                {
                    OnSwipeLeft();
                }
                _fingerUp = _fingerDown;
            }
        }

        private float VerticalMove()
        {
            return Mathf.Abs(_fingerDown.y - _fingerUp.y);
        }

        private float HorizontalValMove()
        {
            return Mathf.Abs(_fingerDown.x - _fingerUp.x);
        }

        private void OnSwipeUp()
        {
            _swipeDirection = SwipeDirection.Up;
            Debug.Log("Swipe UP");
        }

        private void OnSwipeDown()
        {
            _swipeDirection = SwipeDirection.Down;
            Debug.Log("Swipe Down");
        }

        private void OnSwipeLeft()
        {
            _swipeDirection = SwipeDirection.Left;
            Debug.Log("Swipe Left");
        }

        private void OnSwipeRight()
        {
            _swipeDirection = SwipeDirection.Right;
            Debug.Log("Swipe Right");
        }
    }
}