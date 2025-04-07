using BankAccountMicroservice.Data.Repositories;
using BankAccountMicroservice.Models.Enums;
using BankAccountMicroservice.Models;
using BankAccountMicroservice.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

public class BankAccountServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<ILogger<BankAccountService>> _mockLogger;
    private readonly BankAccountService _service;

    public BankAccountServiceTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockLogger = new Mock<ILogger<BankAccountService>>();
        _service = new BankAccountService(_mockUnitOfWork.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAccountByNumberAsync_ValidNumber_ReturnsAccount()
    {
        // Arrange
        var testAccount = new BankAccount { AccountNumber = "123" };

        var mockRepo = new Mock<IRepository<BankAccount>>();
        mockRepo.Setup(r => r.Query())
               .Returns(new List<BankAccount> { testAccount }.AsQueryable());

        _mockUnitOfWork.Setup(u => u.BankAccounts)
                     .Returns(mockRepo.Object);

        // Act
        var result = await _service.GetAccountByNumberAsync("123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("123", result.AccountNumber);
    }

    [Fact]
    public async Task CreateWithdrawalAsync_ValidRequest_CreatesWithdrawal()
    {
        // Arrange
        var testAccount = new BankAccount
        {
            Id = 1,
            AccountNumber = "123",
            AvailableBalance = 1000,
            Status = AccountStatus.Active,
            Type = AccountType.Cheque
        };
        var withdrawalAmount = 500m;

        // Create a proper async test data source
        var testData = new List<BankAccount> { testAccount }.AsQueryable();

        // Mock the BankAccount repository to return the test account when queried
        var mockBankAccountRepo = new Mock<IRepository<BankAccount>>();
        mockBankAccountRepo.Setup(r => r.Query())
            .Returns(testData); // Return the entire dataset

        // Mock the UpdateAsync method
        mockBankAccountRepo.Setup(r => r.UpdateAsync(It.IsAny<BankAccount>()))
            .Returns(Task.CompletedTask);

        // Mock the Withdrawal repository
        var mockWithdrawalRepo = new Mock<IRepository<Withdrawal>>();
        mockWithdrawalRepo.Setup(r => r.AddAsync(It.IsAny<Withdrawal>()))
            .Returns(Task.CompletedTask);

        // Mock the AccountAudit repository
        var mockAuditRepo = new Mock<IRepository<AccountAudit>>();
        mockAuditRepo.Setup(r => r.AddAsync(It.IsAny<AccountAudit>()))
            .Returns(Task.CompletedTask);

        // Mock the UnitOfWork
        _mockUnitOfWork.Setup(u => u.BankAccounts).Returns(mockBankAccountRepo.Object);
        _mockUnitOfWork.Setup(u => u.Withdrawals).Returns(mockWithdrawalRepo.Object);
        _mockUnitOfWork.Setup(u => u.AccountAudits).Returns(mockAuditRepo.Object);
        _mockUnitOfWork.Setup(u => u.CommitAsync()).ReturnsAsync(1);

        // Act
        var result = await _service.CreateWithdrawalAsync("123", withdrawalAmount);

        // Assert
        Assert.True(result.Success);
        Assert.NotNull(result.Withdrawal);
        Assert.Equal(withdrawalAmount, result.Withdrawal.Amount);
        Assert.Equal(testAccount.Id, result.Withdrawal.BankAccountId);

        // Verify interactions
        mockBankAccountRepo.Verify(r => r.Query(), Times.Once); 
        mockBankAccountRepo.Verify(r => r.UpdateAsync(It.Is<BankAccount>(a =>
            a.Id == testAccount.Id && a.AvailableBalance == testAccount.AvailableBalance - withdrawalAmount)), Times.Once);
        mockWithdrawalRepo.Verify(r => r.AddAsync(It.Is<Withdrawal>(w =>
            w.Amount == withdrawalAmount && w.BankAccountId == testAccount.Id)), Times.Once);
        mockAuditRepo.Verify(r => r.AddAsync(It.IsAny<AccountAudit>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }
}