using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _crystalsText;
    [SerializeField] private TMP_Text _killsText;
    [SerializeField] private MouthTrigger _mouthTrigger;

    private void OnEnable()
    {
        _mouthTrigger.CrystalEaten += OnCrystalEaten;
        _mouthTrigger.HumanEaten += OnHumanEaten;
    }

    private void OnDisable()
    {
        _mouthTrigger.CrystalEaten -= OnCrystalEaten;
        _mouthTrigger.HumanEaten -= OnHumanEaten;
    }

    private void OnCrystalEaten(int count)
    {
        _crystalsText.text = count.ToString();
    }
    
    private void OnHumanEaten(int count)
    {
        _killsText.text = count.ToString();
    }
}
