using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipGame.Device;
using ShipGame.Def;
using ShipGame.Util;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ShipGame.Actor
{
    class Player : GameObject
    {
        private Vector2 direction;
        private Vector2 stickDirection;
        private double stickAngle;
        private float speed = 4.0f;
        private float weight;
        private int score;
        private IGameObjectMediator mediator;//ゲームオブジェクト仲介者
        private double angle;
        private float flyingSpeed;

        private Vector2 upVec = new Vector2(0.0f, -1.0f);

        public Player(Vector2 position, float rotation, Vector2 origin, GameDevice gameDevice,
            IGameObjectMediator mediator)
            : base("red", position, rotation, origin, 32, 32, gameDevice)
        {
            velocity = Vector2.Zero;
            isDeadFlag = false;
            this.mediator = mediator;
            this.origin = new Vector2(16, 16);
            this.rotation = 0.0f;

            angle = Math.PI / 2;
            flyingSpeed = 0;

            weight = 0.0f;
            score = 0;
        }

        public override object Clone()
        {
            //自分と同じ型のオブジェクトを自分の情報で生成
            return new Player(this);
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="other"></param>
        public Player(Player other)
            : this(other.position, other.rotation, other.origin, other.gameDevice, other.mediator)
        {

        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position);
        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Hit(GameObject gameObject)
        {
            //当たった方向の取得
            Direction dir = this.CheckDirection(gameObject);

            //ゲームオブジェクトがばみゅだったら死ぬ
            if (gameObject is Bermuda)
            {
                isDeadFlag = true;
            }
            //島とあたったらweightが消える
            if (gameObject is Island)
            {
                isRide = false ;
            }
            //他船とあたったらweightが上がる
            if (gameObject is Ship)
            {
                score+= gameObject.GetScore();
                isRide = true;
            }
        }
        public void PlayerMove()
        {
            //velocity.X = Input.GetLeftStickground(PlayerIndex.One).X* speed;
            //if (Input.IsButtonPress(PlayerIndex.One,Buttons.B))
            //{
            //    velocity.Y = velocity.Y+ 1.1f; 
            //}

            //flyingSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y); //velocityの大きさをflyingSpeedに入れる。


            //velocity = Input.GetLeftStickladder(PlayerIndex.One) * speed;
            //position = position - velocity;


            // 角度計算
            stickDirection = Input.GetLeftSticksky(PlayerIndex.One);
            stickAngle = Math.Atan2(stickDirection.X, stickDirection.Y);
            if (stickDirection != Vector2.Zero)
            {
                direction = stickDirection;
                direction.Normalize();
                angle = stickAngle;
                Console.WriteLine("direction = " + direction);
                //Console.WriteLine("angle = " + angle);
            }

            flyingSpeed += (float)Math.Sin(angle);
            //flyingSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y); //velocityの大きさをflyingSpeedに入れる。

            velocity = flyingSpeed * direction;

            if (stickDirection != Vector2.Zero&&!isRide)
            {
                position += (velocity / 10);
            }
            else if (stickDirection != Vector2.Zero )
            {
                position += (velocity / 35);
            }
            else
            {
                direction.Normalize();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.IsButtonDown(PlayerIndex.One, Buttons.RightShoulder))
            {
                isRide = true;
            }
            if (Input.IsButtonDown(PlayerIndex.One, Buttons.LeftShoulder))
            {
                isRide = false;
            }
            PlayerMove();
        }
    }
}
