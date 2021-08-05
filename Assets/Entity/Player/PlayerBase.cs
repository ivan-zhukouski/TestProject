using System;
using System.Collections;
using Environment.Boosts;
using Environment.Walls;
using GUI;
using UnityEngine;


namespace Entity.Player
{
    public class PlayerBase : MonoBehaviour
    {
        private Animator _animator => GetComponent<Animator>();
        public Transform Transform => transform;

        private float _boostDuration  = 10f;
        
        private bool _canDestroyWalls = false;
        private bool _canScale = false;

        [HideInInspector] public GuiHandler _gui;
        
        public event Action PlayerDie;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Obstacle wallBase = other.gameObject.GetComponent<Obstacle>();
            if (wallBase != null)
            {
                if (!_canDestroyWalls)
                {
                    SpriteRenderer spriteRenderer = wallBase.gameObject.GetComponent<SpriteRenderer>();
                    StartCoroutine(WallColorChange(spriteRenderer));
                    PlayerDie?.Invoke();
                }
                else
                {
                    StartCoroutine(ActiveWallAfterTime(wallBase.gameObject));
                }
            }

            ScaleBoost scaleBoost = other.gameObject.GetComponent<ScaleBoost>();
            if (scaleBoost != null)
            {
                _canScale = true;
                _gui.GameViewController.View.TimePanel.CountDawn = _boostDuration;
                _gui.GameViewController.View.TimePanel.SetTimerValue();
                _gui.GameViewController.ActiveTimer(true);
                _gui.GameViewController.View.TimePanel.TimerText.gameObject.SetActive(true);
                StartCoroutine(PlayerScale(scaleBoost.gameObject));
            }

            WallDestroyerBoost wallDestroyerBoost = other.gameObject.GetComponent<WallDestroyerBoost>();
            if (wallDestroyerBoost != null)
            {
                _canDestroyWalls = true;
                _gui.GameViewController.View.TimePanel.CountDawn = _boostDuration;
                _gui.GameViewController.View.TimePanel.SetTimerValue();
                _gui.GameViewController.ActiveTimer(true);
                _gui.GameViewController.View.TimePanel.BreakWallsText.gameObject.SetActive(true);
                StartCoroutine(DestroyWalls(wallDestroyerBoost.gameObject));
            }
        }

        private IEnumerator WallColorChange(SpriteRenderer renderer)
        {
            renderer.color = new Color(1,0.5f,0.5f);
            yield return new WaitForSeconds(0.1f);
            renderer.color = new Color(1,1,0);
            yield return new WaitForSeconds(0.1f);
            renderer.color = new Color(1,0.5f,0.5f);
        }

        private IEnumerator PlayerScale(GameObject go)
        {
            if (_canScale)
            {
                _animator.SetBool("PlayerScale", true);
                _animator.SetBool("PlayerAnScale", false);
            }
            go.SetActive(false);
            
            yield return new WaitForSeconds(_boostDuration);
            if (_canScale)
            {
                _animator.SetBool("PlayerScale", false);
                _animator.SetBool("PlayerAnScale", true);
            }
            go.SetActive(true);
            _gui.GameViewController.View.TimePanel.TimerText.gameObject.SetActive(false);
            _gui.GameViewController.View.TimePanel.gameObject.SetActive(false);
            _canScale = false;
        }
        private IEnumerator DestroyWalls(GameObject go)
        {
            _animator.SetBool("ChangeColor", true);
            go.SetActive(false);
            yield return new WaitForSeconds(_boostDuration - 1.5f);
            _gui.GameViewController.View.TimePanel.StayInTheLineText.gameObject.SetActive(true);
            _gui.GameViewController.View.TimePanel.BreakWallsText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            go.SetActive(true);
            _gui.GameViewController.View.TimePanel.StayInTheLineText.gameObject.SetActive(false);
            _gui.GameViewController.View.TimePanel.gameObject.SetActive(false);
            _canDestroyWalls = false;
            _animator.SetBool("ChangeColor", false);
        }

        private IEnumerator ActiveWallAfterTime(GameObject go)
        {
            go.SetActive(false);
            yield return  new WaitForSeconds(_boostDuration);
            go.SetActive(true);
        }
    }
}
