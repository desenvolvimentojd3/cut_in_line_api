using ApesWebPcp.Services;
using CutInLine.Models.Class;
using CutInLine.Models.Interface;
using CutInLine.Repository;
using CutInLine.Services;

namespace CutInLine.Models.Implementation
{
    public class EventsImplementation : IEvents
    {
        private readonly EventsRepository _eventsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EventsImplementation(EventsRepository EventsRepository, IUnitOfWork unitOfWork)
        {
            _eventsRepository = EventsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<dynamic> Save(Events _event, string token)
        {
            _event.Token = token;

            if (_event.EventId == 0)
                _event.EventId = await _eventsRepository.Create(_event);
            else if (_event.EventId > 0)
                await _eventsRepository.Update(_event);

            return new { success = true };
        }

        public async Task<dynamic> GetById(int id, string token)
        {
            var _event = await _eventsRepository.GetById(id, token);

            return new { success = true, _event };
        }

        public async Task<dynamic> Delete(int id, string token)
        {
            await _eventsRepository.Delete(id, token);

            return new { success = true };
        }

        public async Task<dynamic> GetEvents(SearchHelper search, string token)
        {
            var where = Search.GetSearchString(search);

            var events = await _eventsRepository.GetEvents(where, token);

            return new { success = true, events };
        }
    }
}