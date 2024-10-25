using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.SharedContext
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow 
        {
            get
            {
                return DateTime.UtcNow;
            }        
        }

        
    }
}
