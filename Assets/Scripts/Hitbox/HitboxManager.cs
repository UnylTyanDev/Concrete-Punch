using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask _targetLayers; // Which layers is considered as enemies to deal damage
    [SerializeField] private bool _debugDraw;

    private Collider _collider;
    private List<IDamageble> _targetsToDamage; // Targets that already got damaged (Its added to not harm one enemy twice, even if we really wanted to)
    private bool _isActive;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            Debug.Log("Хітбокс потребує колайдера щоб працювати");
            return;
        }
        _collider.isTrigger = true;
        _targetsToDamage = new List<IDamageble>();
        gameObject.SetActive(false); // We turn off the hitbox as default
    }

    /// <summary>
    /// Activate hitbox for dealing damage.
    /// </summary>
    /// <param name="damage"> Base damage (How many damage?)</param>
    /// <param name="damageMultiplier"> Multiplies base damage (How strong were this attack?) </param>
    /// <param name="direction"> Attack hit direction (From where attack came from?)</param>
    /// <param name="knockbackForce"> Force that we use to push victim away (How strong is knockback force is?)</param>
    public void Activate(float damage, float damageMultiplier, HitDirection direction, float knockbackForce)
    {
        if (_isActive) return;

        _isActive = true;
        gameObject.SetActive(true);
        _targetsToDamage.Clear();

        // Gathering all colliders that is inside of a hitbox
        Collider[] hitColliders = Physics.OverlapBox(_collider.bounds.center, _collider.bounds.extents, _collider.transform.rotation, _targetLayers);
        foreach (var hitCollider in hitColliders)
        {
            // Skipping the player collider
            if (hitCollider.transform.IsChildOf(transform.root)) continue;

            IDamageble damageble = hitCollider.GetComponent<IDamageble>();

            if (damageble != null && !_targetsToDamage.Contains(damageble))
            {
                DamageData damageData = new DamageData(damage * damageMultiplier, direction, knockbackForce);
                damageble.TakeDamage(damageData);
                _targetsToDamage.Add(damageble);
            }

            if (_debugDraw)
            {
                Debug.DrawLine(_collider.bounds.center, _collider.bounds.center + Vector3.up * 0.5f, Color.red, 1f);
            }
        }
    }

    public void Deactivate()
    {
        if (!_isActive) return;
        _isActive = false;
        gameObject.SetActive(false);
        _targetsToDamage.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        if (_collider == null) return;

        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;

        if (_collider is BoxCollider box) Gizmos.DrawWireCube(box.center, box.size);
    }
}
