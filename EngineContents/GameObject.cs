using Consyl_Engine.EngineContents;
using System;
using System.Numerics;

namespace Consyl_Engine.EngineContents
{
    class GameObject
    {
        public int width;
        public int height;

        public float friction = 0.1f;
        public float gravityStrength = 1.0f;

        public bool collisionEnabled;
        public bool detectOverlap;
        public bool collideWithBounds;
        public bool drawDebugCollision;
        public bool hasGravity = false;
        public bool isPushable = false;
        public bool isOverlapping = false;

        public Texture objectSprite;

        public Vector2 collisionOffset;
        public Vector2 location;
        public Vector2 speed = new Vector2(0.0f, 0.0f);

        public GameObject(int _x, int _y, bool _CollisionEnabled, int _collisionWidth, int _collisionHeight, bool _detectOverlap, Texture _image, bool _isPushable, int _colOffsetX = 0, int _colOffsetY = 0, bool _collideWithBounds = false, bool _drawDebugCollision = false)
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
        }

        public void Update()
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

            if (collideWithBounds) // Checks if the object is colliding with screen bounds
            {
                if (location.X + collisionOffset.X + width >= gfx.drawWidth)
                {
                    location.X = gfx.drawWidth - (collisionOffset.X + width);
                    if (speed.X > 0.0f)
                    {
                        speed.X = 0.0f;
                    }
                }
                else if (location.X + collisionOffset.X < 0.0f)
                {
                    location.X = -collisionOffset.X;
                    if (speed.X < 0.0f)
                    {
                        speed.X = 0.0f;
                    }
                }

                if (location.Y + collisionOffset.Y + height >= gfx.drawHeight)
                {
                    location.Y = gfx.drawHeight - (collisionOffset.Y + height);
                    if (speed.Y > 0.0f)
                    {
                        speed.Y = 0.0f;
                    }
                }
                else if (location.Y + collisionOffset.Y < 0.0f)
                {
                    location.Y = -collisionOffset.Y;
                    if (speed.Y < 0.0f)
                    {
                        speed.Y = 0.0f;
                    }
                }
            }
        }

        public void DrawUpdate(bool areBlackPixelsTransparent = true)
        {
            objectSprite.DrawImage((int)location.X, (int)location.Y, areBlackPixelsTransparent);

            if (drawDebugCollision)
            {
                gfx.DrawRectangleOutline((int)(location.X + collisionOffset.X), (int)(location.Y + collisionOffset.Y), (int)width, (int)height, '.');
            }
        }

        public void AddVelocity(Vector2 addedVelocity)
        {
            speed += addedVelocity;
        }

        public void AddLocation(Vector2 addedLoc)
        {
            location += addedLoc;
        }

        public void Teleport(Vector2 newLoc, bool resetSpeed)
        {
            location = newLoc;

            if (resetSpeed)
            {
                speed = new Vector2(0.0f, 0.0f);
            }
        }

        public bool IsObjectOverlapping()
        {
            return isOverlapping;
        }
    }
}
