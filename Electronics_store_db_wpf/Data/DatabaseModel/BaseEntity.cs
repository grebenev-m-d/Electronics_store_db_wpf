using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Electronics_store_db_wpf.Core;

namespace Electronics_store_db_wpf.Data.DatabaseModel
{
    public abstract class BaseEntity : ObservableObject, ICloneable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public object Clone() => MemberwiseClone();
    }
}
