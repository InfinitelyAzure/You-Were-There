using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    // Hàm gọi khi bấm nút "Play"
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Map");
    }

    // Hàm gọi khi bấm nút "Options"
    public void LoadOptionScene()
    {
        SceneManager.LoadScene("OptionScene");
    }

    // Nếu muốn thoát game
    public void QuitGame()
    {
        Application.Quit();
    }
}
