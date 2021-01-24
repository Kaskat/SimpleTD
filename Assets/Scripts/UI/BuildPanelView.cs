using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;

namespace UI
{
    public class BuildPanelView : MonoBehaviour
    {
        #region Events

        public event Action<int> BuildButtonClicked;
        public event Action CloseButtonClicked;

        #endregion Events

        #region Fields

        [SerializeField] private List<ControllButton> _controllButtons;
        [SerializeField] private Button _exitButton;

        private GameParametrs _gameParametrs;

        //TODO: move this to GameConfig
        public float radius = 1.2f; //px

        #endregion Fields

        #region Private Methods

        private void BuildPurchase(int nextTowerId, int cost)
        {
            BuildButtonClicked(nextTowerId);
            CloseButtonClicked();
            _gameParametrs.Coin -= cost;
        }

        private void ClosePanel()
        {
            _gameParametrs.IsControllingPanelOpen = false;
            gameObject.SetActive(false);

            foreach (var controlButton in _controllButtons)
            {
                controlButton.Deactivate();
            }
        }

        #endregion Private Methods

        #region Public Methods

        public void Initialize(GameParametrs gameParametrs)
        {
            _gameParametrs = gameParametrs;

            CloseButtonClicked += ClosePanel;
            _exitButton.onClick.AddListener(()=> CloseButtonClicked());
        }

        public void OpenPanel(List<ButtonInfo> buttons, Vector3 panelPosition)
        {
            transform.position = panelPosition;

            for (int i = 0; i < buttons.Count; i++)
            {
                int id = buttons[i].id;
                int cost = buttons[i].cost;

                float step = 360f / buttons.Count;
                float angle = (step * i + 90f) * Mathf.Deg2Rad;
                var position = transform.position + 
                    new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);

                _controllButtons[i].Configure(buttons[i], position);
                _controllButtons[i].Button.onClick.AddListener(()=>BuildPurchase(id, cost));
            }

            _gameParametrs.IsControllingPanelOpen = true;
            gameObject.SetActive(true);
        }

        #endregion Public Methods
    }
}
