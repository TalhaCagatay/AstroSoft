using System;
using _Game.Scripts.Game.Levels;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Game.Views
{
    public class LevelCompleteView : MonoBehaviour, IView
    {
        public event Action<IView> ViewOpened;
        public event Action<IView> ViewClosed;

        
        public ILevel Level { get; set; }
        public void Init(){}
        
        public void Open()
        {
            gameObject.SetActive(true);
            ViewOpened?.Invoke(this);
        }

        public void Close()
        {
            gameObject.SetActive(false);
            ViewClosed?.Invoke(this);
        }
    }
}