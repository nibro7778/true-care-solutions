-- Check and create the 'clients_module' database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'clients_module')
BEGIN
    CREATE DATABASE clients_module;
END

-- Check and create the 'staffs_module' database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'staffs_module')
BEGIN
    CREATE DATABASE staffs_module;
END