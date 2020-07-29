using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace GameBox.Admin.UI
{
    public static class NavigationManagerExtensions
    {
        public static string GetQueryParam(this NavigationManager router, string key)
        {
            var query = new Uri(router.Uri).Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue(key, out var value))
            {
                return value;
            }

            return string.Empty;
        }
    }
}