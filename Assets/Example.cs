using System.Linq;
using Bro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Example : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _labelId;
    [SerializeField] private TextMeshProUGUI _labelStatus;
    [SerializeField] private TextMeshProUGUI _labelData;
    [SerializeField] private TMP_InputField _inputKey;
    [SerializeField] private Button _buttonLoad;
    [SerializeField] private Button _buttonSave;
    [SerializeField] private Button _buttonGenerate;

    private const string KeyId = "const_id";
    private const int RandomSize = 10000;
    private static readonly Random _random = new Random();
    
    private async void Awake()
    {
        _buttonLoad.onClick.AddListener(OnButtonLoad);
        _buttonSave.onClick.AddListener(OnButtonSave);
        _buttonGenerate.onClick.AddListener(OnButtonGenerate);
        
        var id = await Storage.Get(KeyId);
        if (string.IsNullOrEmpty(id))
        {
            id = RandomString(4);
            Storage.Save(KeyId, id);
        }
        
        _labelStatus.text = "loaded";
        _labelData.text = "---";
        _labelId.text = $"id = [{id}]";
    }

    private void OnButtonGenerate()
    {
        var key = _inputKey.text;
        var text = RandomString(RandomSize);
        _labelData.text = text;
        _labelStatus.text = $"generated text for key = [{key}]; data length = {text?.Length}";
    }

    private async void OnButtonLoad()
    {
        var key = _inputKey.text;
        var text = await Storage.Get(key);
        var hash = Storage.GetHash(key);
        _labelData.text = text;
        _labelStatus.text = $"loaded data: key = [{key}]; hash = [{hash}]; data length = {text?.Length}";
    }

    private void OnButtonSave()
    {
        var key = _inputKey.text;
        var text = _labelData.text;
        var hash = Storage.GetHash(key);
        Storage.Save(key, text);
        _labelStatus.text = $"saved data: key = [{key}]; hash = [{hash}]; data length = {text?.Length}";
    }

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
