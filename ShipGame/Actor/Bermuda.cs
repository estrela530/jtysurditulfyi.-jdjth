using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShipGame.Util;
using ShipGame.Device;

namespace ShipGame.Actor
{
    class Bermuda:GameObject
    {
        private IGameObjectMediator mediator;
        int a;
        public Bermuda(Vector2 position, Vector2 origin,GameDevice gameDevice,
            IGameObjectMediator mediator)
            :base("blue",position ,0,origin,32,1280,gameDevice)
        {
            this.mediator = mediator;
            this.position = new Vector2(1900, 0);
        }

        /// <summary>
        /// コピーコンストラクタ
        /// </summary>
        /// <param name="other"></param>
        public Bermuda(Bermuda other)
            :this(other.position,other.origin, other.gameDevice, other.mediator)
        {

        }

        public override object Clone()
        {
            return new Bermuda(this); 
        }

        /// <summary>
        /// ヒット通知
        /// </summary>
        /// <param name="gameObject"></param>
        public override void Hit(GameObject gameObject)
        {
            Direction dir = this.CheckDirection(gameObject);

            if(gameObject is Player)
            {
                // isDeadFlag = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(Renderer renderer)
        {
            renderer.DrawTexture(
                name,
                position,
                new Vector2(1,20)
                );
        }
    }
}
