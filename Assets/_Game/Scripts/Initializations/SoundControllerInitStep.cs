using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "SoundControllerInitStep", menuName = "AstroSoft/InitModules/SoundControllerInitStep", order = 0)]
    public class SoundControllerInitStep : InitStep
    {
        protected override void InternalStep()
        {
            GameController.Instance.SoundController.Initialized += OnInitialized;
            GameController.Instance.SoundController.Init();
        }

        private void OnInitialized()
        {
            GameController.Instance.SoundController.Initialized -= OnInitialized;
            FinalizeStep();
        }
    }
}