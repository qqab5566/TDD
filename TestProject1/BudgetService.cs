namespace ConsoleApp1;

public class BudgetService
{
    private readonly IBudgetReport _budgetReport;

    public BudgetService(IBudgetReport budgetReport)
    {
        _budgetReport = budgetReport;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        if (start > end)
        {
            return 0;
        }

        var budgets = _budgetReport.GetAll();
        decimal totalAmount = 0;

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
}

public class Budget
{
    public string YearMonth { get; set; } // 格式為 "yyyyMM"
    public decimal Amount { get; set; }   // 該月的總預算金額
}

public interface IBudgetReport
{
    public IEnumerable<Budget> GetAll();
}