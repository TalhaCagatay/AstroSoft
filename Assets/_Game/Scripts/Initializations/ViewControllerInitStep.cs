using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "ViewControllerInitStep", menuName = "AstroSoft/InitModules/ViewControllerInitStep", order = 0)]
    public class ViewControllerInitStep : InitStep
    {
        protected override void InternalStep()
        {
            GameController.Instance.ViewController.Initialized += OnInitialized;
            GameController.Instance.ViewController.Init();
        }

        private void OnInitialized()
        {
            GameController.Instance.ViewController.Initialized -= OnInitialized;
            FinalizeStep();
        }
    }
}