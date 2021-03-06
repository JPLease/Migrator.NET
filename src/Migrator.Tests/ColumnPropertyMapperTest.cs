using Migrator.Framework;
using Migrator.Providers;
using Migrator.Providers.SqlServer;
using NUnit.Framework;
using System.Data;

namespace Migrator.Tests
{
	[TestFixture]
	public class ColumnPropertyMapperTest
	{
		[Test]
		public void SqlServerCreatesNotNullSql()
		{
			var mapper = new ColumnPropertiesMapper(new SqlServerDialect(), "varchar(30)");
			mapper.MapColumnProperties(new Column("foo", DbType.String, ColumnProperty.NotNull));
			Assert.AreEqual("[foo] varchar(30) NOT NULL", mapper.ColumnSql);
		}

		[Test]
		public void SqlServerCreatesSqWithBooleanDefault()
		{
			var mapper = new ColumnPropertiesMapper(new SqlServerDialect(), "bit");
			mapper.MapColumnProperties(new Column("foo", DbType.Boolean, 0, false));
			Assert.AreEqual("[foo] bit DEFAULT 0", mapper.ColumnSql);

			mapper.MapColumnProperties(new Column("bar", DbType.Boolean, 0, true));
			Assert.AreEqual("[bar] bit DEFAULT 1", mapper.ColumnSql);
		}

		[Test]
		public void SqlServerCreatesSqWithDefault()
		{
			var mapper = new ColumnPropertiesMapper(new SqlServerDialect(), "varchar(30)");
			mapper.MapColumnProperties(new Column("foo", DbType.String, 0, "'NEW'"));
			Assert.AreEqual("[foo] varchar(30) DEFAULT '''NEW'''", mapper.ColumnSql);
		}

		[Test]
		public void SqlServerCreatesSqWithNullDefault()
		{
			var mapper = new ColumnPropertiesMapper(new SqlServerDialect(), "varchar(30)");
			mapper.MapColumnProperties(new Column("foo", DbType.String, 0, "NULL"));
			Assert.AreEqual("[foo] varchar(30) DEFAULT 'NULL'", mapper.ColumnSql);
		}

		[Test]
		public void SqlServerCreatesSql()
		{
			var mapper = new ColumnPropertiesMapper(new SqlServerDialect(), "varchar(30)");
			mapper.MapColumnProperties(new Column("foo", DbType.String, 0));
			Assert.AreEqual("[foo] varchar(30)", mapper.ColumnSql);
		}

		[Test]
		public void SqlServerIndexSqlIsNoNullWhenIndexed()
		{
			var mapper = new ColumnPropertiesMapper(new SqlServerDialect(), "char(1)");
			mapper.MapColumnProperties(new Column("foo", DbType.StringFixedLength, 1, ColumnProperty.Indexed));
			Assert.IsNull(mapper.IndexSql);
		}
	}
}
