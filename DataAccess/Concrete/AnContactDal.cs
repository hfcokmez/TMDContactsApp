using AutoMapper;
using Core.DataAccess.AdoNet;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete
{
    public class AnContactDal : AnEntityRepositoryBase<Contact>, EContactDal
    {
    }
}
