using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLifeSystem : MonoBehaviour
{
        public void TakeDamage(int damage)
{
    // Aqui você pode criar seu sistema de vida depois
    Debug.Log($"Player tomou {damage} de dano!");
}
}
