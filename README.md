# Droid Database [![Official Site](https://img.shields.io/badge/site-servodroid.com-orange.svg)](http://servodroid.com)

Parsing all sentenses with vocabulary and times. XML file with almost french words. You can add your own database to have your words.

 [![License](https://img.shields.io/github/license/brandondahler/Data.HashFunction.svg)](https://raw.githubusercontent.com/ThibaultMontaufray/Tools4Libraries/master/License) [![Version Status](https://img.shields.io/nuget/v/Droid.Database.svg)](https://www.nuget.org/packages/Droid.Database/)    [![Build Status](https://travis-ci.org/ThibaultMontaufray/Droid-Database.svg?branch=master)](https://travis-ci.org/ThibaultMontaufray/Droid-Database)  [![Build status](https://ci.appveyor.com/api/projects/status/7b79fo326cqcy2ww?svg=true)](https://ci.appveyor.com/project/ThibaultMontaufray/Droid-database)  [![Coverage Status](https://coveralls.io/repos/github/ThibaultMontaufray/Droid-Database/badge.svg?branch=master)](https://coveralls.io/github/ThibaultMontaufray/Droid-Database?branch=master)  [![Jenkins test](https://img.shields.io/jenkins/t/http/18.223.66.119:8080/JobMouette.svg)](http://18.223.66.119:8080/job/JobMouette/) 

# Usage

```csharp
MySqlAdapter.ShowTables();
MySqlAdapter.ExecuteQuery("select * from t_user where name like '%ibo%' group by familly_name");
```
