using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Button deleteSaveButton;
    [SerializeField] private Button cancelButton;

    public UnityEvent<float> onMusicChanged = new UnityEvent<float>();
    public UnityEvent<float> onSoundChanged = new UnityEvent<float>();

    public void Init(float music, float sound)
    {
        cancelButton.onClick.AddListener(Hide);
        deleteSaveButton.onClick.AddListener(DeleteSave);
        musicSlider.onValueChanged.AddListener(OnMusicChanged);
        soundSlider.onValueChanged.AddListener(OnSoundChanged);

        musicSlider.value = music;
        soundSlider.value = sound;

        onMusicChanged?.Invoke(music);
        onSoundChanged?.Invoke(sound);  
    }
    private void OnMusicChanged(float value)
    {
        onMusicChanged?.Invoke(value);
    }
    private void OnSoundChanged(float value)
    {
        onSoundChanged?.Invoke(value);
    }
    public void Show()
    {
        panel.SetActive(true);
    }
    private void Hide()
    {
        panel.SetActive(false);
    }
    private void DeleteSave()
    {
        SceneManager.LoadScene(0);
    }
}
