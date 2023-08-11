using Dapper;
using CutInLine.Models.Class;

namespace CutInLine.Repository
{
    public class EventsRepository : Events
    {
        private readonly IUnitOfWork _unitOfWork;

        public EventsRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Create(Events Item)
        {
            var sql = @"

            INSERT INTO Events (
                SDescription, 
                SDetail,
                SGroupName,
                Token,
                DDateCreated
            ) VALUES (
                @SDescription, 
                @SDetail,
                @SGroupName,    
                @Token,
                @DDateCreated

            ) RETURNING EventId; ";

            return await _unitOfWork.Connection.ExecuteScalarAsync<int>(sql, Item);
        }

        public async Task Delete(int id, string token)
        {
            await _unitOfWork.Connection.ExecuteAsync("delete from Events WHERE userId=@_id and token=@_token ",

            new { _id = id, _token = token });
        }

        public async Task Update(Events Item)
        {
            var sql = @"

            UPDATE Events SET
                SDescription = @SDescription,
                SDetail = @SDetail,
                DDateCreated = @DDateCreated
            WHERE EventId = @EventId and Token=@Token; ";
            await _unitOfWork.Connection.ExecuteAsync(sql, Item);
        }

        public async Task<Events?> GetById(int id, string token)
        {
            var sql = @"

            select                                     
              Events.*
            from Events                               
            where Events.ProductId = @_id  
            where Events.Token     = @_token 
            ";

            var resultado = await _unitOfWork.Connection.QueryAsync<Events>
            (sql, new { _id = id, _token = token });

            return resultado.FirstOrDefault();
        }

        public async Task<List<Events>> GetEvents(string token, string where)
        {
            var sql = @$"
            select
            Events.*
            from
            (
                select                                     
                Events.*
                from Events                               
                where Events.EventId = @_id  
            ) Events
             where 1=1 {where}
            ";

            var resultado = await _unitOfWork.Connection.QueryAsync<Events>
            (sql, new { _token = token });

            return resultado.ToList();
        }
    }
}