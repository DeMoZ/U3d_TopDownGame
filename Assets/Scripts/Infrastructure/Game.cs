using Services.Input;
using UnityEngine;

namespace Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

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