using FluentMigrator;

namespace CongratulateApi.DataAccess.Migrations;

[Migration(20240721130200, TransactionBehavior.None, "Add example data")]
public class AddExampleData : Migration {
    public override void Up()
    {
        String addExampleData = @"
            insert into people (name, surname, day_of_birth, photo_id, photo_url) 
            values ('Максим', 'Лавров', '1984-12-22', 'brvf6em6wtbfqnxlf93j', 'https://res.cloudinary.com/dsub7ofob/image/upload/v1722445160/brvf6em6wtbfqnxlf93j.jpg'),
                   ('Павел', 'Дуров', '1984-09-10', 'hchcscpcjssz9tuh3bar', 'https://res.cloudinary.com/dsub7ofob/image/upload/v1722445720/hchcscpcjssz9tuh3bar.jpg'),
                   ('Илон', 'Маск', '1980-08-04', 'gpmtxyhbjkniqphfefa9', 'https://res.cloudinary.com/dsub7ofob/image/upload/v1722445932/gpmtxyhbjkniqphfefa9.jpg'),
                   ('Егор', 'Крид', '1985-10-02', 'vtsg4ukkwbmgrcunqoa6', 'https://res.cloudinary.com/dsub7ofob/image/upload/v1722446087/vtsg4ukkwbmgrcunqoa6.jpg'),
                   ('Светлана', 'Дилярова', '1985-09-21', 'h1cnfrig9jo8mkva7lbm', 'https://res.cloudinary.com/dsub7ofob/image/upload/v1722446205/h1cnfrig9jo8mkva7lbm.jpg'),
                   ('Мария', 'Иванова', '1991-02-22', 'culp1sq3p6elx8wj3zqs', 'https://res.cloudinary.com/dsub7ofob/image/upload/v1722446345/culp1sq3p6elx8wj3zqs.jpg'),
                   ('Тайлер', 'Дёрден', '1963-12-18', 'pr6rradnebkb9u59e1k6', 'https://res.cloudinary.com/dsub7ofob/image/upload/v1722446656/pr6rradnebkb9u59e1k6.jpg'),
                   ('Дмитрий', 'Нагиев', '1967-04-04', 'qq1vcmtqaykmnpmplzqu', 'https://res.cloudinary.com/dsub7ofob/image/upload/v1722444687/qq1vcmtqaykmnpmplzqu.webp');
        ";
        Execute.Sql(addExampleData);
    }

    public override void Down()
    {
        
    }
}