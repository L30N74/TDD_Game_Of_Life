using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using GameOfLife;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfLife.App;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameOfLifeGame gameOfLifeGame;
    private Texture2D _texture;
    private readonly int BOARD_SIZE = 50;
    private readonly int CELL_SIZE = 20;
    private int xOffset;
    private int yOffset;
    private bool isPaused = true;
    private SpriteFont font;

    private Rectangle[,] cells;

    private Cell[,] nextState;
    private MouseState oldMouseState;
    private KeyboardState oldKeyboardState;

    private int[,] initState = new[,] {
        {0, 1, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 1, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 1, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
    };

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        var edgePadding = 100;
        _graphics.PreferredBackBufferWidth = BOARD_SIZE * (CELL_SIZE + 1) + edgePadding;
        _graphics.PreferredBackBufferHeight = BOARD_SIZE * (CELL_SIZE + 1) + 2 * edgePadding;
    }

    protected override void Initialize()
    {
        _texture = new Texture2D(_graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        _texture.SetData(new[] { Color.White });

        gameOfLifeGame = new GameOfLifeGame(BOARD_SIZE);

        InitializeBoard();

        this.nextState = gameOfLifeGame._state;

        IsFixedTimeStep = true;
        TargetElapsedTime = System.TimeSpan.FromSeconds(1d / 15);

        base.Initialize();
    }


    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        font = Content.Load<SpriteFont>("Fonts/MyFont");
    }

    protected override void Update(GameTime gameTime)
    {
        if (!isPaused)
        {
            this.nextState = gameOfLifeGame.NextIteration();
            gameOfLifeGame.SetState(nextState);
        }

        CheckKeyboardInput();

        CheckMouseInput();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.DrawString(font, isPaused ? "Paused" : "", new System.Numerics.Vector2(10, 10), Color.Black);

        DrawCellBoard(this.nextState);

        _spriteBatch.End();
        base.Draw(gameTime);
    }

    private void DrawCellBoard(Cell[,] state)
    {
        for (int xIndex = 0; xIndex < BOARD_SIZE; xIndex++)
        {
            for (int yIndex = 0; yIndex < BOARD_SIZE; yIndex++)
            {
                var cell = state[yIndex, xIndex];
                var color = cell.isAlive ? Color.Black : Color.White;
                _spriteBatch.Draw(_texture, cells[xIndex, yIndex], color);
            }
        }

    }

    private void CheckKeyboardInput()
    {
        var keyboardState = Keyboard.GetState();

        // Hard Reset
        if (keyboardState.IsKeyDown(Keys.R) && !oldKeyboardState.IsKeyDown(Keys.R))
        {
            gameOfLifeGame = new GameOfLifeGame(BOARD_SIZE);
        }

        // Pause
        if (keyboardState.IsKeyDown(Keys.P) && !oldKeyboardState.IsKeyDown(Keys.P))
        {
            this.isPaused = !this.isPaused;
        }

        oldKeyboardState = keyboardState;
    }

    private void CheckMouseInput()
    {
        var mouseState = Mouse.GetState();

        if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
        {
            this.nextState = gameOfLifeGame.NextIteration();
            gameOfLifeGame.SetState(nextState);
        }

        if (!this.isPaused) return;

        if (mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
        {
            // Check if mouse position inside a cell
            for (var x = 0; x < BOARD_SIZE; x++)
            {
                for (var y = 0; y < BOARD_SIZE; y++)
                {
                    // If mouse is clicked inside a cell, switch that cell's state
                    var mouseInCell = cells[x, y].Contains(mouseState.X, mouseState.Y);

                    if (mouseInCell)
                    {
                        var clickedCell = gameOfLifeGame._state[x, y];
                        gameOfLifeGame.SetCellState(y, x, !clickedCell.isAlive);
                    }
                }
            }
        }


        oldMouseState = mouseState;
    }

    private void InitializeBoard()
    {
        var halfWidth = _graphics.GraphicsDevice.Viewport.Width / 2;
        var halfHeight = _graphics.GraphicsDevice.Viewport.Height / 2;

        var cellPadding = 1;
        xOffset = halfWidth - (BOARD_SIZE / 2) * (CELL_SIZE + cellPadding);
        yOffset = halfHeight - (BOARD_SIZE / 2) * (CELL_SIZE + cellPadding) - 50;

        cells = new Rectangle[BOARD_SIZE, BOARD_SIZE];
        for (var x = 0; x < BOARD_SIZE; x++)
        {
            for (var y = 0; y < BOARD_SIZE; y++)
            {
                var xPos = x * (CELL_SIZE + 1) + xOffset;
                var yPos = y * (CELL_SIZE + 1) + yOffset;

                cells[x, y] = new Rectangle(xPos, yPos, CELL_SIZE, CELL_SIZE);
            }
        }
    }
}
