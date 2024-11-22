using SpreadCheetah.Benchmark.Benchmarks;
using SpreadCheetah.TestHelpers.Assertions;
using Xunit;

namespace SpreadCheetah.Benchmark.Test.Tests;

public sealed class StringCellsTests : IDisposable
{
    private const int NumberOfColumns = 10;
    private const int NumberOfRows = 20000;
    private readonly StringCells _stringCells;

    public StringCellsTests()
    {
        _stringCells = new StringCells
        {
            NumberOfColumns = NumberOfColumns,
            NumberOfRows = NumberOfRows,
            Stream = new MemoryStream()
        };

        _stringCells.GlobalSetup();
    }

    public void Dispose()
    {
        _stringCells.Dispose();
    }

    [Fact]
    public async Task StringCells_SpreadCheetah_CorrectCellValues()
    {
        // Act
        await _stringCells.SpreadCheetah();

        // Assert
        AssertCellValuesEqual();
    }

    [Fact]
    public void StringCells_EpPlus4_CorrectCellValues()
    {
        // Act
        _stringCells.EpPlus4();

        // Assert
        AssertCellValuesEqual();
    }

    [Fact]
    public void StringCells_OpenXmlSax_CorrectCellValues()
    {
        // Act
        _stringCells.OpenXmlSax();

        // Assert
        AssertCellValuesEqual();
    }

    [Fact]
    public void StringCells_OpenXmlDom_CorrectCellValues()
    {
        // Act
        _stringCells.OpenXmlDom();

        // Assert
        AssertCellValuesEqual();
    }

    [Fact]
    public void StringCells_ClosedXml_CorrectCellValues()
    {
        // Act
        _stringCells.ClosedXml();

        // Assert
        AssertCellValuesEqual();
    }

    private void AssertCellValuesEqual()
    {
        using var sheet = SpreadsheetAssert.SingleSheet(_stringCells.Stream);

        var values = _stringCells.Values;
        Assert.Equal(values.Count, sheet.RowCount);

        foreach (var (r, rowValues) in values.Index())
        {
            var row = sheet.Row(r + 1).ToList();
            Assert.Equal(rowValues.Count, row.Count);

            foreach (var (c, cellValue) in rowValues.Index())
            {
                Assert.Equal(cellValue, row[c].StringValue);
            }
        }
    }
}
