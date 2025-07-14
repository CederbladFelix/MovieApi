﻿namespace Movies.Core.Models.DTOs
{
    public class PaginationOptions
    {
        private const int MaxPageSize = 100;
        private int pageSize = 10;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
