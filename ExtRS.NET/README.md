# ExtRS for .NET Framework 4.8
This library has some useful formatting and encryption methods for .NET Framework 4.8

## Example method unit tested

  
```csharp
        public void CsvToJsonSucceeds()
        {
            var csv = @"comma,separated,values
                        are,super,duper
                        fun,fun,forever
                        data, is, beautiful";
            var json = FormattingService.CsvToJson(csv, ",");
            Assert.IsTrue(json.Length > 0);
        }
```
