using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace vptest
{
    public class Game1 : Game
    {
        private const float SPEED = 300;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private float _horizon;
        private float _vp1;
        private Rectangle _box;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _horizon = 150;
            _box = new Rectangle(250, 300, 200, 200);

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 768;
            _graphics.ApplyChanges();

            _vp1 = _graphics.PreferredBackBufferWidth / 2f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _box.Y -= (int)(SPEED * gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _box.Y += (int)(SPEED * gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _box.X -= (int)(SPEED * gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _box.X += (int)(SPEED * gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.F))
                _vp1 -= (int)(SPEED * gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.H))
                _vp1 += (int)(SPEED * gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.T))
                _horizon -= (int)(SPEED * gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.G))
                _horizon += (int)(SPEED * gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.DrawLine(new Vector2(0, _horizon), new Vector2(_graphics.PreferredBackBufferWidth, _horizon), Color.DarkGreen);
            DrawPoint(new Vector2(_vp1, _horizon), Color.Yellow);

            DrawBox();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawBox()
        {
            var vp1 = new Vector2(_vp1, _horizon);
            
            var tl1 = new Vector2(_box.Left, _box.Top);
            var tl2 = vp1 - ((vp1 - tl1) * 0.75f);
            var tr1 = new Vector2(_box.Right, _box.Top);
            var tr2 = vp1 - ((vp1 - tr1) * 0.75f);
            var bl1 = new Vector2(_box.Left, _box.Bottom);
            var bl2 = vp1 - ((vp1 - bl1) * 0.75f);
            var br1 = new Vector2(_box.Right, _box.Bottom);
            var br2 = vp1 - ((vp1 - br1) * 0.75f);

            var brect = new Rectangle((int)tl2.X, (int)tl2.Y, (int)(tr2.X - tl2.X), (int)(bl2.Y - tl2.Y));

            _spriteBatch.DrawRect(brect, Color.Red, 2);

            _spriteBatch.DrawLine(tl1, tl2, Color.Red);
            _spriteBatch.DrawLine(tr1, tr2, Color.Red);
            _spriteBatch.DrawLine(bl1, bl2, Color.Red);
            _spriteBatch.DrawLine(br1, br2, Color.Red);

            _spriteBatch.DrawRect(_box, Color.Red, 2);
        }

        private void DrawPoint(Vector2 p, Color col, float len = 3)
        {
            _spriteBatch.DrawLine(p - new Vector2(len, len), p + new Vector2(len, len), col);
            _spriteBatch.DrawLine(p + new Vector2(-len, len), p - new Vector2(-len, len), col);
        }

    }
}
