using TMPro;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _text;

    public void Display(int damage)
    {
        _text.text = damage.ToString();
        _animator.SetTrigger("Display");
        Invoke(nameof(Disapear), 1);
    }

    private void Disapear()
    {
        gameObject.SetActive(false);
    }
}
