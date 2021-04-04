
//using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace RoslynQueryGenerator
{
    public class ItemData
    {
        public string connectionString { get; set; }
        public QueryData[] queriesData { get; set; }

        public QueryData[] queries
        {
            get
            {
                return queriesData.Where(it => it.Ignore == false).ToArray();
            }
        }
        public string Name { get; set; }
        public bool HasResolvedQueries { get; private set; }
        public void ResolveQueries()
        {
            HasResolvedQueries = true;
            if (queries.All(it => it.Ignore))
                return;
            using (var cn = new SqlConnection(this.connectionString))
            {
                cn.Open();
                foreach (var item in queries)
                {
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.CommandText = $"select top 1 * from({item.Query}) a";
                        using (var read = cmd.ExecuteReader())
                        {
                            var nr = read.FieldCount;
                            for (int i = 0; i < nr; i++)
                            {
                                var f = read.GetFieldType(i);
                                var name = read.GetName(i);
                                item.AddField(f, name);
                            }
                        }
                    }
                }
            }
        }
    }
}

