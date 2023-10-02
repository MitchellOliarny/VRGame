using UnityEngine;

[CreateAssetMenu]

public class DeathParticleDatabase : ScriptableObject
{

    [Header("Death Particle Database")]
    [SerializeField] private ParticleSystem[] ParticleSystemDatabase; // Particle System Array

    #region GETTERS
    //-- GETTERS --\\
    public ParticleSystem GetParticleIndex(int i) => ParticleSystemDatabase[i]; // Returns Index of Particle System Database
    public ParticleSystem[] GetParticleArray { get { return ParticleSystemDatabase; } } // Returns Particle System Database Array
    #endregion
}
