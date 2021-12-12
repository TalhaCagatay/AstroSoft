using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "PlayerControllerInitStep", menuName = "AstroSoft/InitModules/PlayerControllerInitStep", order = 0)]
    public class PlayerControllerInitStep : InitStep
    {
        protected override void InternalStep()
        {
            GameController.Instance.PlayerController.Initialized += OnInitialized;
            GameController.Instance.PlayerController.Init();
        }

        private void OnInitialized()
        {
            GameController.Instance.PlayerController.Initialized -= OnInitialized;
            FinalizeStep();
        }
    }
}