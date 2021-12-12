using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Game.Scripts.Game;
using UnityEngine;

namespace _Game.Scripts.Initializations
{
    [CreateAssetMenu(fileName = "GameInitModule", menuName = "AstroSoft/InitModules/GameInitModule", order = 0)]
    public class InitStep : ScriptableObject
    {
        public event Action Initialized;

        [SerializeField] private List<InitStep> _dependencies;

        [NonSerialized] private bool _initRequested;
        [NonSerialized] private bool _isInitialized;
        [NonSerialized] private Dictionary<InitStep, bool> _dependencyInitStatus = new Dictionary<InitStep, bool>();
        
        private string _name;

        public bool IsInitialized => _isInitialized;

        public void Run()
        {
            _name = name;

            if (_initRequested) return;

            for (var i = 0; i < _dependencies.Count; i++)
                _dependencyInitStatus[_dependencies[i]] = false;
            _initRequested = true;

            Debug.Log($"[INITIALIZER]{LOGS.HEAD_LOG} {_name} started initialization at {Thread.CurrentThread.ManagedThreadId}.");

            if (_dependencies.TrueForAll(x => x.IsInitialized))
            {
                Debug.Log($"[INITIALIZER]{LOGS.HEAD_LOG} {_name} already initialized");
                InternalStep();
            }
            else
            {

                foreach (var dep in _dependencies)
                {
                    _dependencyInitStatus[dep] = dep.IsInitialized;

                    if (!dep.IsInitialized)
                    {
                        var temp = dep;
                        dep.Initialized += AfterInit;
                        dep.Run();
                        void AfterInit()
                        {
                            Debug.Log($"[INITIALIZER]{LOGS.HEAD_LOG} {Thread.CurrentThread.ManagedThreadId} {_name} afterInit");
                            temp.Initialized -= AfterInit;
                            _dependencyInitStatus[temp] = temp.IsInitialized;
                            if (_dependencyInitStatus.All(x => x.Value))
                                InternalStep();
                        }
                    }
                }
            }
        }

        protected virtual void InternalStep()
        {
            Debug.Log($"[INITIALIZER]{LOGS.HEAD_LOG} {Thread.CurrentThread.ManagedThreadId} {name} internal step");
            FinalizeStep();
        }

        protected void FinalizeStep()
        {
            Debug.Log($"<color=green>[INITIALIZER]{LOGS.HEAD_LOG} {Thread.CurrentThread.ManagedThreadId} {_name} finalized initialization.</color>");
            _isInitialized = true;
            Initialized?.Invoke();
        }
    }
}