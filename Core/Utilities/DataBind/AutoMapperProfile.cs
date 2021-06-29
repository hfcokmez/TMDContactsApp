using AutoMapper;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Core.Utilities.DataBind
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap< Contact,DataSet > ();
            CreateMap<SqlDataReader, User>();
        }
    }
}
