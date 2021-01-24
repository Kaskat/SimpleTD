using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Data;
using Tower;
using Enemy;

namespace GameCore
{
    public class DIContainer : MonoBehaviour
    {
        [SerializeField] private BuildPanelView _buildPanelView;
        [SerializeField] private RaycastManager _raycastManager;
        [SerializeField] private TowerManager _towerManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HUD _informationPanelController;
        [SerializeField] private GameParametrs _gameParametrs;
        [SerializeField] private LevelController _levelController;
        [SerializeField] private Field _field;
        [Space]
        [SerializeField] private TowerStorage _towerStorage;
        [SerializeField] private EnemyStorage _enemyStorage;
        [Space]
        [SerializeField] private Camera _mainCamera;
        [Space]
        [SerializeField] private LevelData _levelData;

        private Gate _gate;
        private BuildPanelController _buildPanelController;
        private List<Slot> _slots;

        private void Awake()
        {
            //TODO: Get level data from level storage by id
            _gameParametrs.Initialize(_levelData);
            _gate = new Gate(_gameParametrs);
            _enemyManager.Initialize(_enemyStorage, _gate);
            _field.Initialize(_towerManager, _enemyManager, _gate);
            _levelController.Initialize(_gameParametrs, _enemyManager);
            _raycastManager.Initialize(_mainCamera, _gameParametrs);
            _towerManager.Initialize(_towerStorage, _levelController.slots.Count);
            _informationPanelController.Initialize(_gameParametrs);

            _slots = _levelController.slots;

            _buildPanelController = new BuildPanelController(_buildPanelView, _towerStorage, _gameParametrs, _mainCamera);


            foreach (var slot in _slots)
            {
                slot.Initialize(_buildPanelController, _towerManager);
            }
        }
    }
}

