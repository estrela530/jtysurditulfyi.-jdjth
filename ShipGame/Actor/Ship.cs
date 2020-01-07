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
    class Ship : GameObject
    {
        private float speed = 6.0f;
        private IGameObjectMediator mediator;//ゲームオブジェクト仲介者
        public Ship(Vector2 position, float rotation, Vector2 origin, GameDevice gameDevice,
            IGameObjectMediator mediator,int score)
            : base("green", position, rotation, origin, 32, 32, gameDevice)
        {
            isDeadFlag = false;
            this.mediator = mediator;
            this.origin = new Vector2(16, 16);
            this.score = score;
            isRide = true;
        }

        public override object Clone()
        {
            //自分と同じ型のオブジェクトを自分の情報で生成
            return new Ship(this);
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="other"></param>
        public Ship(Ship other)
            : this(other.position, other.rotation, other.origin, other.gameDevice, other.mediator,0)
        {

        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Hit(GameObject gameObject)
        {
            if (gameObject is Player)
            {
                isRide = false;
            }
            if (gameObject is Bermuda)
            {
                isDeadFlag = true;
            }
        }
        public void ShipMove()
        {
            if (isRide)
            {
                position.X += speed/3f;
            }
            else
            {
                position.X += speed;
            }
        }

        public override void Update(GameTime gameTime)
        {
            ShipMove();
        }
    }
}
