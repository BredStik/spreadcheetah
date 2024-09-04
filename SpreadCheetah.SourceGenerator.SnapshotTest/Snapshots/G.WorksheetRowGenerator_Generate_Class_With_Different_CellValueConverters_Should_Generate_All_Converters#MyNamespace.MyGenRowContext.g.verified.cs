﻿//HintName: MyNamespace.MyGenRowContext.g.cs
// <auto-generated />
#nullable enable
using SpreadCheetah;
using SpreadCheetah.SourceGeneration;
using SpreadCheetah.Styling;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNamespace
{
    public partial class MyGenRowContext
    {
        private static MyGenRowContext? _default;
        public static MyGenRowContext Default => _default ??= new MyGenRowContext();

        public MyGenRowContext()
        {
        }
        private static readonly MyNamespace.StringValueConverter _valueConverter0 = new MyNamespace.StringValueConverter(); 
        private static readonly MyNamespace.NullableIntValueConverter _valueConverter1 = new MyNamespace.NullableIntValueConverter(); 
        private static readonly MyNamespace.DecimalValueConverter _valueConverter2 = new MyNamespace.DecimalValueConverter(); 

        private WorksheetRowTypeInfo<MyNamespace.ClassWithSameCellValueConverters>? _ClassWithSameCellValueConverters;
        public WorksheetRowTypeInfo<MyNamespace.ClassWithSameCellValueConverters> ClassWithSameCellValueConverters => _ClassWithSameCellValueConverters
            ??= WorksheetRowMetadataServices.CreateObjectInfo<MyNamespace.ClassWithSameCellValueConverters>(
                AddHeaderRow0Async, AddAsRowAsync, AddRangeAsRowsAsync, null);

        private static async ValueTask AddHeaderRow0Async(SpreadCheetah.Spreadsheet spreadsheet, SpreadCheetah.Styling.StyleId? styleId, CancellationToken token)
        {
            var cells = ArrayPool<StyledCell>.Shared.Rent(3);
            try
            {
                cells[0] = new StyledCell("Property", styleId);
                cells[1] = new StyledCell("Property1", styleId);
                cells[2] = new StyledCell("Property1", styleId);
                await spreadsheet.AddRowAsync(cells.AsMemory(0, 3), token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<StyledCell>.Shared.Return(cells, true);
            }
        }

        private static ValueTask AddAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet, MyNamespace.ClassWithSameCellValueConverters? obj, CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (obj is null)
                return spreadsheet.AddRowAsync(ReadOnlyMemory<DataCell>.Empty, token);
            return AddAsRowInternalAsync(spreadsheet, obj, token);
        }

        private static ValueTask AddRangeAsRowsAsync(SpreadCheetah.Spreadsheet spreadsheet,
            IEnumerable<MyNamespace.ClassWithSameCellValueConverters?> objs,
            CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (objs is null)
                throw new ArgumentNullException(nameof(objs));
            return AddRangeAsRowsInternalAsync(spreadsheet, objs, token);
        }

        private static async ValueTask AddAsRowInternalAsync(SpreadCheetah.Spreadsheet spreadsheet,
            MyNamespace.ClassWithSameCellValueConverters obj,
            CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(3);
            try
            {
                var styleIds = Array.Empty<StyleId>();
                await AddCellsAsRowAsync(spreadsheet, obj, cells, styleIds, token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<DataCell>.Shared.Return(cells, true);
            }
        }

        private static async ValueTask AddRangeAsRowsInternalAsync(SpreadCheetah.Spreadsheet spreadsheet,
            IEnumerable<MyNamespace.ClassWithSameCellValueConverters?> objs,
            CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(3);
            try
            {
                var styleIds = Array.Empty<StyleId>();
                foreach (var obj in objs)
                {
                    await AddCellsAsRowAsync(spreadsheet, obj, cells, styleIds, token).ConfigureAwait(false);
                }
            }
            finally
            {
                ArrayPool<DataCell>.Shared.Return(cells, true);
            }
        }

        private static ValueTask AddCellsAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet,
            MyNamespace.ClassWithSameCellValueConverters? obj,
            DataCell[] cells, IReadOnlyList<StyleId> styleIds, CancellationToken token)
        {
            if (obj is null)
                return spreadsheet.AddRowAsync(ReadOnlyMemory<DataCell>.Empty, token);

            cells[0] = _valueConverter0.ConvertToCell(obj.Property);
            cells[1] = _valueConverter1.ConvertToCell(obj.Property1);
            cells[2] = _valueConverter2.ConvertToCell(obj.Property2);
            return spreadsheet.AddRowAsync(cells.AsMemory(0, 3), token);
        }

        private static DataCell ConstructTruncatedDataCell(string? value, int truncateLength)
        {
            return value is null || value.Length <= truncateLength
                ? new DataCell(value)
                : new DataCell(value.AsMemory(0, truncateLength));
        }
    }
}
