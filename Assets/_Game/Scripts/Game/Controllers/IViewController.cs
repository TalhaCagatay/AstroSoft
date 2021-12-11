using System;
using System.Collections.Generic;
using _Game.Scripts.Game.Views;

namespace _Game.Scripts.Game.Controllers
{
    public interface IViewController : IController
    {
        event Action StartClicked;
        event Action RetryClicked;
        event Action ExitClicked;
        Queue<IView> ActiveViews { get; set; }
        Queue<IView> ActivePopups { get; set; }
        IView MenuView { get; set; }
        IView LevelCompleteView { get; set; } 
        IView LevelFailView { get; set; } 
        IView GameplayView { get; set; } 
        IView SplashView { get; set; } 
        void ChangeView(IView viewToOpen);
        void OpenPopup(IView popupToOpen);
        void ClosePopup();
        void StartButton();
        void RestartButton();
    }
}