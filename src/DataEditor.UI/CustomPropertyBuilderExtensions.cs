using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore;

public static class CustomPropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasColumnOrder<TProperty>(this PropertyBuilder<TProperty> propertyBuilder,
        int order)
    {
        propertyBuilder.HasAnnotation(CustomAnnotationNames.ColumnOrder, order);
        return propertyBuilder;
    }
}