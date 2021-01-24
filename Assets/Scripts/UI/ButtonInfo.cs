using Data;
using System;
using UnityEngine;

namespace UI
{
    public class ButtonInfo
    {
        #region Events

        public event Action<bool> BuildСanBuy;

        #endregion Events

        #region Fields

        public Sprite emblemSprite;
        public int cost;
        public int id;
        public bool isLocked;

        #endregion Fields

        #region Public Methods

        public ButtonInfo(Sprite emblem, int towerCost, int towerId, bool locked)
        {
            emblemSprite = emblem;
            cost = towerCost;
            id = towerId;
            isLocked = locked;
        }

        public void CanBuild(bool value)
        {
            BuildСanBuy?.Invoke(value);
        }

        #endregion Public Methods
    }
}