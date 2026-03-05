using UnityEngine;

/// <summary>
/// A class that a responsible for a giving the ability to the object to have health, and to damage and destroying it. 
/// (Well, i need to split the responsibilities. This class shouldnt have be responsible for destroing object)
/// </summary>
public class EntityHealth : MonoBehaviour, IDamageble
{
    [SerializeField] private MonoBehaviour _eventReceiverBehaviour;
    private IAnimationEventReceiver eventReceiver;
    [SerializeField] private float _maxHealth = 30;
    [SerializeField] private float _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;

        eventReceiver = _eventReceiverBehaviour as IAnimationEventReceiver;
        if (eventReceiver == null)
        {
            Debug.LogError($"{_eventReceiverBehaviour.name} does not implement IAnimationEventReceiver!");
        }
    }

    public void TakeDamage(DamageData damageData)
    {
        if (_currentHealth <= 0) return;

        _currentHealth -= damageData.Amount;
        Debug.Log("ќтримали {damageData.Amount} шкоди, напр€мок атаки: {damageData.Direction}, залишилось здоровь€: {_currentHealth}");

        // Play animation using hit direction and apply knockback to the entity
    }

    public void SendHurtEvent()
    {
        eventReceiver?.OnHurtEvent();
    }

    public void Die()
    {
        SendHurtEvent();
        Destroy(gameObject);
    }
}
