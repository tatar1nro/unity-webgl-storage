using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bro;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Example : MonoBehaviour
{
    private const int RandomSize = 10000;
    
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TMP_InputField _inputKey;
    [SerializeField] private Button _buttonLoad;
    [SerializeField] private Button _buttonSave;
    [SerializeField] private Button _buttonGenerate;
    
    private static readonly Random _random = new Random();
    
    private void Awake()
    {
        _label.text = "---";
        _buttonLoad.onClick.AddListener(OnButtonLoad);
        _buttonSave.onClick.AddListener(OnButtonSave);
        _buttonGenerate.onClick.AddListener(OnButtonGenerate);
    }

    private void OnButtonGenerate()
    {
        var text = RandomString(RandomSize);
        _label.text = text;
    }

    private async void OnButtonLoad()
    {
        var key = _inputKey.text;
        var text = await Storage.Get(key);
        _label.text = text;
        
        Debug.Log($"id = {Storage.GetHash(key)}");
    }

    private void OnButtonSave()
    {
        var key = _inputKey.text;
        var text = _label.text;
        Storage.Save(key, text);
        _label.text = "saved";
        
        Debug.Log($"id = {Storage.GetHash(key)}");
    }

    private static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}
