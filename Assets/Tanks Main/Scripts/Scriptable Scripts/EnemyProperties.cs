using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NameSpaceName {
    public enum DoubleMissile { none, doubleMissile, doubleMissileSameTime }

    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemy Propertiess")]
    public class EnemyProperties : ScriptableObject
    {

        #region Variables
        [Tooltip("find next patrol point")]
        public float minDisToPatrol = 5f;

        public float minStoppingDist = 5f;
        public float attackRadius = 20f;
        public float startDecisionForNextPointTime = 0;
        public float fireRate = 0.2f;
        public float maxSpeed = 10f;
        public DoubleMissile doubleMissile;

        public float startHealth = 100f;
        #endregion

        #region Builtin Methods



        #endregion

        #region Custom Methods

        #endregion

    }
}
