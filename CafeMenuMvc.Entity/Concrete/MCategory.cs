using CafeMenuMvc.Shared.Abstract;
using System;

namespace CafeMenuMvc.Entity.Concrete
{
    public class MCategory:EntityBase
    {
        public virtual int ISDELETED { get; set; } = 0;
        public virtual DateTime CREATEDDATE { get; set; }
        public virtual int CREATORUSERID { get; set; }
    }
}