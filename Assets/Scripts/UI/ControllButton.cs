using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class ControllButton : MonoBehaviour
    {
        #region Constants

        //TODO: Move to GameConfig
        private const float whiteValue = 1f;
        private const float grayColor = 0.5f;

        #endregion Constants

        #region Fields

        [SerializeField] private Image _emblemImage;
        [SerializeField] private Image _priceBackroundImage;
        [SerializeField] private TextMeshProUGUI _priceText;
        
        public Button Button;
        private ButtonInfo _buttonData;

        #endregion Fields

        #region Public Methods

        public void Configure(ButtonInfo buttonData, Vector3 position)
        {
            _buttonData = buttonData;
            _emblemImage.sprite = buttonData.emblemSprite;
            _priceText.text = Mathf.Abs(buttonData.cost).ToString();

            transform.position = position;

            //TODO: Redesign block button
            if (buttonData.isLocked)
            {
                _priceText.text = "--";
                ShowButton(false);
                gameObject.SetActive(true);
                return;
            }

            _buttonData.BuildСanBuy += ShowButton;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _buttonData.BuildСanBuy -= ShowButton;
            Button.onClick.RemoveAllListeners();
            gameObject.SetActive(false);
        }

        public void ShowButton(bool value)
        {
            Button.interactable = value;

            if (value)
            {
                _priceText.color = SetColorToGray(_priceText.color, whiteValue);
                _priceBackroundImage.color = SetColorToGray(_priceBackroundImage.color, whiteValue);
            }
            else
            {
                _priceText.color = SetColorToGray(_priceText.color, grayColor);
                _priceBackroundImage.color = SetColorToGray(_priceBackroundImage.color, grayColor);
            }
        }

        #endregion Public Methods

        #region Private Methods

        //TODO: MOVE TO UTIL
        private Color SetColorToGray(Color color, float grayValue)
        {
            Color.RGBToHSV(color, out float H, out float S, out float V);
            V = grayValue;
            return Color.HSVToRGB(H, S, V);
        }

        #endregion Private Methods
    }
}
