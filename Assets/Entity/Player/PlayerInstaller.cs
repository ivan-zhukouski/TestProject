using System;
using UnityEngine;

namespace Entity.Player
{
    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerBase _currentPlayerPrefab = null;
        [SerializeField] private Transform _spawnPoint = null;
        [HideInInspector] public PlayerBase Player;

        private void Awake()
        {
            Player = Instantiate(_currentPlayerPrefab, _spawnPoint.transform);
        }
    }
}
