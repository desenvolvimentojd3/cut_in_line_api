
using CutInLine.Models.Class;

namespace CutInLine.Models.Interface
{
    public interface IProducts
    {
        Task<dynamic> Save(Products user, string token);
        Task<dynamic> Delete(int id, string token);
        Task<dynamic> GetById(int id, string token);
        Task<dynamic> GetProducts(SearchHelper search, string token);
    }
}
