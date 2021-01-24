using UnityEngine;
using Data;

namespace GameCore
{
    public class RaycastManager : MonoBehaviour
    {
        #region Fields

        private Camera _mainCamera;
        private GameParametrs _gameParametrs;

        #endregion Fields

        #region Public Methods
     
        public void Initialize(Camera camera, GameParametrs gameParametrs)
        {
            _mainCamera = camera;
            _gameParametrs = gameParametrs;
        }

        #endregion Public Methods

        #region MonoBehaviour

        private void Update()
        {
            if (_gameParametrs.IsControllingPanelOpen)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit;
                var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(mouseWorldPosition, Vector3.back);

                //REMOVE: debug ray
                Debug.DrawRay(mouseWorldPosition, mouseWorldPosition + Vector3.forward * 10000, Color.green);

                if (hit.collider != null)
                {
                    var clickableEntity = hit.collider.gameObject.GetComponent<IClickable>();
                    if (clickableEntity != null)
                    {
                        clickableEntity.OnClick();
                    }
                    return;
                }
            }
        }

        #endregion MonoBehaviour
    }
}

