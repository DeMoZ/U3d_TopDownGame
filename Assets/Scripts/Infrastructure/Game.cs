using Services.Input;
using UnityEngine;

namespace Scripts.Infrastructure
{
    public class Game
    {
        public IInputService InputService;

        public Game()
        {
            RegisterInputService();
        }

        private void RegisterInputService()
        {
            if (Application.isMobilePlatform)
                InputService = new MobileInputService();
            else
                InputService = new StandaloneInputService();
        }
    }
}