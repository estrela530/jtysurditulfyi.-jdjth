using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ShipGame.Device;

namespace ShipGame.Actor
{
    enum Direction
    {
        Top, Bottom, Left, Right
    };

    abstract class GameObject:ICloneable
    {
        protected string name;//アセット名
        // 速度
        protected Vector2 velocity;
        protected Vector2 position;//位置
        protected Vector2 origin;//回転軸の位置
        protected int width;//幅
        protected int height;//高さ
        protected float rotation;
        protected bool isDeadFlag;//死亡フラグ
        protected GameDevice gameDevice;//ゲームデバイス
        protected GameObjectID id =
            GameObjectID.NONE;//個別に見分ける（デフォルト値は識別無し）

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="gameDevice">ゲームデバイス</param>
        public GameObject(string name, Vector2 position,float rotation,Vector2 origin ,int width,
            int height, GameDevice gameDevice)
        {
            this.name = name;
            this.position = position;
            this.width = width;
            this.height = height;
            this.gameDevice = gameDevice;
            this.rotation = rotation;//
            this.origin = origin;
        }

        //抽象メソッド
        public abstract object Clone();//ICloneableで必ず必要
        public abstract void Update(GameTime gameTime);//更新
        public abstract void Hit(GameObject gameObject);//ヒット通知

        //仮想メソッド
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">描画オブジェクト</param>
        public virtual void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position /* +gameDevice.GetDisplayModify()*/);
        }

        public virtual void Initialize()
        {

        }

        /// <summary>
        /// 死んでいるか
        /// </summary>
        /// <returns>死んでいたらtrue</returns>
        public bool IsDead()
        {
            return isDeadFlag;
        }

        /// <summary>
        /// 位置の設定
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

        /// <summary>
        /// 位置の取得
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return position;
        }

        /// <summary>
        /// オブジェクト幅の取得
        /// </summary>
        /// <returns></returns>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// オブジェクトの高さの取得
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            return height;
        }

        /// <summary>
        /// 当たり判定用、矩形情報の取得
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            //矩形の生成
            Rectangle area = new Rectangle();

            //位置と幅、高さを設定
            area.X = (int)position.X;
            area.Y = (int)position.Y;
            area.Height = height;
            area.Width = width;

            return area;
        }

        /// <summary>
        /// 矩形同士の当たり判定
        /// </summary>
        /// <param name="otherObj"></param>
        /// <returns></returns>
        public bool IsCollision(GameObject otherObj)
        {
            //RectangleクラスのIntersectsメソッドで重なり判定
            return this.GetRectangle().Intersects(otherObj.GetRectangle());
        }

        public Direction CheckDirection(GameObject otherObj)
        {
            //中心位置の取得
            Point thisCenter = this.GetRectangle().Center;//自分の中心位置
            Point otherCenter = otherObj.GetRectangle().Center;//相手の中心位置

            //向きのベクトルを取得
            Vector2 dir = new Vector2(thisCenter.X, thisCenter.Y) -
                new Vector2(otherCenter.X, otherCenter.Y);
            //当たっている側面をリターンする
            //x成分とy成分でどちらの方が量が多いか
            if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            {
                //xの向きが正しいとき
                if (dir.X > 0)
                {
                    return Direction.Right;
                }
                return Direction.Left;
            }


            //y成分が大きく正の値か
            if (dir.Y > 0)
            {
                return Direction.Bottom;
            }
            //プレイヤーがブロックに乗った
            return Direction.Top;
        }

        public virtual void CorrectPosition(GameObject other)
        {
            //当たった面の取得
            Direction dir = this.CheckDirection(other);

            //相手のブロックと位置補正
            if (dir == Direction.Top)
            {
                position.Y = other.GetRectangle().Top - this.height;
            }
            else if (dir == Direction.Right)
            {
                position.X = other.GetRectangle().Right;
            }
            else if (dir == Direction.Left)
            {
                position.X = other.GetRectangle().Left - this.width;
            }
            else if (dir == Direction.Bottom)
            {
                position.Y = other.GetRectangle().Bottom;
            }
        }

        /// <summary>
        /// 識別用IDの取得
        /// </summary>
        /// <returns></returns>
        public GameObjectID GetID()
        {
            return id;
        }

        public void SetID(GameObjectID id)
        {
            this.id = id;
        }
    }
}
