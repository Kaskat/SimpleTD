using UnityEngine;
using GameCore;
using UI;

namespace Tower
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Slot : MonoBehaviour, IClickable
    {
        #region Constants

        private const int _id = 0;

        #endregion Constants

        #region Fields

        private BuildPanelController _buildPanelController;
        private SpriteRenderer _spriteRenderer;
        private TowerManager _towerBuilder;
        private BaseTower _tower;

        #endregion Fields

        #region Public Methods

        public void Initialize(BuildPanelController buildPanelController, TowerManager towerBuilder)
        {
            _buildPanelController = buildPanelController;
            _towerBuilder = towerBuilder;
        }

        public void OnClick()
        {
            int id = _tower != null ? _tower.Id : _id;

            _buildPanelController.OpenPanel(id, transform.position);
            _buildPanelController.NextTowerSelected += OnTowerSelected;
        }

        #endregion Public Methods

        #region Private Methods

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTowerSelected(int nextTowerId)
        {
            if(_tower != null)
            {
                _towerBuilder.DistructBuild(_tower);
            }

            _buildPanelController.NextTowerSelected -= OnTowerSelected;

            if (nextTowerId == _id)
            {
                _spriteRenderer.enabled = true;
                _tower = null;
                return;
            }

            _tower = _towerBuilder.Build(nextTowerId, transform.position);
            _spriteRenderer.enabled = false;
        }

        #endregion Private Methods
    }
}
