using GameStateMachine.StartGameState;
using UnityEngine;

namespace Environment.Walls
{
    public class WallBase : MonoBehaviour
    {
        private float _wallSpeed = 2f;
        private const float MIN_Y_FOR_WALL = -32.5f;
        private const float MAX_Y_FOR_WALL = 32.5f;
        
        private void Update()
        {
            MoveWallDawn();
            CircleForWall();
        }

        private void MoveWallDawn()
        {
            if (StartState.CanPlay)
            {
                transform.Translate(Vector3.down * _wallSpeed * Time.deltaTime);
            }
        }

        private void CircleForWall()
        {
            if (transform.position.y <= MIN_Y_FOR_WALL)
            {
                transform.position = new Vector3(transform.position.x, MAX_Y_FOR_WALL, transform.position.z);
            }
        }
    }
}
