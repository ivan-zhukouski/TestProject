using System;
using System.Collections;
using UnityEngine;

namespace GUI.GamePanel
{
    [RequireComponent(typeof(GameView))]
    public class GameViewController : MonoBehaviour
    {
        public GameView View => GetComponent<GameView>();
        private void OnEnable()
        {
           
        }

        private void OnDisable()
        {
          
        }
        
    }
}
