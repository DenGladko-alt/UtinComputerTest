using TMPro;
using UnityEngine;

namespace Utin
{
    public class SizeGaugeUI : MonoBehaviour
    {
        [SerializeField] private PlayerBall playerBall;

        [SerializeField] private TextMeshProUGUI ScaleLeftText;

        private void OnEnable()
        {
            playerBall.OnBallSizeChanged += PlayerBallOnOnBallSizeChanged;
        }

        private void OnDisable()
        {
            playerBall.OnBallSizeChanged -= PlayerBallOnOnBallSizeChanged;
        }

        private void PlayerBallOnOnBallSizeChanged(float percent)
        {
            ScaleLeftText.text = "Size left: " + percent;
        }
    }   
}
