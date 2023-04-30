using Autofac.Extras.Moq;
using AutoMapper;
using Moq;
using Shouldly;
using VideoConferencingDemo.Infrastructure.Adapter;
using VideoConferencingDemo.Infrastructure.Exceptions;
using VideoConferencingDemo.Infrastructure.Repositories;
using VideoConferencingDemo.Infrastructure.Services;
using VideoConferencingDemo.Infrastructure.UnitOfWorks;
using ApplicationUserEO = VideoConferencingDemo.Infrastructure.Entities.Identity.ApplicationUser;
using ApplicationUserBO = VideoConferencingDemo.Infrastructure.BusinessObjects.ApplicationUser;
using VideoConferencingDemo.Infrastructure.Entities;
using System.Linq.Expressions;

namespace VideoConferencingDemo.Infrastructure.UnitTest.Services;

public class MeetingLinksServiceTest
{
    private AutoMock _mock;
    private Mock<IApplicationUnitOfWork> _applicationtUnitOfWorkMock;
    private Mock<IMeetingLinkRepository> _meetingLinkRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private IMeetingLinksService _meetingLinksService;
    private Mock<IUserManager> _userManagerMock;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _mock = AutoMock.GetLoose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _mock?.Dispose();
    }

    [SetUp]
    public void Setup()
    {
        _applicationtUnitOfWorkMock = _mock.Mock<IApplicationUnitOfWork>();
        _meetingLinkRepositoryMock = _mock.Mock<IMeetingLinkRepository>();
        _mapperMock = _mock.Mock<IMapper>();
        _meetingLinksService = _mock.Create<MeetingLinksService>();
        _userManagerMock = _mock.Mock<IUserManager>();
    }

    [TearDown]
    public void TearDown()
    {
        _applicationtUnitOfWorkMock.Reset();
        _meetingLinkRepositoryMock.Reset();
        _mapperMock.Reset();
        _userManagerMock.Reset();
    }

    [Test, Category("unit test")]
    public async Task CreateMeetingLinkAsync_LessThanMaxLimit_CreateLink()
    {
        // Arrange
        var userEO = new ApplicationUserEO
        {
            TotalGeneratedLinq = 5
        };

        var userBO = new ApplicationUserBO
        {
            TotalGeneratedLinq = 5
        };

        var meetingLinks = new MeetingLink
        {
            Id = Guid.Parse("042254EC-A1AE-4D6A-934D-4BF2D203BC3C"),
            UserEmail = "user@gmail.com",
            MeetingId = Guid.Parse("042254ED-A1AE-4D6A-934D-4BF2D203BC3C"),
            LastUsed = DateTime.Parse("2023-04-28 12:20:05.5503846")
        };

        _applicationtUnitOfWorkMock.Setup(x => x.MeetingLinks)
            .Returns(_meetingLinkRepositoryMock.Object).Verifiable();

        _userManagerMock.Setup(x => x.GetUserAsync(default)).ReturnsAsync(userBO).Verifiable();

        //_meetingLinkRepositoryMock.Setup(x => x.AddAsync(meetingLinks)).Returns(Task.CompletedTask).Verifiable();

        _applicationtUnitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

        _userManagerMock.Setup(x => x.IncreaseTotalLinkInfoAsync(default)).Returns(Task.CompletedTask).Verifiable();

        // Act
        var result = await _meetingLinksService.CreateMeetingLinkAsync(default);

        // Assert
        this.ShouldSatisfyAllConditions(() =>
        {
            _applicationtUnitOfWorkMock.VerifyAll();
            _meetingLinkRepositoryMock.VerifyAll();
            _userManagerMock.VerifyAll();
            result.ShouldBeOfType<Guid>();
        });
    }

    [Test, Category("unit test")]
    public async Task CreateMeetingLinkAsync_MoreThanMaxLimit_ThrowMaxLimitException()
    {
        // Arrange
        var userBO = new ApplicationUserBO
        {
            TotalGeneratedLinq = 25
        };

        _applicationtUnitOfWorkMock.Setup(x => x.MeetingLinks)
            .Returns(_meetingLinkRepositoryMock.Object).Verifiable();

        _userManagerMock.Setup(x => x.GetUserAsync(default)).ReturnsAsync(userBO).Verifiable();

        // Act
        await Should.ThrowAsync<MaxLimitException>(async () =>
            await _meetingLinksService.CreateMeetingLinkAsync(default)
        );
    }

    [Test, Category("unit test")]
    public async Task CheckLinkOwner_ValidLink_If_Owner_ReturnTrue()
    {
        // Arrange
        var userBO = new ApplicationUserBO
        {
            TotalGeneratedLinq = 25,
            Email = "user@gmail.com"
        };

        var meetingLinks = new List<MeetingLink>()
        {
            new MeetingLink
            {
                Id = Guid.Parse("042254EC-A1AE-4D6A-934D-4BF2D203BC3C"),
                UserEmail = "user@gmail.com",
                MeetingId = Guid.Parse("042254ED-A1AE-4D6A-934D-4BF2D203BC3C"),
                LastUsed = DateTime.Parse("2023-04-28 12:20:05.5503846")
            }
        };

        _applicationtUnitOfWorkMock.Setup(x => x.MeetingLinks)
            .Returns(_meetingLinkRepositoryMock.Object).Verifiable();

        _userManagerMock.Setup(x => x.GetUserAsync(default)).ReturnsAsync(userBO).Verifiable();

        _meetingLinkRepositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<MeetingLink, bool>>>(),
            It.IsAny<string>())).Returns(meetingLinks).Verifiable();

        _applicationtUnitOfWorkMock.Setup(x => x.SaveAsync()).Returns(Task.CompletedTask).Verifiable();

        // Act
        var result = await _meetingLinksService.CheckLinkOwnerAsync(meetingLinks[0].MeetingId, default);

        // Assert
        this.ShouldSatisfyAllConditions(() =>
        {
            _applicationtUnitOfWorkMock.VerifyAll();
            _meetingLinkRepositoryMock.VerifyAll();
            _userManagerMock.VerifyAll();
            result.Equals(true);
        });
    }

    [Test, Category("unit test")]
    public async Task CheckLinkOwner_ValidLink_If_Not_Owner_ReturnFalse()
    {
        // Arrange
        var userBO = new ApplicationUserBO
        {
            TotalGeneratedLinq = 25,
            Email = "user1@gmail.com"
        };


        var meetingLinks = new List<MeetingLink>()
        {
            new MeetingLink
            {
                Id = Guid.Parse("042254EC-A1AE-4D6A-934D-4BF2D203BC3C"),
                UserEmail = "user@gmail.com",
                MeetingId = Guid.Parse("042254ED-A1AE-4D6A-934D-4BF2D203BC3C"),
                LastUsed = DateTime.Parse("2023-04-28 12:20:05.5503846")
            }
        };

        _applicationtUnitOfWorkMock.Setup(x => x.MeetingLinks)
            .Returns(_meetingLinkRepositoryMock.Object).Verifiable();

        _userManagerMock.Setup(x => x.GetUserAsync(default)).ReturnsAsync(userBO).Verifiable();

        _meetingLinkRepositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<MeetingLink, bool>>>(),
            It.IsAny<string>())).Returns(meetingLinks).Verifiable();

        // Act
        var result = await _meetingLinksService.CheckLinkOwnerAsync(meetingLinks[0].MeetingId, default);

        // Assert
        this.ShouldSatisfyAllConditions(() =>
        {
            _applicationtUnitOfWorkMock.VerifyAll();
            _meetingLinkRepositoryMock.VerifyAll();
            _userManagerMock.VerifyAll();
            result.Equals(false);
        });
    }

    [Test, Category("unit test")]
    public async Task CheckLinkOwner_InValidLink_Throw_InvalidLinkException()
    {
        // Arrange
        var userBO = new ApplicationUserBO
        {
            TotalGeneratedLinq = 25,
            Email = "user@gmail.com"
        };

        var meetingLinks = new List<MeetingLink>();

        _applicationtUnitOfWorkMock.Setup(x => x.MeetingLinks)
            .Returns(_meetingLinkRepositoryMock.Object).Verifiable();

        _userManagerMock.Setup(x => x.GetUserAsync(default)).ReturnsAsync(userBO).Verifiable();

        _meetingLinkRepositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<MeetingLink, bool>>>(),
            It.IsAny<string>())).Returns(meetingLinks).Verifiable();

        var meetingId = Guid.Parse("042254EC-0000-0000-934D-4BF2D203BC3C");

        // Act
        await Should.ThrowAsync<InvalidLinkException>(async () =>
            await _meetingLinksService.CheckLinkOwnerAsync(meetingId, default)
        );
    }

    [Test, Category("unit test")]
    public async Task DeleteMeetingLinkAsync_InValidId_Throw_InvalidOperationException()
    {
        MeetingLink? meetingLink = null;
        var meetingId = Guid.Parse("042254EC-0000-0000-934D-4BF2D203BC3C");

        _applicationtUnitOfWorkMock.Setup(x => x.MeetingLinks)
            .Returns(_meetingLinkRepositoryMock.Object).Verifiable();

        _meetingLinkRepositoryMock.Setup(x => x.GetByIdAsync(meetingId
            )).ReturnsAsync(meetingLink).Verifiable();

        // Act
        await Should.ThrowAsync<InvalidOperationException>(async () =>
            await _meetingLinksService.DeleteMeetingLinkAsync(meetingId)
        );
    }
}
