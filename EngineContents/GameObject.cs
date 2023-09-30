// You are not suppose to create a GameObject the same way as how you create objects, new GameObject(...);
// You need to create it like this, Engine.CreateGameObject(...); Mainly because it will not do anything, and it won't render at all.
// Go to Engine.cs file and look into the methods that are related to GameObjects which will help you utilize the GameObjects.

using System.Numerics;

namespace Consyl_Engine.EngineContents
{
    class GameObject
    {
        // Collision Width and Height
        public int width;
        public int height;
        public int objID = -1;

        public float friction = 0.1f; // Movement Friction for velocity
        public float gravityStrength = 0.5f; // Gravity strength

        public bool collisionEnabled; // If true, it's going to collide with other GameObjects
        public bool detectOverlap; // If true, it's going to check if the object is overlapping an object or not
        public bool collideWithBounds; // If true, it's not going to allow the GameObject to leave the screen bounds
        public bool drawDebugCollision; // If true, it will draw the hit-box of the GameObject, it's useful for debugging
        public bool hasGravity = false; // If true, gravity will be enabled for the GameObject
        public bool isPushable = false; // If true, collisions with other GameObjects will cause this object to be pushed or moved
        public bool isOverlapping = false; // a boolean that will tell if a GameObject is overlapping with this one. Will work only if detectOverlap is true
        public bool isVisible = true; // GameObject will be invisible if set to false

        public Texture objectSprite; // Object texture

        public Vector2 collisionOffset; // The offsets the collision box relative to the location
        public Vector2 location; // The location of the GameObject
        public Vector2 speed = new Vector2(0.0f, 0.0f); // The Velocity of the GameObject

        // returns the 4 corners of the GameObject's collision box
        public Vector2[] colCorners  { get { return new Vector2[4] { location + collisionOffset, location + collisionOffset + new Vector2(width, 0),
            location + collisionOffset + new Vector2(0, height), location + collisionOffset + new Vector2(width, height) }; } }


        /// <summary>
        /// Constructor that initializes a GameObject. 
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_CollisionEnabled"></param>
        /// <param name="_collisionWidth"></param>
        /// <param name="_collisionHeight"></param>
        /// <param name="_detectOverlap"></param>
        /// <param name="_image"></param>
        /// <param name="_isPushable"></param>
        /// <param name="_colOffsetX"></param>
        /// <param name="_colOffsetY"></param>
        /// <param name="_collideWithBounds"></param>
        /// <param name="_drawDebugCollision"></param>
        /// <param name="objID"></param>
        public GameObject(int _x, int _y, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap,
            Texture _image, bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false, int objID = -1)
        {
            location = new Vector2(_x, _y);
            collisionEnabled = _CollisionEnabled;
            width = _collisionWidth;
            height = _collisionHeight;
            detectOverlap = _detectOverlap;
            objectSprite = _image;
            isPushable = _isPushable;
            collisionOffset = new Vector2(_colOffsetX, _colOffsetY);
            collideWithBounds = _collideWithBounds;
            drawDebugCollision = _drawDebugCollision;
            this.objID = objID;
        }

        /// <summary>
        /// Constructor that initializes a GameObject.
        /// </summary>
        /// <param name="_loc"></param>
        /// <param name="_CollisionEnabled"></param>
        /// <param name="_collisionWidth"></param>
        /// <param name="_collisionHeight"></param>
        /// <param name="_detectOverlap"></param>
        /// <param name="_image"></param>
        /// <param name="_isPushable"></param>
        /// <param name="_colOffsetX"></param>
        /// <param name="_colOffsetY"></param>
        /// <param name="_collideWithBounds"></param>
        /// <param name="_drawDebugCollision"></param>
        /// <param name="objID"></param>
        public GameObject(Vector2 _loc, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap,
            Texture _image, bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false, int objID = -1)
        {
            location = _loc;
            collisionEnabled = _CollisionEnabled;
            width = _collisionWidth;
            height = _collisionHeight;
            detectOverlap = _detectOverlap;
            objectSprite = _image;
            isPushable = _isPushable;
            collisionOffset = new Vector2(_colOffsetX, _colOffsetY);
            collideWithBounds = _collideWithBounds;
            drawDebugCollision = _drawDebugCollision;
            this.objID = objID;
        }

        /// <summary>
        /// Constructor that initializes a GameObject without a texture.
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <param name="_CollisionEnabled"></param>
        /// <param name="_collisionWidth"></param>
        /// <param name="_collisionHeight"></param>
        /// <param name="_detectOverlap"></param>
        /// <param name="_isPushable"></param>
        /// <param name="_colOffsetX"></param>
        /// <param name="_colOffsetY"></param>
        /// <param name="_collideWithBounds"></param>
        /// <param name="_drawDebugCollision"></param>
        /// <param name="objID"></param>
        public GameObject(int _x, int _y, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap,
            bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false, int objID = -1)
        {
            location = new Vector2(_x, _y);
            collisionEnabled = _CollisionEnabled;
            width = _collisionWidth;
            height = _collisionHeight;
            detectOverlap = _detectOverlap;
            isPushable = _isPushable;
            collisionOffset = new Vector2(_colOffsetX, _colOffsetY);
            collideWithBounds = _collideWithBounds;
            drawDebugCollision = _drawDebugCollision;
            this.objID = objID;
        }

