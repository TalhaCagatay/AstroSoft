using UnityEngine;

namespace _Game.Scripts.MobileInput
{
    public class JoyStickView : MonoBehaviour
    {
        public static bool Firing;
        
        private void Awake()
        {
#if UNITY_EDITOR
            gameObject.SetActive(false);
#endif
        }

        public void OnFiring() => Firing = true;
        public void OnFireReleased() => Firing = false;
    }
}