using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SpaceInvaders
{
    public class Entity
    {
        public World m_world;

        public bool isVisible;

        public Entity(World world) { m_world = world; }

        public virtual void Update(GameTime gameTime) {}

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {}
    }
}
