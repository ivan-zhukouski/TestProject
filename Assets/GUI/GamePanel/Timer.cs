using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GUI.GamePanel
{
    public class Timer : MonoBehaviour
    {
        public TimeSettings _timeSettings;
        
        public Text TimerText; 
        public Text BreakWallsText;
        public Text StayInTheLineText;
       
        
        private float _timer = 0f;
        public float CountDawn { get; set; }

        public void SetTimerValue()
        {
            _timer = CountDawn;
        }

        private void Update()
        {
            if ((int)_timeSettings == 0 || TimerText != null)
            {
                _timer -= Time.deltaTime;
                TimerText.text = ((int)_timer -((int)_timer / 60) * 60).ToString("D2");
            }
        }
    }
    
    public enum TimeSettings
    {
        Timer
    }
}
