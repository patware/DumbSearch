using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
namespace DumbSearch.Services
{
    public interface ISearchService
    {
        void Search(DumbSearch.Model.SearchParameters parameters);
    }
}
