using SpreadCheetah.CellWriters;
using SpreadCheetah.Helpers;
using SpreadCheetah.Styling;
using SpreadCheetah.Styling.Internal;
using System.Buffers.Text;

namespace SpreadCheetah.CellValueWriters.Number;

internal sealed class DoubleCellValueWriter : NumberCellValueWriter
{
    protected override bool TryWriteValue(in DataCell cell, Span<byte> destination, out int bytesWritten)
    {
        return Utf8Formatter.TryFormat(cell.NumberValue.DoubleValue, destination, out bytesWritten);
    }

    public override bool TryWriteCell(in DataCell cell, DefaultStyling? defaultStyling, SpreadsheetBuffer buffer)
    {
        return buffer.TryWrite($"{BeginDataCell}{cell.NumberValue.DoubleValue}{EndDefaultCell}");
    }

    public override bool TryWriteCell(in DataCell cell, StyleId styleId, SpreadsheetBuffer buffer)
    {
        return buffer.TryWrite(
            $"{StyledCellHelper.BeginStyledNumberCell}{styleId.Id}{EndStyleBeginValue}" +
            $"{cell.NumberValue.DoubleValue}" +
            $"{EndDefaultCell}");
    }

    public override bool TryWriteCell(string formulaText, in DataCell cachedValue, StyleId? styleId, DefaultStyling? defaultStyling, SpreadsheetBuffer buffer)
    {
        if (styleId is { } style)
        {
            return buffer.TryWrite(
                $"{StyledCellHelper.BeginStyledNumberCell}{style.Id}{FormulaCellHelper.EndStyleBeginFormula}" +
                $"{formulaText}" +
                $"{FormulaCellHelper.EndFormulaBeginCachedValue}" +
                $"{cachedValue.NumberValue.DoubleValue}" +
                $"{FormulaCellHelper.EndCachedValueEndCell}");
        }

        return buffer.TryWrite(
            $"{FormulaCellHelper.BeginNumberFormulaCell}" +
            $"{formulaText}" +
            $"{FormulaCellHelper.EndFormulaBeginCachedValue}" +
            $"{cachedValue.NumberValue.DoubleValue}" +
            $"{FormulaCellHelper.EndCachedValueEndCell}");
    }

    public override bool TryWriteCellWithReference(in DataCell cell, DefaultStyling? defaultStyling, CellWriterState state)
    {
        return state.Buffer.TryWrite(
            $"{state}{EndStyleBeginValue}" +
            $"{cell.NumberValue.DoubleValue}" +
            $"{EndDefaultCell}");
    }

    public override bool TryWriteCellWithReference(in DataCell cell, StyleId styleId, CellWriterState state)
    {
        return state.Buffer.TryWrite(
            $"{state}{StyledCellHelper.EndReferenceBeginStyleId}{styleId.Id}{EndStyleBeginValue}" +
            $"{cell.NumberValue.DoubleValue}" +
            $"{EndDefaultCell}");
    }

    public override bool TryWriteCellWithReference(string formulaText, in DataCell cachedValue, StyleId? styleId, DefaultStyling? defaultStyling, CellWriterState state)
    {
        if (styleId is { } style)
        {
            return state.Buffer.TryWrite(
                $"{state}{StyledCellHelper.EndReferenceBeginStyleId}{style.Id}{FormulaCellHelper.EndStyleBeginFormula}" +
                $"{formulaText}" +
                $"{FormulaCellHelper.EndFormulaBeginCachedValue}" +
                $"{cachedValue.NumberValue.DoubleValue}" +
                $"{FormulaCellHelper.EndCachedValueEndCell}");
        }

        return state.Buffer.TryWrite(
            $"{state}{FormulaCellHelper.EndStyleBeginFormula}" +
            $"{formulaText}" +
            $"{FormulaCellHelper.EndFormulaBeginCachedValue}" +
            $"{cachedValue.NumberValue.DoubleValue}" +
            $"{FormulaCellHelper.EndCachedValueEndCell}");
    }
}
