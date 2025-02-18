namespace ConsoleApp1;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        if (CheckDateRange(start, end))
        {
            return 0;
        }

        var budgets = _budgetRepo.GetAll();
        var totalAmount = 0m;

        foreach (var budget in budgets)
        {
            if (!DateTime.TryParseExact(budget.YearMonth + "01", "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime budgetMonth))
            {
                continue; // 無效的 YearMonth 格式，跳過
            }

            var monthStart = new DateTime(budgetMonth.Year, budgetMonth.Month, 1);
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            if (end < monthStart || start > monthEnd)
            {
                continue; // 該月不在查詢範圍內
            }

            var effectiveStart = start > monthStart ? start : monthStart;
            var effectiveEnd = end < monthEnd ? end : monthEnd;

            int daysInMonth = DateTime.DaysInMonth(budgetMonth.Year, budgetMonth.Month);
            int effectiveDays = (effectiveEnd - effectiveStart).Days + 1;

            totalAmount += (budget.Amount / daysInMonth) * effectiveDays;
        }

        return totalAmount;
    }
    

    private bool CheckDateRange(DateTime start, DateTime end)
        => start > end;
}

public class Budget
{
    public string YearMonth { get; set; } // 格式為 "yyyyMM"
    public decimal Amount { get; set; }   // 該月的總預算金額
}

public interface IBudgetRepo
{
    public IEnumerable<Budget> GetAll();
}