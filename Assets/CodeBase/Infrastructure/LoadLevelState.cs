using CodeBase.CameraLogic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string HeroPath = "Hero/Hero";
        private const string HudPath = "Hud/Hud";
        private const string InitialPoint = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _stateMachine = stateMachine;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPoint);
            GameObject hero = InstantiateObject(HeroPath, initialPoint.transform.position);

            InstantiateObject(HudPath);

            CameraFollow(hero);

            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject follow) =>
            Camera.main.GetComponent<CameraFollow>().Follow(follow);

        private static GameObject InstantiateObject(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static GameObject InstantiateObject(string path, Vector3 at)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            prefab = Object.Instantiate(prefab, at, Quaternion.identity);

            return prefab;
        }
    }
}