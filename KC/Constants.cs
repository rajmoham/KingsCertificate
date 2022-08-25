using System;
using System.Collections.Generic;
using KC.Models;

namespace KC
{
    public static class Constants
    {
        public static class DisplayOptions
        {
            public static int ItemsPerPage = 33;
        }

        public static class SearchOptions
        {
            public static class PriceAscending
            {
                public const string key = "asc";
                public const string name = "Price (ascending)";
            }
            public static class PriceDescending
            {
                public const string key = "des";
                public const string name = "Price (descending)";
            }
            public static class BrandName
            {
                public const string key = "bn";
                public const string name = "Brand name";
            }
            public static class DisplayName
            {
                public const string key = "dn";
                public const string name = "Product name";
            }
        }

        public static class Spelling
        {
            public const string Endpoint = "https://api.bing.microsoft.com/v7.0/spellcheck?text=";
        }

        public static class SessionKeys
        {
            public const string Cart = "CartSession";
        }

        public static class DisplayContent
        {
            public static class HomePage
            {
                public static string CarouselSlideMessage1 = "Fresh food delivered straight to your doorstep";
                public static string CarouselSlideMessage2 = "Can't get to the shops? Not to worry - we've got you covered";
                public static string CarouselSlideMessage3 = "Over 3000 products to choose from, so variety is never a problem";
            }
        }

        public static class NotificationKeys
        {
            public const string SuccessKey = "NOTIF_SUCCESS";
            public const string ErrorKey = "NOTIF_ERROR";
        }

        public static List<WebsiteProblem> WebsiteProblems = new List<WebsiteProblem>()
        {
            new WebsiteProblem()
            {
                Title = "Filtering",
                Description = "'Update search' does not update anything.",
                Status = 0,
                Explanation = "Implementing these filters is beyond the role of a front-end developer. Unlike other backend parts of this website, we have decided not to code this functionality."
            },
            new WebsiteProblem()
            {
                Title = "Quantities",
                Description = "You can have negative quantities.",
                Status = 1,
                Explanation = ""
            },
            new WebsiteProblem()
            {
                Title = "Suggested mismatch",
                Description = "Suggested products are randomly generated",
                Status = 0,
                Explanation = "Suggesting products based on user behaviour requires some form of artificial intelligence. We do not have the facilities nor the need to implement this in our prototype - the design itself has been completed, however."
            },
            new WebsiteProblem()
            {
                Title = "Shipping & Returns",
                Description = "There is no 'Shipping & Returns page",
                Status = 0,
                Explanation = "This would be part of the Terms and Conditions page."
            }
        };
    }
}
