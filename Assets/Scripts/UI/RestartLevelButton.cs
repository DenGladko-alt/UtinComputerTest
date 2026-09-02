using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utin
{
    [RequireComponent(typeof(Button))]

    public class RestartLevelButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(RestartLevel);
        }
        
        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
