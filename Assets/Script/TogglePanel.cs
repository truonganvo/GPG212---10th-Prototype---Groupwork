using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    public GameObject panel;
    private bool isPanelVisible = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isPanelVisible = !isPanelVisible;
            panel.SetActive(isPanelVisible);
        }
    }
}
