using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShipGame.Actor;
using ShipGame.Device;

namespace ShipGame.Scene
{
    class GamePlay : IScene
    {
        private GameDevice gameDevice;
        private GameObjectManager gameObjectManager;
        private Renderer renderer;
        private Player player;
        private Ship ship;
        private Island island;
        private Bermuda bermuda;
        private bool IsEndFlag;
        private Vector2 startPlayerPosi = new Vector2(32 * 30, 32 * 12);
        private Vector2 startOrigin = new Vector2(16, 16);
        private float startPlayerRota = 0;
        private Vector2 startBermudaPosi = new Vector2(600, 600);
        private float score;

        int num;
        Random rnd = new Random();

        public GamePlay()
        {
            gameDevice = GameDevice.Instance();
            gameObjectManager = new GameObjectManager();
        }
        public void Draw(Renderer renderer)
        {
            renderer.Begin();


            renderer.DrawTexture("backColor", Vector2.Zero);
            gameObjectManager.Draw(renderer);
            if ((int)score / 1000 <= 9 && (int)score / 1000 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, score.ToString("f2"), 7);
            }
            if ((int)score / 100 <= 9 && (int)score / 100 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, score.ToString("f2"), 6);
            }
            if ((int)score / 10 <= 9 && (int)score / 10 >= 1)
            {
                renderer.DrawNumber("number", Vector2.Zero, score.ToString("f2"), 5);
            }
            renderer.DrawNumber("number", Vector2.Zero, score.ToString("f2"), 4);
            renderer.End();
        }


        public void Initialize()
        {
            IsEndFlag = false;
            gameObjectManager.Initialize();
            num = 0;

            //プレイヤーの生成
            player = new Player(startPlayerPosi, startPlayerRota, startOrigin, gameDevice, gameObjectManager);

            bermuda = new Bermuda(new Vector2(), new Vector2(), gameDevice, gameObjectManager);

            island = new Island(new Vector2(), new Vector2(), gameDevice, gameObjectManager);

            //プレイヤーにIDを設定
            gameObjectManager.Add(player);
            gameObjectManager.Add(bermuda);
            gameObjectManager.Add(island);
            score = 0;
        }

        public bool IsEnd()
        {
            return IsEndFlag;
        }

        public SceneName Next()
        {
            return SceneName.GameEnding;
        }

        public void Shutdown()
        {

        }

        public void Update(GameTime gameTime)
        {
            gameObjectManager.Update(gameTime);

            if (num == 0)
            {
                ship = new Ship(new Vector2(0,/* rnd.Next(0, */900), 0.0f, new Vector2(), gameDevice, gameObjectManager, rnd.Next(2, 4));
                gameObjectManager.Add(ship);

            }
            num++;
            if (num >= 200)
            {
                num = 0;
            }

            if (Input.GetKeyTrigger(Keys.Space))
            {
                IsEndFlag = true;
            }

            score = player.score;
        }
    }
}