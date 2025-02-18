using ConsoleApp1;
using NSubstitute;

namespace TestProject1;

public class BudgetTests
{
    //private  IBudgetReport _budgetReport;
    private  BudgetService _budgetService;
    [SetUp]
    public void Setup()
    {
        //_budgetReport = Substitute.For<IBudgetReport>();
    }

    [Test]
    public void invalid_date_range()
    {
        var budgetReport = Substitute.For<IBudgetReport>();

        budgetReport.GetAll().Returns(new List<Budget>
        {
            new Budget { YearMonth = "202502", Amount = 28000m }
        });
        
        _budgetService = new BudgetService(budgetReport);
        
        
    }
}
