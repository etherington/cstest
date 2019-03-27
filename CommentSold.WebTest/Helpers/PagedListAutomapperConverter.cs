using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CommentSold.WebTest.Services;

namespace CommentSold.WebTest.Helpers
{
    public class PagedListAutomapperConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);

            return new PagedList<TDestination>(collection.ToList(), source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}
