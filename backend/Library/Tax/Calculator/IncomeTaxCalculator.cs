namespace Library.Back.Calculator;

public static class IncomeBackCalculator
{
  public static IncomeTaxResult Calculate(int year, decimal income, decimal raise, List<Bracket> brackets)
  {
    var result = new IncomeTaxResult(year);
    var sorted = brackets.OrderBy(b => b.min).ToList();
    var incomeBracket = IncomeBackCalculator.FindBracketByIncome(income, brackets);
    if(incomeBracket != null) {
      result.incomeTaxPayableAmount = ApplyRateToAmount(incomeBracket.rate, income);

      if (raise > 0) {
        if(incomeBracket.max > 0){
          decimal thresholdTaxableWindow = incomeBracket.max.Value - income;
          result.thresholdPayableAmount = ApplyRateToAmount(incomeBracket.rate, thresholdTaxableWindow);

          decimal raiseTaxable = raise - thresholdTaxableWindow;
          var raiseBracket = IncomeBackCalculator.FindBracketByIncome(income+raise, brackets);
          if (raiseBracket != null) {
            result.raiseTaxes = ApplyRateToAmount(raiseBracket.rate, raiseTaxable);
          }
        } else {
          result.raiseTaxes = ApplyRateToAmount(raise, incomeBracket.rate);
        }
      }
    }

    result.marginalTaxPayableAmount = Math.Round(result.thresholdPayableAmount + result.raiseTaxes, 2);
    result.income = income;
    result.raise = raise;

    return result;
  }

  private static decimal ApplyRateToAmount(decimal rate, decimal amount) {
    return Math.Round(rate * amount, 2);
  }

  public static Bracket? FindBracketByIncome(decimal amount, List<Bracket> bracketList) {
    foreach(var bracket in bracketList) {
      if (bracket.min != null && bracket.max != null){
        if (bracket.min <= amount && amount < bracket.max){
          return bracket;
        }
      } else if (bracket.min != null) {
        if (bracket.min <= amount){
          return bracket;
        }
      }
      else if (bracket.max != null) {
        if (bracket.max > amount){ //2021 rule
          return bracket;
        }
      }
    }
    return null;
  }

}
