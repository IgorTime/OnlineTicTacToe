using TTT.Client.Root;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private ApplicationScope applicationScope;

    private void Start()
    {
        var container = Instantiate(applicationScope);
        DontDestroyOnLoad(container.gameObject);

        SceneManager.LoadScene("00_Main");
    }
}