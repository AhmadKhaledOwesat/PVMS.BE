using PVMS.Domain.Entities;
using PVMS.Domain;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PVMS.Infrastructure.EfContext
{
    public class StudioContext(DbContextOptions<StudioContext> dbContextOptions, ICurrentUserProvider? currentUserProvider) : DbContext(dbContextOptions)
    {
        public DbSet<AditLog> AditLogs { get; set; }
        public DbSet<TypeCategory> TypeCategories { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<SmsHistory> SmsHistories { get; set; }
        public DbSet<WorkFlowHistory> WorkFlowHistories { get; set; }
        public DbSet<RoleStatus> RoleStatus { get; set; }
        public DbSet<ProcedureType> ProcedureTypes { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<TicketTypePrice> TicketTypePrices { get; set; }
        public DbSet<RolePrivilege> RolePrivilege { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserWorkFlowDefinition> UserWorkFlowDefinitions { get; set; }
        public DbSet<TicketViolation> TicketViolations { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<Privilege> Privilege { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportParameter> ReportParameter { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<WorkFlowDefinition> WorkFlowDefinitions { get; set; }
        public DbSet<WorkFlowStep> WorkFlowSteps { get; set; }
        public DbSet<WorkFlowDefinitionTicketType> WorkFlowDefinitionTicketTypes { get; set; }
        public DbSet<WorkFlowStepApproveRole> WorkFlowStepApproveRoles { get; set; }
        public DbSet<WorkFlowStepRejectRole> WorkFlowStepRejectRoles { get; set; }
        public DbSet<WorkFlowStepSkipRole> WorkFlowStepSkipRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkFlowHistory>().ToTable("WorkFlowHistory");
            modelBuilder.Entity<TicketCategory>().ToTable("TicketCategory");
            modelBuilder.Entity<TypeCategory>().ToTable("TypeCategory");
            modelBuilder.Entity<AditLog>().ToTable("AditLog");
            modelBuilder.Entity<AditLog>()
                .HasOne(a => a.Creator)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AditLog>()
                .HasOne(a => a.Modifier)
                .WithMany()
                .HasForeignKey(a => a.ModifiedBy)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WorkFlowDefinition>().ToTable("WorkFlowDefinition");
            modelBuilder.Entity<WorkFlowStep>().ToTable("WorkFlowStep");
            modelBuilder.Entity<WorkFlowDefinitionTicketType>().ToTable("WorkFlowDefinitionTicketType");
            modelBuilder.Entity<WorkFlowStepApproveRole>().ToTable("WorkFlowStepApproveRole");
            modelBuilder.Entity<WorkFlowStepRejectRole>().ToTable("WorkFlowStepRejectRole");
            modelBuilder.Entity<WorkFlowStepSkipRole>().ToTable("WorkFlowStepSkipRole");

            modelBuilder.Entity<WorkFlowStep>()
                .HasOne(s => s.WorkFlowDefinition)
                .WithMany(d => d.Steps)
                .HasForeignKey(s => s.WorkFlowDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.WorkFlowDefinition)
                .WithMany()
                .HasForeignKey(t => t.WorkFlowDefinitionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkFlowDefinitionTicketType>()
                .HasOne(x => x.WorkFlowDefinition)
                .WithMany(d => d.TicketTypes)
                .HasForeignKey(x => x.WorkFlowDefinitionId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<WorkFlowDefinitionTicketType>()
                .HasOne(x => x.TicketType)
                .WithMany()
                .HasForeignKey(x => x.TicketTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkFlowStepApproveRole>()
                .HasOne(x => x.WorkFlowStep)
                .WithMany(s => s.ApproveRoles)
                .HasForeignKey(x => x.WorkFlowStepId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<WorkFlowStepApproveRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkFlowStepRejectRole>()
                .HasOne(x => x.WorkFlowStep)
                .WithMany(s => s.RejectRoles)
                .HasForeignKey(x => x.WorkFlowStepId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<WorkFlowStepRejectRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkFlowStepSkipRole>()
                .HasOne(x => x.WorkFlowStep)
                .WithMany(s => s.SkipRoles)
                .HasForeignKey(x => x.WorkFlowStepId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<WorkFlowStepSkipRole>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserWorkFlowDefinition>().ToTable("UserWorkFlowDefinition");
            modelBuilder.Entity<UserWorkFlowDefinition>()
                .HasOne(x => x.User)
                .WithMany(u => u.UserWorkFlowDefinitions)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserWorkFlowDefinition>()
                .HasOne(x => x.WorkFlowDefinition)
                .WithMany(w => w.UserWorkFlowDefinitions)
                .HasForeignKey(x => x.WorkFlowDefinitionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserWorkFlowDefinition>()
                .HasIndex(x => new { x.UserId, x.WorkFlowDefinitionId })
                .IsUnique();
            modelBuilder.Entity<UserWorkFlowDefinition>()
                .HasIndex(x => x.UserId);
            modelBuilder.Entity<UserWorkFlowDefinition>()
                .HasIndex(x => x.WorkFlowDefinitionId);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var auditEntries = new List<AditLog>();
            var userId = currentUserProvider?.GetCurrentUserId() ?? Guid.Empty;
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is AditLog)
                    continue;

                if (entry.State is EntityState.Modified or EntityState.Added or EntityState.Deleted)
                {
                    var entityTypeName = entry.Metadata.ClrType.Name;
                    Guid? entityId = GetPrimaryKeyGuid(entry);

                    // Check if any properties are actually modified for Modified entities
                    if (entry.State == EntityState.Modified)
                    {
                        bool hasActualChanges = entry.Properties.Any(p =>
                            !p.Metadata.IsShadowProperty() &&
                            !p.Metadata.IsForeignKey() &&
                            p.IsModified &&
                            !Equals(p.OriginalValue, p.CurrentValue));

                        if (!hasActualChanges)
                            continue; // Skip if no properties actually changed
                    }

                    string? oldJson = null;
                    string? newJson = null;

                    if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                        oldJson = GetPropertyValuesJson(entry, useOriginal: true);
                    if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                        newJson = GetPropertyValuesJson(entry, useOriginal: false);

                    auditEntries.Add(new AditLog
                    {
                        Id = Guid.NewGuid(),
                        EntityType = entityTypeName,
                        EntityId = entityId,
                        OldValue = oldJson,
                        NewValue = newJson,
                        CreatedBy = userId,
                        CreatedDate = now
                    });
                }
            }

            foreach (var audit in auditEntries)
                await Set<AditLog>().AddAsync(audit, cancellationToken);
            }
            catch (Exception ex)
            {
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        private static Guid? GetPrimaryKeyGuid(EntityEntry entry)
        {
            var key = entry.Metadata.FindPrimaryKey();
            if (key?.Properties.Count != 1)
                return null;
            var value = entry.Property(key.Properties[0].Name).CurrentValue;
            return value as Guid?;
        }

        private static string? GetPropertyValuesJson(EntityEntry entry, bool useOriginal)
        {
            var dict = new Dictionary<string, object?>();
            foreach (var prop in entry.Properties.Where(p =>
                !p.Metadata.IsShadowProperty() &&
                !p.Metadata.IsForeignKey() &&
                !IsBaseEntityProperty(p.Metadata.Name)))
            {
                var name = prop.Metadata.Name;
                var value = useOriginal ? prop.OriginalValue : prop.CurrentValue;
                if (value is DateTime dt)
                    dict[name] = dt;
                else if (value != null && value.GetType().IsEnum)
                    dict[name] = value.ToString();
                else
                    dict[name] = value;
            }
            return JsonSerializer.Serialize(dict);
        }

        private static bool IsBaseEntityProperty(string propertyName)
        {
            return propertyName switch
            {
                "Id" or "CreatedBy" or "CreatedDate" or "ModifiedBy" or "ModifiedDate" => true,
                _ => false
            };
        }
    }
}
