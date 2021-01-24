using Data;

namespace GameCore
{
    public class Gate
    {
        #region Fields

        private GameParametrs _gameParametrs;

        #endregion Fields

        #region Public Methods

        public Gate(GameParametrs gameParametrs)
        {
            _gameParametrs = gameParametrs;
        }

        public void Damage(int damage)
        {
            _gameParametrs.Health -= damage;
        }

        #endregion Public Methods
    }
}

