using UnityEngine;
using DG.Tweening;

namespace Tower
{
    public class BoomTower : BaseTower
    {
        #region Fields

        [SerializeField] private Transform shell;

        private Sequence towerAttack;

        #endregion Fields

        #region Protected Methods

        protected override void Attack()
        {
            if (isCanAttack || towerAttack == null)
            {
                var enemy = FindDangerousEnemy();

                if (!enemy.IsReadyMove)
                    return;

                var predicatePosition = enemy.GetPositionThroughTime(_towerData.shellFlyDuration);

                towerAttack = DOTween.Sequence();

                towerAttack.AppendCallback(() => isCanAttack = false);

                towerAttack.Append(shell.DOMove(shell.transform.position + Vector3.up, _towerData.shellFlyDuration * 0.3f).SetEase(Ease.Linear));

                //TODO: Move to Predicate position
                towerAttack.Append(shell.DOMove(predicatePosition, _towerData.shellFlyDuration * 0.7f)
                    .SetEase(Ease.Linear));
                towerAttack.AppendCallback(() =>
                {
                    enemy.ApplyEffect(EffectType.Damage, _towerData.damage);
                });
                towerAttack.Append(shell.DOMove(transform.position, 0f));

                towerAttack.AppendCallback(() => isCanAttack = true);
            }
        }

        #endregion Protected Methods
    }
}