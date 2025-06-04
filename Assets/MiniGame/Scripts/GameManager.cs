using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("GameManager: StartGame called");
        // Здесь логика запуска игры (UI включение, переменные сбросить и т.д.)
    }

    public void EndGame()
    {
        Debug.Log("GameManager: EndGame called");
        // Здесь логика завершения игры (UI выключить, сохранить результат и т.д.)
    }

}
