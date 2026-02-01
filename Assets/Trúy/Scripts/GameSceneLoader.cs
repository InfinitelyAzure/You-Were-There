using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogSceneLoader : MonoBehaviour
{
    // Hàm gọi khi bấm nút MainMenu
    public void ToBadEnd()
    {
        SceneManager.LoadScene("BadEnding");
    }

    // Nếu muốn thoát game
    public void ToGoodEnd()
    {
        SceneManager.LoadScene("RealEnding");
    }
}
