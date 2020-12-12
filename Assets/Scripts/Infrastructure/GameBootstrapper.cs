using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            Game game = new Game();
            
            DontDestroyOnLoad(this);
        }
    }
}