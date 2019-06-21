using System.Collections.Generic;
using AutoMapper;

namespace DataLayer.Tools
{
    public static class Mapper
    {
        public static TTo Map<TTo, TFrom>(TFrom entity)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>()).CreateMapper();
            return mapper.Map<TFrom, TTo>(entity);
        }

        public static IEnumerable<TTo> MapEnumerable<TTo, TFrom>(IEnumerable<TFrom> entity)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TFrom, TTo>()).CreateMapper();
            return mapper.Map<IEnumerable<TFrom>, IEnumerable<TTo>>(entity);
        }
    }
}