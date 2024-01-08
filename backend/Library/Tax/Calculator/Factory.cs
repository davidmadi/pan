using System;
using System.Collections.Generic;
using Library.Tax.MemoryCache;

namespace Library.Back.Calculator;

public static class Factory {

  private static Dictionary<int, Cacheable> singletonHashServices = new Dictionary<int, Cacheable>();

  static Factory(){
    singletonHashServices[2019] = new Cacheable(new InterviewTestService(2019));
    singletonHashServices[2020] = new Cacheable(new InterviewTestService(2020));
    singletonHashServices[2021] = new Cacheable(new InterviewTestService(2021));
  }

  public static Cacheable GetTaxServiceBy(int year) {

    if (singletonHashServices.ContainsKey(year))
      return singletonHashServices[year];

    throw new ArgumentOutOfRangeException("Year " + year + " is not supported yet");
  }


}