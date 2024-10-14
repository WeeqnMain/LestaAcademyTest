using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameRoot
{
    public class GameRestarter : MonoBehaviour
    {
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
