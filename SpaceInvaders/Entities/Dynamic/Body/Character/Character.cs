using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    public class Character : Body
    {
        public Character(World world, Vector2 pos, Vector2 size, Texture2D tex,
                         float maxVel = 200.0f, float accel = 1000.0f, float friction = 5.0f)
            : base(world, pos, size, tex, maxVel, accel, friction) { }

        public override bool CollidesWithMe(Entity e)
        {
            return base.CollidesWithMe(e);
        }
    }
}