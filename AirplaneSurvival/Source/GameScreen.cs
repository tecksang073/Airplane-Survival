using AirplaneSurvival.Source.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AirplaneSurvival.Source
{
    public class GameScreen : Component
    {
        static readonly SpriteFont scoreFont;

        static GameScreen()
        {
            // Loads the font for the score text.
            scoreFont = Main.MainGame.Content.Load<SpriteFont>("Fonts/ScoreFont");
        }

        readonly World world;

        int score;
        int highScore;

        public void IncreaseScore(int amount)
        {
            // Increases the score by the specified parameter.
            score += amount;

            if (score > highScore)
            {
                // Sets the high score if the score is greater than it.
                highScore = score;
            }
        }

        GameOverScreen gameOver;

        public void GameOver()
        {
            // Saves the high score to memory so that it can be retrieved the next time the game runs.
            if (highScore > Properties.Settings.Default.HighScore)
            {
                Properties.Settings.Default.HighScore = highScore;
                Properties.Settings.Default.Save();
            }

            gameOver = new GameOverScreen(this);
        }

        public GameScreen()
        {
            world = new World(this);

            scoreIncrease = new CountdownTimer(100f);

            // Assigns the high score from memory.
            highScore = Properties.Settings.Default.HighScore;
        }

        public void Reset()
        {
            // Resets the world, score count, and closes the game over screen.
            if (gameOver != null)
            {
                gameOver.Dispose();
                gameOver = null;
            }

            world.Reset();
            scoreIncrease.Reset();

            score = 0;
        }

        CountdownTimer scoreIncrease;

        public override void Update(GameTime gameTime)
        {
            // Updates the world.
            world.Update(gameTime);

            if (gameOver == null)
            {
                // Adds to the score over time if the game hasn't ended.
                scoreIncrease.Decrement(gameTime.Multiplier());

                if (scoreIncrease.IfEndedReset())
                {
                    IncreaseScore(1);
                }
            }
            
            // Update the game over screen if it exists.
            gameOver?.Update(gameTime);
        }

        public override void Show(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Displays the world.
            world.Show(gameTime, spriteBatch);

            // Displays the score and high score.
            spriteBatch.Begin();
            spriteBatch.DrawString(scoreFont, $"Score: {score}{Environment.NewLine}High Score: {highScore}", new Vector2(10, 10), Color.Black);
            spriteBatch.End();

            // Displays the game over screen if it exists.
            gameOver?.Show(gameTime, spriteBatch);
        }
    }
}
