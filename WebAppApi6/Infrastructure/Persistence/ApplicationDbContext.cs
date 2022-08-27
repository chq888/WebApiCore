//using System;
//using System.Linq;
//using System.Reflection;
//using System.Threading;
//using System.Threading.Tasks;
//using Application.Common.Interfaces;
//using Domain.Common;
//using Domain.Entities;
//using Domain.Interfaces;
//using Duende.IdentityServer.EntityFramework.Options;
//using Infrastructure.Identity;
//using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;

//namespace Infrastructure.Persistence
//{
//    public class ApplicationDbContext
//    {
//        private readonly ICurrentUserService _currentUserService;
//        private readonly IDateTimeService _dateTimeService;
//        private readonly IDomainEventService _domainEventService;

//        public ApplicationDbContext(
//            DbContextOptions contextOptions,
//            ICurrentUserService currentUserService,
//            IDateTimeService dateTimeService,
//            IDomainEventService domainEventService) : base(contextOptions, operationalStoreOptions)
//        {
//            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
//            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
//            _domainEventService = domainEventService ?? throw new ArgumentNullException(nameof(domainEventService));
//        }


//        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
//        {
//            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
//            {
//                switch (entry.State)
//                {
//                    case EntityState.Added:
//                        entry.Entity.CreatedBy = _currentUserService.UserId;
//                        entry.Entity.CreatedAt = _dateTimeService.Now;
//                        break;

//                    case EntityState.Modified:
//                        entry.Entity.ModifiedBy = _currentUserService.UserId;
//                        entry.Entity.ModifiedAt = _dateTimeService.Now;
//                        break;
//                }
//            }

//            var result = await base.SaveChangesAsync(cancellationToken);
//            await DispatchEvents();

//            return result;
//        }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

//            base.OnModelCreating(builder);
//        }

//        private async Task DispatchEvents()
//        {
//            while (true)
//            {
//                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
//                    .Select(x => x.Entity.DomainEvents)
//                    .SelectMany(x => x)
//                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);

//                if (domainEventEntity == null) break;

//                domainEventEntity.IsPublished = true;
//                await _domainEventService.Publish(domainEventEntity);
//            }
//        }
//    }
//}