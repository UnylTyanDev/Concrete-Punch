using UnityEngine;

public class EntityHealth : MonoBehaviour, IDamageble
{
    [SerializeField] private float _maxHealth = 30;
    [SerializeField] private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(DamageData damageData)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damageData.Amount;
        Debug.Log("ќтримали {damageData.Amount} шкоди, напр€мок атаки: {damageData.Direction}, залишилось здоровь€: {_currentHealth}");

        // Play animation using hit direction and apply knockback to the entity
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
