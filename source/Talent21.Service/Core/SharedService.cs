using System;
using System.Linq;
using Talent21.Data.Repository;
using Talent21.Service.Abstraction;
using Talent21.Service.Models;

namespace Talent21.Service.Core
{
    public abstract class SharedService : ISharedService, ISecuredService
    {
        public string CurrentUserId { set; protected get; }
    }
}