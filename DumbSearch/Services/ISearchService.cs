using System;
namespace DumbSearch.Services
{
    public interface ISearchService
    {
        void Search(DumbSearch.Model.SearchParameters parameters);
    }
}
