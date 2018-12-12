using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    public interface IPoolable
    {
        string PoolId { get; }
        int ObjectsCount { get; }
    }

