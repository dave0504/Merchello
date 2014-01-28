﻿using Merchello.Core.Models;

namespace Merchello.Core.Gateways.Shipping.RateTable
{
    /// <summary>
    /// Vistor class that calculates 
    /// </summary>
    public class RateTableShipMethodShipmentLineItemVisitor : ILineItemVisitor
    {
        public RateTableShipMethodShipmentLineItemVisitor()
        {
            TotalPrice = 0M;
            TotalWeight = 0M;
            UseOnSalePriceIfOnSale = false;
        }

        public void Visit(ILineItem lineItem)
        {
            if (!lineItem.ExtendedData.DefinesProductVariant()) return;

            // adjust the total weight
            TotalWeight += lineItem.ExtendedData.GetWeightValue() * lineItem.Quantity;

            // adjust the total price
            if(UseOnSalePriceIfOnSale)
            {
                TotalPrice += lineItem.ExtendedData.GetOnSaleValue()
                    ? lineItem.ExtendedData.GetSalePriceValue()
                    : lineItem.ExtendedData.GetPriceValue();
            }
            else
            {
                TotalPrice += lineItem.ExtendedData.GetPriceValue() * lineItem.Quantity;
            }
        }

        /// <summary>
        /// Returns the TotalWeight from ExtendedData
        /// </summary>
        public decimal TotalWeight { get; private set; }

        /// <summary>
        /// Returns the TotalPrice form ExtendedData
        /// </summary>
        public decimal TotalPrice { get; private set; }

        /// <summary>
        /// True/false indicating whether or not to use the OnSale price in the total price calculation
        /// </summary>
        public bool UseOnSalePriceIfOnSale
        {
            get; set;
        }
    }
}