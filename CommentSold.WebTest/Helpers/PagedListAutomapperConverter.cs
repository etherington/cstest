using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace CommentSold.WebTest.Helpers
{
    /// <summary>
    /// Provides Automapper with a converter between PagedList<TSource> and PagedList<TDestination>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public class PagedListAutomapperConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source);
            return new PagedList<TDestination>(collection.ToList(), source.TotalCount, source.CurrentPage, source.PageSize);
        }
    }
}
