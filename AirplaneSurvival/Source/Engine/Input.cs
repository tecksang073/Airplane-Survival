using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AirplaneSurvival.Source.Engine
{
    /*
     * A reusable static class for handling input in MonoGame projects.
     */

    public static class Input
    {
        private static MouseState? lastMouseState;
        private static MouseState mouseState;

        private static KeyboardState? lastKeyboardState;
        private static KeyboardState keyboardState;

        public static Texture2D CursorTex { get; private set; }

        static Game game;

        public static void LoadData(Game _game)
        {
            game = _game;
        }

        public static void Update()
        {
            // Assigns the previous and current mouse state to the corresponding variables.

            lastMouseState = mouseState;
            mouseState = Mouse.GetState();

            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        #region Mouse Functions

        public static Point MousePosition => mouseState.Position;

        public static Vector2 MousePositionMatrix(Matrix matrix)
        {
            return Vector2.Transform(MousePosition.ToVector2(),
                                     Matrix.Invert(matrix));
        }

        public static int MouseX => mouseState.X;
        public static int MouseY => mouseState.Y;

        static MouseState GetLastMouseState => lastMouseState ?? mouseState;
        public static Point LastMousePosition => GetLastMouseState.Position;
        public static int LastMouseX => LastMousePosition.X;
        public static int LastMouseY => LastMousePosition.Y;

        public static bool LeftMouseDown()
        {
            return game.IsActive && mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftMouseUp()
        {
            return !game.IsActive || mouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftMousePressed()
        {
            return game.IsActive && mouseState.LeftButton == ButtonState.Pressed && GetLastMouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftMouseReleased()
        {
            return !game.IsActive || mouseState.LeftButton == ButtonState.Released && GetLastMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool MiddleMouseDown()
        {
            return game.IsActive && mouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool MiddleMouseUp()
        {
            return !game.IsActive || mouseState.MiddleButton == ButtonState.Released;
        }

        public static bool MiddleMousePressed()
        {
            return game.IsActive && mouseState.MiddleButton == ButtonState.Pressed && GetLastMouseState.MiddleButton == ButtonState.Released;
        }

        public static bool MiddleMouseReleased()
        {
            return !game.IsActive || mouseState.MiddleButton == ButtonState.Released && GetLastMouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool RightMouseDown()
        {
            return game.IsActive && mouseState.RightButton == ButtonState.Pressed;
        }

        public static bool RightMouseUp()
        {
            return !game.IsActive || mouseState.RightButton == ButtonState.Released;
        }

        public static bool RightMousePressed()
        {
            return game.IsActive && mouseState.RightButton == ButtonState.Pressed && GetLastMouseState.RightButton == ButtonState.Released;
        }

        public static bool RightMouseReleased()
        {
            return !game.IsActive || mouseState.RightButton == ButtonState.Released && GetLastMouseState.RightButton == ButtonState.Pressed;
        }

        public static bool InRectBounds(Rectangle rect)
        {
            return game.IsActive && MouseX >= rect.Left && MouseX < rect.Right &&
                                    MouseY >= rect.Top && MouseY < rect.Bottom;
        }

        public static bool InRectBoundsMatrix(Rectangle rect, Matrix matrix)
        {
            var p = MousePositionMatrix(matrix);

            return game.IsActive && p.X >= rect.Left && p.X < rect.Right &&
                                    p.Y >= rect.Top && p.Y < rect.Bottom;
        }

        static readonly Point cursorSize = new Point(16);

        public static void DrawCursor(SpriteBatch spriteBatch)
        {
            if (CursorTex == null) CursorTex = game.Content.Load<Texture2D>("Cursor");
            spriteBatch.Draw(CursorTex, new Rectangle(MousePosition, cursorSize), Color.White);
        }

        public static int GetScrollDelta()
        {
            return game.IsActive ? (mouseState.ScrollWheelValue - GetLastMouseState.ScrollWheelValue) : 0;
        }

        #endregion

        #region Keyboard Functions

        static KeyboardState GetLastKeyboardState => lastKeyboardState ?? keyboardState;

        public static bool KeyDown(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool KeyUp(Keys key)
        {
            return keyboardState.IsKeyUp(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) && GetLastKeyboardState.IsKeyUp(key);
        }

        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) && GetLastKeyboardState.IsKeyDown(key);
        }

        public static Keys[] AllPressedKeys()
        {
            return keyboardState.GetPressedKeys();
        }

        #endregion
    }
}
