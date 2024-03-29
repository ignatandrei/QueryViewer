﻿namespace RoslynQueryGenerator

{
    public class OrderBy
    {
        public string FieldName { get; set; }
        public bool Asc { get; set; }
    }
    public class SearchField
    {
        public string FieldName { get; set; }        
        public string Value { get; set; }
        public SearchCriteria Criteria { get; set; }
         
        public string CriteriaString { get; set; }

    }

    public enum SearchCriteria
    {
        None = 0,
        StartsWith,
        EndsWith,
        Contains,
        Equal,
        Different,
        Greater,
        Less,
        GreaterOrEqual,
        LessOrEqual
    }

}
