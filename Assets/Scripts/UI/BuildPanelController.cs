using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace UI
{
    public class BuildPanelController
    {
        #region Events

        public event Action<int> NextTowerSelected;

        #endregion Events

        #region Fields

        private GameParametrs _gameParametrs;
        private BuildPanelView _buildPanelView;
        private Camera _mainCamera;
        private TowerStorage _towerStorage;
        private List<ButtonInfo> _buttons;

        #endregion Fields

        #region Public Methods
  
        public BuildPanelController(BuildPanelView buildPanelView, TowerStorage towerStorage, GameParametrs gameParametrs, Camera camera)
        {
            _gameParametrs = gameParametrs;
            _buildPanelView = buildPanelView;
            _mainCamera = camera;
            _towerStorage = towerStorage;

            _buildPanelView.Initialize(gameParametrs);
            _buildPanelView.BuildButtonClicked += (nextId) => NextTowerSelected(nextId);
            _buildPanelView.CloseButtonClicked += () => NextTowerSelected = null;

            _gameParametrs.CoinChanged += CheckPurchasing;
        }

        public void OpenPanel(int targetId, Vector3 position)
        {
            TowerData towerData = _towerStorage.GetTowerDataById(targetId);
            int buildCount = towerData.nextTowerUpgrade.Length + 1;

            _buttons = new List<ButtonInfo>(buildCount);

            for (int i = 0; i < towerData.nextTowerUpgrade.Length; i++)
            {
                TowerData nextTower = towerData.nextTowerUpgrade[i];

                //TODO: Define cost
                _buttons.Add(new ButtonInfo(nextTower.emblem, nextTower.price, nextTower.id, nextTower.isLocked));
            }

            //TODO: REMOVE HARDCODE
            if(targetId > 0)
            {
                //Get Sprite from GameConfig
                var sellButton = new ButtonInfo(_gameParametrs.sellSprite, -(towerData.price / 2), 0, false);
                _buttons.Add(sellButton);
            }

            Vector3 panelPosition = _mainCamera.WorldToScreenPoint(position);
            _buildPanelView.OpenPanel(_buttons, panelPosition);

            CheckPurchasing(_gameParametrs.Coin);
        }

        #endregion Public Methods

        #region Private Methods

        private void CheckPurchasing(int currentCoin)
        {
            if (_buttons == null)
                return;

            foreach (var button in _buttons)
            {
                if (button.isLocked)
                {
                    button.CanBuild(false);
                    return;
                }

                bool isCanPurchase = button.cost < currentCoin;
                button.CanBuild(isCanPurchase);
            }
        }

        #endregion Private Methods
    }
}
