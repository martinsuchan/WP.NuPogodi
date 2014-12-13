using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NuPogodi.AppLogic.Components
{
    public class BackgroundComponent : ItemComponent
    {
        protected static Texture2D Texture;

        public BackgroundComponent(ContentManager contentManager)
            : base(contentManager)
        {
        }

        public override void LoadContent()
        {
            if (Texture == null)
            {
                Texture = ContentManager.Load<Texture2D>("images/bg");
            }
        }

        public override void UnloadContent()
        {
            Texture = null;
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Draw(Texture, new Rectangle(0, 0, Common.W, Common.H), Color.White);
        }
    }
}
