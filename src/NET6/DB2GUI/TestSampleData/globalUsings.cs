﻿global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global  using System.Threading.Tasks;
global using Generated;
global using Xunit;
global using Microsoft.EntityFrameworkCore;
global using System.IO;

//[assembly: CollectionBehavior(MaxParallelThreads = 1)]
[assembly: CollectionBehavior(DisableTestParallelization = true)] 