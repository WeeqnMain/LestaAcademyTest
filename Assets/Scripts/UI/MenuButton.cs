using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;

        public void LoadScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
