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
    class Island : GameObject
    {
        private IGameObjectMediator mediator;

        public Island(Vector2 position, Vector2 origin, GameDevice gameDevice,
            IGameObjectMediator mediator)
            :base("orage",position,0,origin,200,200,gameDevice)
        {
            this.mediator = mediator;
            this.position = new Vector2(600, 1000);
        }

        public Island(Island other)
            : this(other.position, other.origin, other.gameDevice, other.mediator)
        {

        }

        public override object Clone()
        {
            return new Island(this);
        }

        public override void Hit(GameObject gameObject)
        {
            if (gameObject is Player)
            {
                
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
                new Vector2(4,4)
                );
        }
    }
}
