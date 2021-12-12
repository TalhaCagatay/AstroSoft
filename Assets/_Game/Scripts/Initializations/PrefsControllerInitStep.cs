using _Game.Scripts.Game.Controllers;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "PrefsControllerInitStep", menuName = "AstroSoft/InitModules/PrefsControllerInitStep", order = 0)]
    public class PrefsControllerInitStep : InitStep
    {
        protected override void InternalStep()
        {
            GameController.Instance.PrefsController = new PrefsController();
            GameController.Instance.PrefsController.Initialized += OnInitialized;
            GameController.Instance.PrefsController.Init();
        }

        private void OnInitialized()
        {
            GameController.Instance.PrefsController.Initialized -= OnInitialized;
            FinalizeStep();
        }
    }
}