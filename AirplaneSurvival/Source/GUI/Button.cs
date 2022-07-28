using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AirplaneSurvival.Source.GUI
{
    public class Button : RectangularControl
    {
        public Button(ControlGroup manager, bool _pressRemain = false) : base(manager) { pressRemain = _pressRemain; }

        public Color buttonColour = Color.White,
                     hoverColour = Color.LightGray,
                     pressedColour = Color.DarkGray,
                     textColour = Color.Black,
                     disabledColour = Color.SlateGray;

        public Texture2D BackgroundImage { get; set; } = SpecialContent.Pixel;

        public SpriteFont Font { get; set; }

        public bool IsEnabled { get; set; } = true;

        public readonly bool pressRemain;
        public string Text { get; set; }

        State state = State.NORMAL;
        enum State
        {
            NORMAL,
            HOVER,
            PRESSED,
            REMAIN
        }

        public Action<object> ClickEvent { get; set; }
        public Action<object> ReleaseEvent { get; set; }

        public void PerformClick()
        {
            ClickEvent?.Invoke(this);
        }

        public void Release()
        {
            state = State.NORMAL;
        }

        protected override void UpdateControl(GameTime gameTime)
        {
            if (IsEnabled)
            {
                switch (state)
                {
                    default:
                        // Normal state.

                        if (Input.InRectBoundsMatrix(Rectangle, manager.Matrix))
                        {
                            // Sets to hover when mouse is over it.
                            state = State.HOVER;
                        }
                        break;
                    case State.HOVER:
                        // Hover state.

                        if (!Input.InRectBoundsMatrix(Rectangle, manager.Matrix))
                        {
                            // Sets to normal if mouse isn't over it.
                            state = State.NORMAL;
                        }
                        else if (Input.LeftMousePressed())
                        {
                            // Sets to pressed when mouse is pressed.
                            state = State.PRESSED;
                        }
                        break;
                    case State.PRESSED:
                        // Pressed state.

                        if (!Input.InRectBoundsMatrix(Rectangle, manager.Matrix))
                        {
                            // Sets to normal if mouse isn't over it.
                            state = State.NORMAL;
                        }
                        else if (Input.LeftMouseReleased())
                        {
                            // If the mouse is released, either set it to remain or normal depending on if it should remain pressed.
                            if (pressRemain)
                                state = State.REMAIN;
                            else
                                state = State.NORMAL;

                            // Click event is called.
                            PerformClick();
                        }
                        break;
                    case State.REMAIN:
                        // Remain state.

                        if (Input.InRectBoundsMatrix(Rectangle, manager.Matrix) && Input.LeftMouseReleased())
                        {
                            // Sets it back to normal when the left mouse is pressed.
                            state = State.NORMAL;

                            // Invokes the release event.
                            ReleaseEvent?.Invoke(this);
                        }

                        break;
                }
            }
            
        }

        protected override void ShowControl(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Sets the colour depending on the state of the button.
            Color c;

            if (IsEnabled)
            {
                switch (state)
                {
                    default:
                        c = buttonColour;
                        break;
                    case State.HOVER:
                        c = hoverColour;
                        break;
                    case State.PRESSED:
                    case State.REMAIN:
                        c = pressedColour;
                        break;
                }
            } else
            {
                c = disabledColour;
            }
            

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: manager.Matrix);

            // Draws the background.
            DrawBackground(spriteBatch, c);

            spriteBatch.End();

            // Draws the text if it isn't null and has a length greater than 0.
            if (Text != null && Text.Length > 0)
            {
                spriteBatch.Begin(transformMatrix: manager.Matrix);

                spriteBatch.DrawString(Font,
                                       Text,
                                       Rectangle.Center.ToVector2() - (Font.MeasureString(Text) / 2),
                                       textColour);

                spriteBatch.End();
            }
        }

        protected virtual void DrawBackground(SpriteBatch spriteBatch, Color c)
        {
            spriteBatch.Draw(BackgroundImage, Rectangle, c);
        }

        protected override void Dispose(bool disposing)
        {
            ClickEvent = null;
        }
    }
}
