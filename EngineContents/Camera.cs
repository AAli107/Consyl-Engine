using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Consyl_Engine.EngineContents
{
    class Camera
    {
        private Vector2 loc;
        public float speed;

        public Vector2 camPos { get { return loc; } }

        public Camera() // Default Camera constructor
        {
            loc = new Vector2(0,0);
            speed = 1;
        }

        public Camera(Vector2 _loc) // Camera constructor to decide where to create the camera
        {
            loc = _loc;
            speed = 1;
        }

        public Camera(Vector2 _loc, float _speed) // Camera constructor to decide where to create the camera and what speed to move
        {
            loc = _loc;
            speed = _speed;
        }

        /// <summary>
        /// Offsets the camera location based on the offset vector xy values.
        /// </summary>
        /// <param name="offset"></param>
        public void MoveCamera(Vector2 offset)
        {
            Vector2 newLoc = new Vector2(Utilities.Numbers.ClampN(loc.X + offset.X, 0, Engine.worldSize.X - gfx.drawWidth),
                Utilities.Numbers.ClampN(loc.Y + offset.Y, 0, Engine.worldSize.Y - gfx.drawHeight));

            loc = newLoc;
        }

        /// <summary>
        /// Offsets the camera location based on the dir(normalized vector) multiplied by the camera's speed
        /// </summary>
        /// <param name="dir"></param>
        public void MoveCameraBySpeed(Vector2 dir)
        {
            Vector2 clampedDir = new Vector2(Utilities.Numbers.ClampN(dir.X, -1, 1), Utilities.Numbers.ClampN(dir.Y, -1, 1));

            Vector2 newLoc = new Vector2(Utilities.Numbers.ClampN(loc.X + (clampedDir.X * speed), 0, Engine.worldSize.X - gfx.drawWidth),
                Utilities.Numbers.ClampN(loc.Y + (clampedDir.Y * speed), 0, Engine.worldSize.Y - gfx.drawHeight));

            loc = newLoc;
        }

        public void SetCameraPosition(Vector2 newPos)
        {
            loc = new Vector2(Utilities.Numbers.ClampN(newPos.X, 0, Engine.worldSize.X - gfx.drawWidth),
                Utilities.Numbers.ClampN(newPos.Y, 0, Engine.worldSize.Y - gfx.drawHeight));
        }
    }
}
