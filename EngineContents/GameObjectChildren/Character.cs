using System.Numerics;

namespace Consyl_Engine.EngineContents.GameObjectChildren
{
    class Character : GameObject
    {
        private Vector2 spawnpoint; // Stores the initial spawn point of the character
        private float hitpoints; // The character's HP
        private float maxHitpoints; // The character's max HP
        private float walkSpeed; // stores the character's walking speed
        private bool isInvincible; // HP will not deplete if invincible

        /// <summary>
        /// returns whether the character's hitpoints is completely depleted or not
        /// </summary>
        public bool isDead { get { return hitpoints <= 0; } }

        /// <summary>
        /// Constructor for the character class
        /// </summary>
        /// <param name="maxHitpoints"></param>
        /// <param name="walkSpeed"></param>
        /// <param name="isInvincible"></param>
        /// <param name="_loc"></param>
        /// <param name="_colWidth"></param>
        /// <param name="_colOffset"></param>
        /// <param name="_CollisionEnabled"></param>
        /// <param name="_collideWithBounds"></param>
        /// <param name="_drawDebugCollision"></param>
        /// <param name="objID"></param>
        public Character(float maxHitpoints, float walkSpeed, bool isInvincible,

            Vector2 _loc, Vector2 _colWidth, Vector2 _colOffset, bool _CollisionEnabled,
            bool _collideWithBounds = false, bool _drawDebugCollision = false, int objID = -1)
            : base(_loc, _CollisionEnabled, (int)_colWidth.X, (int)_colWidth.Y, true,
            true, (int)_colOffset.X, (int)_colOffset.Y, _collideWithBounds, _drawDebugCollision, objID)
        {
            this.maxHitpoints = maxHitpoints;
            this.walkSpeed = walkSpeed;
            this.isInvincible = isInvincible;

            hitpoints = this.maxHitpoints;
            spawnpoint = location;
        }


        /// <summary>
        /// Respawns Character with full HP back to its spawnpoint
        /// </summary>
        public void RespawnCharacter()
        {
            hitpoints = maxHitpoints;
            Teleport(spawnpoint, true);
        }

        /// <summary>
        /// Damages Character by reducing its hitpoints
        /// </summary>
        /// <param name="dmg"></param>
        public void Damage(float dmg)
        {
            if (!isInvincible && !isDead) hitpoints = Utilities.Numbers.ClampN(hitpoints - dmg, 0, maxHitpoints);
        }

        /// <summary>
        /// Heals Character by increasing its hitpoints
        /// </summary>
        /// <param name="heal"></param>
        public void Heal(float heal)
        {
            if (!isDead) hitpoints = Utilities.Numbers.ClampN(hitpoints + heal, 0, maxHitpoints);
        }

        /// <summary>
        /// Sets the character's hitpoints
        /// </summary>
        /// <param name="newHP"></param>
        public void SetHP(float newHP)
        {
            hitpoints = Utilities.Numbers.ClampN(newHP, 0, maxHitpoints);
        }

        /// <summary>
        /// Sets the character's maximum hitpoints
        /// </summary>
        /// <param name="newMaxHP"></param>
        public void SetMaxHP(float newMaxHP)
        {
            maxHitpoints = newMaxHP;
            hitpoints = Utilities.Numbers.ClampN(hitpoints, 0, maxHitpoints);
        }

        /// <summary>
        /// Returns the character's hitpoints
        /// </summary>
        /// <returns></returns>
        public float GetHP()
        {
            return hitpoints;
        }

        /// <summary>
        /// Returns the character's maximum hitpoints
        /// </summary>
        /// <returns></returns>
        public float GetMaxHP()
        {
            return maxHitpoints;
        }
    }
}