        /// <summary>
        /// Constructor that initializes a GameObject without a texture.
        /// </summary>
        /// <param name="_loc"></param>
        /// <param name="_CollisionEnabled"></param>
        /// <param name="_collisionWidth"></param>
        /// <param name="_collisionHeight"></param>
        /// <param name="_detectOverlap"></param>
        /// <param name="_isPushable"></param>
        /// <param name="_colOffsetX"></param>
        /// <param name="_colOffsetY"></param>
        /// <param name="_collideWithBounds"></param>
        /// <param name="_drawDebugCollision"></param>
        /// <param name="objID"></param>
        public GameObject(Vector2 _loc, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap,
            bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false, int objID = -1)
        {
            location = _loc;
            collisionEnabled = _CollisionEnabled;
            width = _collisionWidth;
            height = _collisionHeight;
            detectOverlap = _detectOverlap;
            isPushable = _isPushable;
            collisionOffset = new Vector2(_colOffsetX, _colOffsetY);
            collideWithBounds = _collideWithBounds;
            drawDebugCollision = _drawDebugCollision;
            this.objID = objID;
        }

        /// <summary>
        /// Updates the GameObject.
        /// </summary>
        public void Update()
        {
            location += speed; // Moves Object based on speed

            // Friction to reduce speed on the X axis
            if (speed.X > 0.0f)
            {
                if (speed.X > friction)
                    speed.X -= friction;
                else
                    speed.X -= speed.X;
            }
            if (speed.X < 0.0f)
            {
                if (speed.X < -friction)
                    speed.X += friction;
                else
                    speed.X += -speed.X;
            }

            // Friction to reduce speed on the Y axis
            if (speed.Y > 0.0f)
            {
                if (speed.Y > friction)
                    speed.Y -= friction;
                else
                    speed.Y -= speed.Y;
            }
            if (speed.Y < 0.0f)
            {
                if (speed.Y < -friction)
                    speed.Y += friction;
                else
                    speed.Y += -speed.Y;
            }

            if (hasGravity) // if gravity is enabled, a constant force will be pulling down on the object
                speed.Y += gravityStrength * 0.98f;
        }

        /// <summary>
        /// Draws the GameObject
        /// </summary>
        /// <param name="areBlackPixelsTransparent"></param>
        public void DrawUpdate(bool areBlackPixelsTransparent = true)
        {
            if (objectSprite != null)
                if (isVisible) // Will only render the texture sprite if isVisible is true
                    objectSprite.DrawImage((int)location.X, (int)location.Y, areBlackPixelsTransparent); // Draws the texture sprite

            // This will draw a rectangle that will show the collision box only if drawDebugCollision is true
            if (drawDebugCollision)
                gfx.DrawRectangle((int)(location.X + collisionOffset.X), (int)(location.Y + collisionOffset.Y), (int)width, (int)height, '.', true);
        }

        /// <summary>
        /// A method that will allow adding velocity into the speed.
        /// </summary>
        /// <param name="addedVelocity"></param>
        public void AddVelocity(Vector2 addedVelocity)
        {
            speed += addedVelocity;
        }

        /// <summary>
        /// A method that will allow adding the coordinates into the location which will make it move.
        /// </summary>
        /// <param name="addedLoc"></param>
        public void AddLocation(Vector2 addedLoc)
        {
            location += addedLoc;
        }

        /// <summary>
        /// A method for teleporting the GameObject.
        /// </summary>
        /// <param name="newLoc"></param>
        /// <param name="resetSpeed"></param>
        public void Teleport(Vector2 newLoc, bool resetSpeed)
        {
            location = newLoc;

            if (resetSpeed)
                speed = new Vector2(0.0f, 0.0f);
        }

        /// <summary>
        /// A method that will return true if isOverlapping is true and vice versa.
        /// </summary>
        /// <returns></returns>
        public bool IsObjectOverlapping()
        {
            return isOverlapping;
        }

        /// <summary>
        /// Returns the center of the collision box.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetObjectCollisionCenter()
        {
            return Utilities.Vec2D.Midpoint2D(location + collisionOffset, location + new Vector2(width, height));
        }

        /// <summary>
        /// Returns the Area of the collision box.
        /// </summary>
        /// <returns></returns>
        public int GetCollisionArea()
        {
            return width * height;
        }

        /// <summary>
        /// Gets Distance between this GameObject and other GameObject by ID.
        /// </summary>
        /// <param name="otherObjID"></param>
        /// <returns></returns>
        public float GetDistanceToGameObject(int otherObjID)
        {
            return Utilities.Vec2D.Distance2D(location, Engine.GetGameObjectByID(otherObjID).location);
        }
    }
}
