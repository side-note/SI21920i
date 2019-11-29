﻿using System;
//using System.Transactions;
using System.Data.SqlClient;

namespace TypesProject.mapper
{
    interface IContext: IDisposable
    {
        void Open();
        SqlCommand createCommand();
        void EnlistTransaction();

    }
}
