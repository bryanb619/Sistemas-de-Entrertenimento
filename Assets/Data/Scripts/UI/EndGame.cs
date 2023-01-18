using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject scene;

    // Start is called before the first frame update
    void Start()
    {
         scene.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("_Start");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
