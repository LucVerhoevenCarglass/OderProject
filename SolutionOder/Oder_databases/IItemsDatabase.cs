using System;
using System.Collections.Generic;
using System.Text;
using Order.Domain.Items;

namespace Order.Databases
{
    public interface IItemsDatabase
    {
        List<Item> GetDatabase();
        void InitDatabase();
        void ClearDatabase();
        void AddItem(Item newItem);
    }
}
