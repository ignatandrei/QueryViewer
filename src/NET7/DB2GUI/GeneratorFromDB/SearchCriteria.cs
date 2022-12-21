namespace GeneratorFromDB;


public enum SearchCriteria
{
    None = 0,
    StartsWith,
    EndsWith,
    Contains,
    Equal,
    InArray,
    NotInArray,
    Between,
    NotBetween,
    Different,
    Greater,
    Less,
    GreaterOrEqual,
    LessOrEqual,
    //Like,

    EqualYear,
    DifferentYear,
    GreaterYear,
    LessYear,
    GreaterOrEqualYear,
    LessOrEqualYear,

    //EqualMonthYear,
    //DifferentMonthYear,
    //GreaterMonthYear,
    //LessMonthYear,
    //GreaterOrEqualMonthYear,
    //LessOrEqualMonthYear,

    //EqualDay,
    //DifferentDay,
    //GreaterDay,
    //LessDay,
    //GreaterOrEqualDay,
    //LessOrEqualDay,

}