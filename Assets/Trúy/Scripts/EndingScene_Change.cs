using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingLoader : MonoBehaviour
{
    // Hàm gọi khi bấm nút MainMenu
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Nếu muốn thoát game
    public void QuitGame()
    {
        Application.Quit();
    }
}
