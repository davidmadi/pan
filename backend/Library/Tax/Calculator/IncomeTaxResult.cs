namespace Library.Back.Calculator
{
  public class IncomeTaxResult
  {
    public IncomeTaxResult(int year){
      this.year = year;
    }
    internal decimal raiseTaxes;
    internal decimal thresholdPayableAmount;

    public decimal year{get;set;}
    public decimal income{get;set;}
    public decimal raise{get;set;}
    public decimal marginalTaxPayableAmount {get;set;}
    public decimal incomeTaxPayableAmount{get;set;}
  }
}