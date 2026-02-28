using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace NWARE.Domain.DIContainer
{
    public static class DIContainer
    {
        public static ServiceProvider Instance { get; set; }
    }
}
