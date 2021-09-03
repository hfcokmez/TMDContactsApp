using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class PaginationDto<T>: IDto
    {
        public PaginationDto()
        {
            FirstPage = 1;
        }
        public List<T> Data { get; set; }
        public int PreviousPage { get; set; }
        public int CurrentPage { get; set; }
        public int NextPage { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
