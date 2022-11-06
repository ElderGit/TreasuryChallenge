using System;
using System.Diagnostics;
using System.Text;
using TreasuryChallenge;

Console.WriteLine("Tell me the number of lines do you need and press enter.");

var lineNumbers = int.Parse(Console.ReadLine());

var time = Stopwatch.StartNew();

StringBuilder uniqueCodes = await UniqueCodeGenerator.Generate(lineNumbers);

FileActions.RecordStringBuilder(uniqueCodes);

time.Stop();
Console.WriteLine(time.ElapsedMilliseconds);