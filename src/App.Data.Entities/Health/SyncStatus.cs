using System;

namespace App.Data.Entities.Health
{
    public class SyncStatus : BaseDataEntity
    {
        public DateTime? CheckTime { get; set; }
    }
}