using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    public bool InMenu { get; private set; }
    public void Init()
    {
        playButton.onClick.AddListener(PlayButton);
        quitButton.onClick.AddListener(QuitButton);
        Show();
    }
    public void Show()
    {
        panel.SetActive(true);
        InMenu = true;
        var eggs = FindObjectsByType<EggComponent>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        for (int i = 0; i < eggs.Length; i++)
        {
            eggs[i].DeInit();
        }
    }
    private void PlayButton()
    {
        InMenu = false;
        panel.SetActive(false);
    }
    private void QuitButton()
    {
        Application.Quit();
    }
}
