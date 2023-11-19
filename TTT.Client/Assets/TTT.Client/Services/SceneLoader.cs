using UnityEngine.SceneManagement;

namespace TTT.Client.Services
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadLoginScene()
        {
            SceneManager.LoadScene("01_Login");
        }

        public void LoadLobbyScene()
        {
            SceneManager.LoadScene("02_Lobby");
        }

        public void LoadGameScene()
        {
            SceneManager.LoadScene("03_Game");
        }
    }
}