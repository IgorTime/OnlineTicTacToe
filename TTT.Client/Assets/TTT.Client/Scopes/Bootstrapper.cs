using TTT.Client.Scopes;
using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private ApplicationScope applicationScope;

    private void Start()
    {
        var container = Instantiate(applicationScope);
        DontDestroyOnLoad(container.gameObject);
    }
}