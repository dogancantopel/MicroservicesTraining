﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Web.Models.Basket
{
    public class BasketViewModel
    {
        public BasketViewModel()
        {
            BasketItems = new List<BasketItemViewModel>();
        }
        public string UserId { get; set; }
        public string DiscountCode { get; set; }

        public int? DiscountRate { get; set; }
        private List<BasketItemViewModel> _basketItems { get; set; }
        public List<BasketItemViewModel> BasketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketItems.ForEach(m =>
                    {
                        var discountPrice = m.Price * ((decimal)DiscountRate.Value / 100);
                        m.AppliedDiscount(Math.Round(m.Price - discountPrice, 2));
                    });
                }
                return _basketItems;
            }
            set {_basketItems = value; }
        }
        public decimal TotalPrice { get => _basketItems.Sum(m => m.GetCurrentPrice); }

        public bool HasDiscount { get => !string.IsNullOrEmpty(DiscountCode) && DiscountRate.HasValue; }

        public void CancelDiscount()
        {
            DiscountCode = null;
            DiscountRate = null;
        }
        public void ApplyDiscount(string code,int rate)
        {
            DiscountCode = code;
            DiscountRate = rate;
        }
    }
}
