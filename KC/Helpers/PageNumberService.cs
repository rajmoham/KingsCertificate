using System;
using System.Collections.Generic;
using System.Linq;

namespace KC.Helpers
{
    public static class PageNumberService
    {
        public static List<int> GetPageNumbers(int pageNumber, int totalPages)
        {
            List<int> pageNumbers = new List<int>() { pageNumber - 2, pageNumber - 1, pageNumber, pageNumber + 1, pageNumber + 2 };

            if (pageNumbers.Where(p => p > 0 && p <= totalPages).Count() == 5)
                return pageNumbers;

            if (totalPages <= 5)
                return Enumerable.Range(1, totalPages <= 5 ? totalPages : 5).ToList();

            if (pageNumber == 1)
                return Enumerable.Range(1, totalPages <= 5 ? totalPages : 5).ToList();

            if (pageNumber == 2)
                return Enumerable.Range(1, totalPages <= 5 ? totalPages : 5).ToList();

            if (totalPages - pageNumber == 1)
                return Enumerable.Range(pageNumber - 3, totalPages <= 5 ? totalPages : 5).ToList();

            if (totalPages == pageNumber)
                return Enumerable.Range(pageNumber - 4, totalPages <= 5 ? totalPages : 5).ToList();

            return pageNumbers;
        }
    }
}
