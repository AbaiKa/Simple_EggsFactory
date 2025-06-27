using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    public void Init()
    {
        playButton.onClick.AddListener(PlayButton);
        quitButton.onClick.AddListener(QuitButton);
    }
    public void Show()
    {
        panel.SetActive(true);
    }
    private void PlayButton()
    {
        panel.SetActive(false);
    }
    private void QuitButton()
    {
        Application.Quit();
    }
}
