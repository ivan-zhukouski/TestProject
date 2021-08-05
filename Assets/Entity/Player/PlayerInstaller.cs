using GUI;
using UnityEngine;
using Zenject;

namespace Entity.Player
{
    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerBase _currentPlayerPrefab = null;
        [SerializeField] private Transform _spawnPoint = null;
        [HideInInspector] public PlayerBase Player;
        [Inject] private GuiHandler _guiHandler;

        private void Awake()
        {
            Player = Instantiate(_currentPlayerPrefab, _spawnPoint.transform);
            Player._gui = _guiHandler;
        }
    }
}
