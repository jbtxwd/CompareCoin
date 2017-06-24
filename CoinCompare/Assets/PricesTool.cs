using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace cptool
{
    public class PricesTool : MonoBehaviour
    {
        //Dictionary<string, IPrice> prices = new Dictionary<string, IPrice>();
        public List<IPrice> prices = new List<IPrice>();
        public List<Coin19800> pricess = new List<Coin19800>();
        Dictionary<string, int> cnt = new Dictionary<string, int>();

        public void Awake()
        {
            Coin19800 coin19800 = new Coin19800();
            coin19800.Init();
        }
        public void Init()
        {
        }
    }
}
