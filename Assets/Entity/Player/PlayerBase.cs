using System;
using System.Collections;
using Environment.Walls;
using UnityEngine;


namespace Entity.Player
{
    public class PlayerBase : MonoBehaviour
    {
        private Animator _animator => GetComponent<Animator>();
        public Transform Transform => transform;
        
        public event Action PlayerDie;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Obstacle wallBase = other.gameObject.GetComponent<Obstacle>();
            
            if (wallBase != null)
            {
                SpriteRenderer spriteRenderer = wallBase.gameObject.GetComponent<SpriteRenderer>();
                StartCoroutine(WallColorChange(spriteRenderer));
                PlayerDie?.Invoke();
            }

            ScaleBoost scaleBoost = other.gameObject.GetComponent<ScaleBoost>();
            if (scaleBoost != null)
            {
                StartCoroutine(PlayerScale(scaleBoost.gameObject));
            }
        }

        private IEnumerator WallColorChange(SpriteRenderer renderer)
        {
            renderer.color = new Color(1,0,0);
            yield return new WaitForSeconds(0.1f);
            renderer.color = new Color(1,1,0);
            yield return new WaitForSeconds(0.1f);
            renderer.color = new Color(1,0,0);
        }

        private IEnumerator PlayerScale(GameObject go)
        {
            _animator.SetBool("PlayerScale", true);
            _animator.SetBool("PlayerAnScale", false);
            go.SetActive(false);
            
            yield return new WaitForSeconds(10f);
            _animator.SetBool("PlayerScale", false);
            _animator.SetBool("PlayerAnScale", true);
            go.SetActive(true);
        }
    }
}
