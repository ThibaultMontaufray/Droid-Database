# Droid Database [![Official Site](https://img.shields.io/badge/site-servodroid.com-orange.svg)](http://servodroid.com)

Parsing all sentenses with vocabulary and times. XML file with almost french words. You can add your own database to have your words.

 [![License](https://img.shields.io/github/license/brandondahler/Data.HashFunction.svg)](https://raw.githubusercontent.com/ThibaultMontaufray/Tools4Libraries/master/License) [![Version Status](https://img.shields.io/nuget/v/Droid_Database.svg)](https://www.nuget.org/packages/Droid_Database/)    [![Build Status](https://travis-ci.org/ThibaultMontaufray/Droid-Database.svg?branch=master)](https://travis-ci.org/ThibaultMontaufray/Droid-Database)  [![Coverage Status](https://coveralls.io/repos/github/ThibaultMontaufray/Droid-Database/badge.svg?branch=master)](https://coveralls.io/github/ThibaultMontaufray/Droid-Database?branch=master)  [![Jenkins test](https://img.shields.io/jenkins/t/http/servodroid.com:8080/CI-Droid-Database.svg)](http://servodroid.com:8080/job/CI-Droid-Database/) 

# Usage

```csharp
MySqlAdapter.ShowTables();
MySqlAdapter.ExecuteQuery("select * from t_user where name like '%ibo%' group by familly_name");
```
