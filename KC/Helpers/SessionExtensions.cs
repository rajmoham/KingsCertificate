using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using KC.Models;
using Microsoft.AspNetCore.Http;

namespace KC.Helpers
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static int GetQuantityInCart(this ISession session, string productId)
        {
            List<ShoppingCartItem> cartList = new List<ShoppingCartItem>();
            if (session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart) != null
                && session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart).Count() > 0)
            {
                cartList = session.Get<List<ShoppingCartItem>>(Constants.SessionKeys.Cart);
            }

            ShoppingCartItem matchingItem = cartList.Where(item => item.Product.productId == productId).FirstOrDefault();

            if (matchingItem != null)
                return matchingItem.Quantity;
            return 0;
        }
    }
}
