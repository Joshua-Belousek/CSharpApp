using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Libary.eCom.Models;

namespace Libary.eCom.Services
{
    public class InventoryServiceProxy
    {
        private InventoryServiceProxy() { }

        private List<Product?> list = new List<Product?>();


        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();
        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Product?> Products => list;
    }
}
