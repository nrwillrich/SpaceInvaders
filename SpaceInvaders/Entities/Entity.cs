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

        public bool isVisible = true;

        public Entity(World world) { m_world = world; }

        public virtual bool Update(GameTime gameTime) { return true;  }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {}
    }
}
