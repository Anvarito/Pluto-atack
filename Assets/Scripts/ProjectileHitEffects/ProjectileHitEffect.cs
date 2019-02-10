using Projectiles;
using UnityEngine;

namespace ProjectileHitEffects
{
	/// <inheritdoc />
	/// <summary>
	/// Эффект от попадания снаряда
	/// </summary>
	public abstract class ProjectileHitEffect : MonoBehaviour
	{
		/// <summary>
		/// Инициировать эффект от попадания снаряда
		/// </summary>
		/// <param name="target">Цель в которую попадает снаряд</param>
		/// <param name="shell">Снаряд</param>
		public abstract void Initiate(GameObject target, Shell shell);
	}
}