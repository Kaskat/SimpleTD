using UnityEngine;

namespace Tower
{
    public class IceTotemTower : BaseTower
    {
        #region Fields

        [SerializeField] private int _freezeDiviser = 2;

        #endregion Fields

        #region Protected Methods

        protected override void Attack()
        {
            foreach (var enemy in _enemiesInRange)
            {
                enemy.ApplyEffect(EffectType.Freeze, _freezeDiviser);
            }
        }

        #endregion Protected Methods
    }
}