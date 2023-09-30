/*
 * This Character class is a child class of the GameObject class.
 * If you want to instantiate it you can do it like this: Engine.CreateGameObject(new Character( ... ));
*/

using System.Numerics;
using static Consyl_Engine.EngineContents.Utilities.Enums;

namespace Consyl_Engine.EngineContents.GameObjectChildren
{
    class Character : GameObject
    {
        private Vector2 spawnpoint; // Stores the initial spawn point of the character
        private float hitpoints; // The character's HP
        private float maxHitpoints; // The character's max HP
        private float walkSpeed; // stores the character's walking speed
        private bool isInvincible; // HP will not deplete if invincible

        // Stores the type of damage last time the character was damaged
        private DamageType damageType = DamageType.None;

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
            bool _collideWithBounds = false, bool _drawDebugCollision = false)
            : base(_loc, _CollisionEnabled, (int)_colWidth.X, (int)_colWidth.Y, true,
            true, (int)_colOffset.X, (int)_colOffset.Y, _collideWithBounds, _drawDebugCollision, -1)
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
        public void Damage(float dmg, DamageType damageType = DamageType.Generic)
        {
            if (!isInvincible && !isDead)
            {
                hitpoints = Utilities.Numbers.ClampN(hitpoints - dmg, 0, maxHitpoints);
                this.damageType = damageType;
            }
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
        /// Will move the character in a specific direction based on its walkSpeed variable
        /// </summary>
        /// <param name="dir"></param>
        public void WalkTowardsDirection(Vector2 dir, bool useVelocity = true)
        {
            if (useVelocity) AddVelocity(dir * walkSpeed);
            else AddLocation(dir * walkSpeed); 
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
        /// Sets the character's walk speed
        /// </summary>
        /// <param name="newWalkSpeed"></param>
        public void SetWalkSpeed(float newWalkSpeed)
        {
            walkSpeed = newWalkSpeed;
        }

        /// <summary>
        /// Sets the character to be either invincible or not
        /// </summary>
        /// <param name="isInvincible"></param>
        public void SetIsInvincible(bool isInvincible)
        {
            this.isInvincible = isInvincible;
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

        /// <summary>
        /// Returns the character's walkSpeed
        /// </summary>
        /// <returns></returns>
        public float GetWalkSpeed()
        {
            return walkSpeed;
        }

        /// <summary>
        /// Returns whether the character is invincible or not
        /// </summary>
        /// <returns></returns>
        public bool IsInvincible()
        {
            return isInvincible;
        }

        /// <summary>
        /// Returns the type of damage the character was last hit by
        /// </summary>
        /// <returns></returns>
        public DamageType GetDamageType()
        {
            return damageType;
        }
    }
}
