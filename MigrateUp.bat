
.\packages\FluentMigrator.Tools.1.2.1.0\tools\AnyCPU\40\Migrate -c "server=.\SQLExpress;database=advocado;Integrated Security=SSPI" -db sqlserver2008 -a "Advocado.Migrations\bin\Local\Advocado.Migrations.dll" -t migrate:up
