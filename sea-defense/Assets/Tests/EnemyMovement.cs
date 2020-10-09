using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;

namespace Tests
{
    public class NewTestScript
    {
        [UnityTest]
        public IEnumerator TestShipSpawning()
        {
            GameObject ship = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Tests/EnemyShip"));
            yield return new WaitForSeconds(2f);
            Assert.IsNotNull(ship);
        }
    }
}
