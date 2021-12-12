using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "AsteroidControllerInitStep", menuName = "AstroSoft/InitModules/AsteroidControllerInitStep", order = 0)]
    public class AsteroidControllerInitStep : InitStep
    {
        protected override void InternalStep()
        {
            GameController.Instance.AsteroidController.Initialized += OnInitialized;
            GameController.Instance.AsteroidController.Init();
        }

        private void OnInitialized()
        {
            GameController.Instance.AsteroidController.Initialized -= OnInitialized;
            FinalizeStep();
        }
    }
}