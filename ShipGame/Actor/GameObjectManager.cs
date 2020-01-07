using Microsoft.Xna.Framework;
using ShipGame.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipGame.Actor
{
    class GameObjectManager : IGameObjectMediator
    {
        private List<GameObject> gameObjectList;//プレイヤーグループ
        private List<GameObject> addGameObjects;//追加するキャラクターリスト
        
        //リスポーン位置
        private Vector2 respawnPos;

        public Vector2 RespawnPos
        {
            get { return respawnPos; }
            set { respawnPos = value; }
        }

        public GameObjectManager()
        {
            Initialize();
        }

        public void Initialize()
        {
            //リストの実体があればクリアしなければ実体生成
            if (gameObjectList != null)
            {
                gameObjectList.Clear();
            }
            else
            {
                gameObjectList = new List<GameObject>();
            }

            if (addGameObjects != null)
            {
                addGameObjects.Clear();
            }
            else
            {
                addGameObjects = new List<GameObject>();
            }
        }

        public void Add(GameObject gameObject)
        {
            if (gameObjectList == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }
        
        private void HitToGameObject()
        {
            //ゲームオブジェクトリストを繰り返し
            foreach (var c1 in gameObjectList)
            {
                //同じゲームオブジェクトリストを繰り返し
                foreach (var c2 in gameObjectList)
                {
                    if (c1.Equals(c2) || c1.IsDead() || c2.IsDead())
                    {
                        //同じキャラか、キャラが死んでたら次へ
                        continue;
                    }

                    //衝突判定
                    if (c1.IsCollision(c2))
                    {
                        //ヒット通知
                        c1.Hit(c2);
                        c2.Hit(c1);
                    }
                }
            }
        }

        private void RemoveDeadcharacter()
        {
            gameObjectList.RemoveAll(c => c.IsDead());
        }

        public void Update(GameTime gameTime)
        {
            //全キャラ更新
            foreach (var c in gameObjectList)
            {
                c.Update(gameTime);
            }

            //キャラクタの追加
            foreach (var c in addGameObjects)
            {
                gameObjectList.Add(c);
            }

            //追加終了後、追加リストはクリア
            addGameObjects.Clear();

            //当たり判定
            
            HitToGameObject();

            //死亡フラグがたっているキャラをすべて削除
            RemoveDeadcharacter();
        }

        public void Draw(Renderer renderer)
        {
            foreach (var c in gameObjectList)
            {
                c.Draw(renderer);
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return;
            }
            addGameObjects.Add(gameObject);
        }

        public GameObject GetPlayer()
        {
            GameObject find = gameObjectList.Find(c => c is Player);
            //Playerが存在していて死んでないとき
            if (find != null && !find.IsDead())
            {
                return find;
            }
            return null;//プレイヤーがいない
        }

        public bool IsPlayerDead()
        {
            GameObject find = gameObjectList.Find(c => c is Player);

            return (find == null || find.IsDead());
        }


        public GameObject GetGameObject(GameObjectID id)
        {
            //引数のidがリストのGameObjectにあるか探す
            GameObject find = gameObjectList.Find(c => c.GetID() == id);

            //発見したオブジェクトがnullでない時かつ、死んでないとき
            if (find != null && !find.IsDead())
            {
                return find;
            }
            return null;
        }


        public List<GameObject> GetGameObjectList(GameObjectID id)
        {
            //引数で指定されたオブジェクトを複数探す
            List<GameObject> list = gameObjectList.FindAll(c => c.GetID() == id);
            //発見したオブジェクトが生きているか確認
            List<GameObject> aliveList = new List<GameObject>();//生存リスト
            foreach (var c in list)
            {
                //生きていたらリストに追加
                if (!c.IsDead())
                {
                    aliveList.Add(c);
                }
            }
            //生存リストを返す
            return aliveList;
        }

      
        /// <summary>
        /// リスポーン位置をセット
        /// </summary>
        /// <param name="respawnPos">セットする位置</param>
        public void SetRespawnPos(Vector2 respawnPos)
        {
            RespawnPos = respawnPos;
        }
        
    }
}
