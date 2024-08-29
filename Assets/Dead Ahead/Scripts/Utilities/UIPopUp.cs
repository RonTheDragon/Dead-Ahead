using TMPro;
using UnityEngine;

public class UIPopUp : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _disapearTime = 2;

    public void Display(string toDisplay)
    {
        _text.text = toDisplay;
        _animator.SetTrigger("Display");
        Invoke(nameof(Disapear), _disapearTime);
    }

    private void Disapear()
    {
        gameObject.SetActive(false);
    }
}
