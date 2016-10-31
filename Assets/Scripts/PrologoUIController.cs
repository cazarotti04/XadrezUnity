using UnityEngine;
using UnityEngine.SceneManagement;

public class PrologoUIController : MonoBehaviour
{

    public void Sair()
    {
        SceneManager.LoadScene("inicio");
    }
}
