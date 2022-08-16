using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanelComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _toggle.isOn = false;
    }

    public void Initialize(LevelTask task)
    {
        _toggle.isOn = false;
        _descriptionText.text = task.Description;
        task.Complete += OnTaskCompleted;
    }

    private void OnTaskCompleted(LevelTask task)
    {
        _toggle.isOn = true;
        _animator.SetTrigger("Win");
    }

    public void UpdateDescription(string description)
    {
        _descriptionText.text = description;
    }
}
