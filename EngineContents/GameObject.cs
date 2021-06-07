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

        // constructor, which would initialize the GameObject
        public GameObject(int _x, int _y, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap, Texture _image, bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false, int objID = -1)
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

        public GameObject(int _x, int _y, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap, bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false, int objID = -1)
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

        public void Update() // Executed every frame to update it
        {
            location += speed; // Moves Object based on speed

            // Friction to reduce speed on the X axis
            if (speed.X > 0.0f)
            {
                if (speed.X > friction)
                {
                    speed.X -= friction;
                }
                else
                {
                    speed.X -= speed.X;
                }
            }
            if (speed.X < 0.0f)
            {
                if (speed.X < -friction)
                {
                    speed.X += friction;
                }
                else
                {
                    speed.X += -speed.X;
                }
            }

            // Friction to reduce speed on the Y axis
            if (speed.Y > 0.0f)
            {
                if (speed.Y > friction)
                {
                    speed.Y -= friction;
                }
                else
                {
                    speed.Y -= speed.Y;
                }
            }
            if (speed.Y < 0.0f)
            {
                if (speed.Y < -friction)
                {
                    speed.Y += friction;
                }
                else
                {
                    speed.Y += -speed.Y;
                }
            }

            if (hasGravity) // if gravity is enabled, a constant force will be pulling down on the object
            {
                speed.Y += gravityStrength * 0.98f;
            }
        }

        public void DrawUpdate(bool areBlackPixelsTransparent = true) // Updates the graphics
        {
            if (objectSprite != null)
            {
                if (isVisible) // Will only render the texture sprite if isVisible is true
                {
                    objectSprite.DrawImage((int)location.X, (int)location.Y, areBlackPixelsTransparent); // Draws the texture sprite
                }
            }

            // This will draw a rectangle that will show the collision box only if drawDebugCollision is true
            if (drawDebugCollision)
            {
                gfx.DrawRectangle((int)(location.X + collisionOffset.X), (int)(location.Y + collisionOffset.Y), (int)width, (int)height, '.', true);
            }
        }

        public void AddVelocity(Vector2 addedVelocity) // A method that will allow adding velocity into the speed
        {
            speed += addedVelocity;
        }

        public void AddLocation(Vector2 addedLoc) // A method that will allow adding the coordinates into the location which will make it move
        {
            location += addedLoc;
        }

        public void Teleport(Vector2 newLoc, bool resetSpeed) // A method for teleporting the GameObject
        {
            location = newLoc;

            if (resetSpeed)
            {
                speed = new Vector2(0.0f, 0.0f);
            }
        }

        public bool IsObjectOverlapping() // A method that will return true if isOverlapping is true and vice versa
        {
            return isOverlapping;
        }
        
        public Vector2 GetObjectCollisionCenter() // Returns the center of the collision box
        {
            return Utilities.Vec2D.Midpoint2D(location + collisionOffset, location + new Vector2(width, height));
        }
    }
}
