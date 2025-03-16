using BuildingBlocks.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tixly.API.Controllers;
using Tixly.Application.Tickets.CreateTicket;
using Tixly.Application.Tickets.DeleteTicket;
using Tixly.Application.Tickets.GetTicket;
using Tixly.Application.Tickets.UpdateTicket;
using Tixly.Domain.Dtos;
using Tixly.Infrastructure.Models;

namespace Tixly.Tests.Controllers
{
    public class TicketControllerTests
    {
        private readonly Mock<IGetTicketUseCase> _mockGetTicketUseCase = new();
        private readonly Mock<ICreateTicketUseCase> _mockCreateTicketUseCase = new();
        private readonly Mock<IUpdateTicketUseCase> _mockUpdateTicketUseCase = new();
        private readonly Mock<IDeleteTicketUseCase> _mockDeleteTicketUseCase = new();
        private readonly TicketController _controller;

        private readonly Event Event = new();
        private readonly User User = new();
        private readonly List<PricingTier> _pricingTiers = [];

        public TicketControllerTests()
        {
            _controller = new TicketController(
                _mockGetTicketUseCase.Object,
                _mockCreateTicketUseCase.Object,
                _mockUpdateTicketUseCase.Object,
                _mockDeleteTicketUseCase.Object
            );

            _pricingTiers =
            [
                new()
                {
                    Id = Guid.NewGuid(),
                    EventId = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                    Name = "General Admission",
                    Price = 50.00M,
                    Capacity = 100,
                    Benefits = ["Event Entry"],
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            ];

            Event = new Event()
            {
                Id = new Guid("b87df7be-91e9-48e6-8df2-c6bea744e0b4"),
                Name = It.IsAny<string>(),
                Venue = It.IsAny<string>(),
                Description = It.IsAny<string>(),
                AvailableTickets = It.IsAny<int>(),
                TotalCapacity = It.IsAny<int>(),
                Date = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                PricingTiers = _pricingTiers
            };

            User = new User()
            {
                Id = new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),
                FirstName = "Juca",
                LastName = "Jarbas",
                Email = "customer1@email.com",
                Password = "654123@",
                Role = (int)UserRole.CUSTOMER,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        [Fact]
        public async Task GetTicketAsync_ShouldReturnOk()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var request = new GetTicketRequest(ticketId);
            var ticketDto = new TicketDto(
                ticketId,
                It.IsAny<decimal>(),
                TicketStatus.Booked,
                Event,
                User
            );

            _mockGetTicketUseCase
                .Setup(x => x.GetTicketByIdAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_controller.Ok(ticketDto));

            // Act
            var result = await _controller.GetTicketAsync(request, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }

        [Fact]
        public async Task GetTicketAsync_ShouldReturnNotFound()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var request = new GetTicketRequest(ticketId);

            _mockGetTicketUseCase
                .Setup(x => x.GetTicketByIdAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_controller.NotFound());

            // Act
            var result = await _controller.GetTicketAsync(request, CancellationToken.None);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateTicketAsync_ShouldReturnCreated()
        {
            // Arrange
            var price = It.IsAny<decimal>();
            var status = TicketStatus.Booked;
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var request = new CreateTicketRequest(price, status, eventId, userId);
            var ticketId = Guid.NewGuid();

            _mockCreateTicketUseCase.Setup(x => x.CreateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_controller.Ok(ticketId));

            // Act
            var result = await _controller.CreateTicketAsync(request, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(ticketId, okResult.Value);
        }

        [Fact]
        public async Task CreateTicketAsync_ShouldReturnBadRequest()
        {
            // Arrange
            var price = It.IsAny<decimal>();
            var status = It.IsAny<string>();
            var eventId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var request = new CreateTicketRequest(price, status, eventId, userId);

            _mockCreateTicketUseCase.Setup(x => x.CreateAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_controller.BadRequest());

            // Act
            var result = await _controller.CreateTicketAsync(request, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task DeleteTicketAsync_ShouldReturnOk()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var request = new DeleteTicketRequest(ticketId);
            _mockDeleteTicketUseCase.Setup(x => x.DeleteTicketAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_controller.Ok(true));

            // Act
            var result = await _controller.DeleteTicketAsync(request, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.True((bool)okResult.Value!);
        }

        [Fact]
        public async Task DeleteTicketAsync_ShouldReturnNotFound()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var request = new DeleteTicketRequest(ticketId);
            _mockDeleteTicketUseCase.Setup(x => x.DeleteTicketAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_controller.NotFound(false));

            // Act
            var result = await _controller.DeleteTicketAsync(request, CancellationToken.None);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task UpdateTicketAsync_ShouldReturnOk_WhenTicketUpdatedSuccessfully()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var price = It.IsAny<decimal>();
            var status = TicketStatus.Booked;

            var request = new UpdateTicketRequest(ticketId, price, status);
            var updatedTicket = new TicketDto(ticketId, price, status, Event, User);

            _mockUpdateTicketUseCase.Setup(x => x.UpdateTicketAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_controller.Ok(updatedTicket));

            // Act
            var result = await _controller.UpdateTicketAsync(request, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(updatedTicket, okResult.Value);
        }

        [Fact]
        public async Task UpdateTicketAsync_ShouldReturnNotFound_WhenTicketDoesNotExist()
        {
            // Arrange
            var ticketId = new Guid();
            var price = It.IsAny<decimal>();
            var status = TicketStatus.Booked;

            var request = new UpdateTicketRequest(ticketId, price, status);
            _mockUpdateTicketUseCase.Setup(x => x.UpdateTicketAsync(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(_controller.NotFound(null));

            // Act
            var result = await _controller.UpdateTicketAsync(request, CancellationToken.None);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}
