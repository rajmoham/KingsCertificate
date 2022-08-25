using System;
using System.Collections.Generic;

namespace KC.Models
{
    public class WebsiteProblem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Explanation { get; set; }
        public int Status { get; set; }

        /*
         * Status codes are:
         * 
         * 0 - no action required
         * 1 - fixed
         * 2 - in progress
         * 3 - awaiting action
         */

        public static Dictionary<int, string> statusMessages = new Dictionary<int, string>()
        {
            {0, "No action required" },
            {1, "Fixed" },
            {2, "In progress" },
            {3, "Awaiting action" },
        };

        public static Dictionary<int, string> statusBackgroundColours = new Dictionary<int, string>()
        {
            {0, "table-light" },
            {1, "table-success" },
            {2, "table-info" },
            {3, "table-warning" },
        };
    }
}
