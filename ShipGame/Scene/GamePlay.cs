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
        private Vector2 startPlayerPosi = new Vector2(32 * 2, 32 * 12);
        private Vector2 startOrigin = new Vector2(16, 16);
        private float startPlayerRota = 0;
        private Vector2 startBermudaPosi = new Vector2(600, 600);

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
            renderer.End();
        }


        public void Initialize()
        {
            IsEndFlag = false;
            gameObjectManager.Initialize();

            //プレイヤーの生成
            player = new Player(startPlayerPosi, startPlayerRota,startOrigin, gameDevice, gameObjectManager);

            ship = new Ship(new Vector2(), 0.0f, new Vector2(), gameDevice, gameObjectManager,3);

            bermuda = new Bermuda(new Vector2(), new Vector2(),gameDevice, gameObjectManager);

            island = new Island(new Vector2(), new Vector2(), gameDevice, gameObjectManager);

            //プレイヤーにIDを設定
            gameObjectManager.Add(player);
            gameObjectManager.Add(ship);
            gameObjectManager.Add(bermuda);
            gameObjectManager.Add(island);

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
            if (Input.GetKeyTrigger(Keys.Space))
            {
                IsEndFlag = true;
            }
        }
    }
}