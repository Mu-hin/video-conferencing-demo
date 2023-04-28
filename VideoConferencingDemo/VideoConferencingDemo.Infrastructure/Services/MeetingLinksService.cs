using AutoMapper;
using VideoConferencingDemo.Infrastructure.UnitOfWorks;

namespace VideoConferencingDemo.Infrastructure.Services;

public class MeetingLinksService : IMeetingLinksService
{
    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    private readonly IMapper _mapper;

    public MeetingLinksService(IApplicationUnitOfWork unitOfWork, IMapper mapper)
    {
        _applicationUnitOfWork = unitOfWork;
        _mapper = mapper;
    }
}
