using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "ParticleControllerInitStep", menuName = "AstroSoft/InitModules/ParticleControllerInitStep", order = 0)]
    public class ParticleControllerInitStep : InitStep
    {
        protected override void InternalStep()
        {
            GameController.Instance.ParticleController.Initialized += OnInitialized;
            GameController.Instance.ParticleController.Init();
        }

        private void OnInitialized()
        {
            GameController.Instance.ParticleController.Initialized -= OnInitialized;
            FinalizeStep();
        }
    }
}