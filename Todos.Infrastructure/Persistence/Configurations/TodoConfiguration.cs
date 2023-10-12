using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todos.Domain.Entities;

namespace Todos.Infrastructure.Persistence.Configurations;

internal class TodoConfiguration : IEntityTypeConfiguration<Todo>
{
    public void Configure(EntityTypeBuilder<Todo> builder)
    {
        builder.ToTable(nameof(Todo));

        builder.HasKey(t => t.Id);
    }
}
