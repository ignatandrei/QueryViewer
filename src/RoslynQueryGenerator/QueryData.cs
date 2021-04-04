using System;
using System.Collections.Generic;

namespace RoslynQueryGenerator
{
    public class QueryData
    {
        public QueryData()
        {
            Ignore = false;
        }
        
        public SearchField[] DefaultValues { get; set; }
        public string IDSearch { get; set; }
        public string Name { get; set; }
        public string[] QueryMultiline { get; set; }
        public bool Ignore { get; set; }
        public string Query
        {
            get
            {
                return string.Join(Environment.NewLine, QueryMultiline);
            }
        }
        internal void AddField(Type f, string name)
        {
            if(Fields == null)
                Fields= new Dictionary<string, Type>();

            Fields.Add(name, f);

        }

        public Dictionary<string,Type> Fields;
    }
}
