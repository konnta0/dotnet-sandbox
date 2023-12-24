using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;

namespace EFCore;

    public class CustomSqlServerMigrationsSqlGenerator(
        MigrationsSqlGeneratorDependencies dependencies,
        ICommandBatchPreparer commandBatchPreparer)
        : SqlServerMigrationsSqlGenerator(dependencies, commandBatchPreparer)
    {
        protected override void CreateTableColumns(CreateTableOperation operation, IModel? model, MigrationCommandListBuilder builder)
        {
            operation.Columns.Sort(new ColumnOrderComparision());
            base.CreateTableColumns(operation, model, builder);
        }

        private class ColumnOrderComparision : IComparer<AddColumnOperation>
        {
            public int Compare(AddColumnOperation? x, AddColumnOperation? y)
            {
                var orderX = Convert.ToInt32(x?.FindAnnotation(CustomAnnotationNames.ColumnOrder)?.Value ?? 0);
                var orderY = Convert.ToInt32(y?.FindAnnotation(CustomAnnotationNames.ColumnOrder)?.Value ?? 0);
                return orderX.CompareTo(orderY);
            }
        }
    }