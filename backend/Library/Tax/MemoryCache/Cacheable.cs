namespace Library.Tax.MemoryCache;
using Library.Back.Calculator.Services;

public class Cacheable {
  TaxService service;

  Dictionary<int, QueryBracketResponse> yearCache = new Dictionary<int, QueryBracketResponse>();
  //Dependency injection for cache
  public Cacheable(TaxService? taxService){
    if(taxService == null)
      throw new ArgumentNullException();

    this.service = taxService;
  }

  public List<Bracket> FetchBrackets(int year, bool withCache)
  {
    if (yearCache.ContainsKey(year) && withCache){
      return yearCache[year].tax_brackets;
    } else {
      var onlineResult = this.service.QueryOnline();
      if (onlineResult != null) {
        yearCache[year] = onlineResult;
        return onlineResult.tax_brackets;
      }
    }
    throw new Exception("Not found tax for this query: " + year);
  }
}