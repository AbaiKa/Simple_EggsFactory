using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [field: Header("Properties")]
    [field: SerializeField]
    public string ID { get; private set; }
    [field: SerializeField]
    public int Value { get; private set; }
    [field: SerializeField]
    public int Price { get; private set; }
    [field: SerializeField, TextArea()]
    public string Description { get; private set; }
    [Header("Components")]
    [SerializeField] private Button buttonComponent;
    [SerializeField] private TextMeshProUGUI textComponent;

    public UnityEvent<UpgradeButton> onSelect = new UnityEvent<UpgradeButton>();
    public void Init()
    {
        buttonComponent.onClick.AddListener(OnClick);
    }
    public void UpdateItem(string text)
    {
        textComponent.text = text;
    }
    private void OnClick()
    {
        onSelect?.Invoke(this);
    }
}
