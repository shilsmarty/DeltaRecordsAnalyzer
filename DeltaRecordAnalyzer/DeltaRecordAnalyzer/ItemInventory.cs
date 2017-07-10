using System;

namespace DeltaRecordAnalyzer
{
    public class ItemInventory : IEquatable<ItemInventory>
    {

        public string ItemCode { get; set; }

        public string ItemName { get; set; }

        public int ItemCost { get; set; }

        public bool Equals(ItemInventory other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the properties are equal.
            return ItemCode.Equals(other.ItemCode) && ItemName.Equals(other.ItemName) && ItemCost.Equals(other.ItemCost);
        }

        public override int GetHashCode()
        {

            int hashItemCode = ItemCode.GetHashCode();

            int hashItemName = ItemName?.GetHashCode() ?? 0;

            int hashItemCost = ItemCost.GetHashCode();

            //Calculate the hash code.
            return (hashItemCode + hashItemName) ^ (hashItemCost);
        }
    }
}
