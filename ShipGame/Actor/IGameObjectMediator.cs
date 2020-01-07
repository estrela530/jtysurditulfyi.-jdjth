using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ShipGame.Actor
{
    /// <summary>
    /// ゲームオブジェクト仲介者インターフェース
    /// </summary>
    interface IGameObjectMediator
    {
        //ゲームオブジェクト追加
        void AddGameObject(GameObject gameObject);

        //プレイヤーを取得
        GameObject GetPlayer();

        //プレイヤーが死んでるかどうか
        bool IsPlayerDead();

        //特定のオブジェクトを取得する
        GameObject GetGameObject(GameObjectID id);
        //List<GameObject>GetGameObject(GameObjectID id);//戻り値だけ違うメソッドは定義できない

        //複数のゲームオブジェクトの取得
        //List<GameObject> GetGameObjectList(GameObjectID id);
        
        //リスポーン位置をセット
        void SetRespawnPos(Vector2 respawnPos);
    }
}
