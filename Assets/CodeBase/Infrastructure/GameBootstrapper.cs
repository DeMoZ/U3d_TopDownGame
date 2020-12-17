using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public LoadingCurtain Curtain;
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, Curtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        public Coroutine StartCoroune(IEnumerator coroutine)
        {
            throw new NotImplementedException();
        }
    }
}